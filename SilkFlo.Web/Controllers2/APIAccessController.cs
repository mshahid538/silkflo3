using System;
using SilkFlo.Data.Core;
using SilkFlo.Web.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using SilkFlo.Web.APIControllers.Apis;
using SilkFlo.Web.ViewModels;
using System.Collections.Generic;
using ExcelDataReader;
using System.Linq;
using SilkFlo.Web.ViewModels.Dashboard;
using MediatR;
using Silkflo.API.Services.Ideas.Queries;
using Silkflo.API.Services.ClientApplicationInterfaceSession.Command;
using Microsoft.Extensions.Logging;
using Silkflo.API.Services.AccessKey.Queries;

namespace SilkFlo.Web.Controllers
{
    public class APIAccessController : AbstractAPI
    {
        private readonly ILogger<APIAccessController> _logger;
		private readonly IMediator _mediator;
		private readonly IConfiguration _configuration;
		static Dictionary<string, string> ImportStatus;

        public APIAccessController(IUnitOfWork unitOfWork, ViewToString viewToString, IAuthorizationService authorization, IConfiguration configuration
            , IMediator mediator, ILogger<APIAccessController> logger)
            : base(unitOfWork, viewToString, authorization)
        {
            _configuration = configuration;
            ImportStatus = new Dictionary<string, string>();
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("/api/APIAccessController/{tab}")]
        public async Task<IActionResult> GetCreateAPIAccessapi(string tab)
        {
            return await CreateAPIAccessView(true, tab);
        }



        [HttpGet("/APIAccessController/{tab}")]
        public async Task<IActionResult> GetCreateAPIAccess(string tab)
        {
            return await CreateAPIAccessView(false, tab);
        }

        private async Task<IActionResult> CreateAPIAccessView(
       bool returnStringContent,
       string tab)
        {
            if (!(await AuthorizeAsync(Policy.ManageTenantSettings)).Succeeded)
            {
                if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                {
                    if (returnStringContent)
                        return NegativeFeedback();

                    return Redirect("/account/signin");
                }

                return View("../home/Page", "<h1 class=\"text-warning\">You do not have permissions to manage platform settings.</h1>");
            }

            try
            {
                var client = await GetClientAsync();

                if (client == null)
                {
                    if (returnStringContent)
                        return NegativeFeedback();

                    return Redirect("/account/signin");
                }


                if (string.IsNullOrWhiteSpace(tab))
                    tab = "api";


				//var userId = GetUserId();
				//var user = await _unitOfWork.Users.GetAsync(userId);
				//var clients = await GetForUserValidatedAsync(user);

				//var clientForApis = clients.FirstOrDefault(x => !String.IsNullOrEmpty(x.AgencyId));

				var result = await _mediator.Send(new GetAllClientsAccessKeyQuery()
				{
					ClientId = client.Id
				});


				var aPIAccess = new ViewModels.Settings.APIAccess(
                    tab,
                    client.IsPractice,
                    result.Result
                    );


                const string url = "/Views/Settings/APIAccess.cshtml";
                if (returnStringContent)
                {
                    var html = await _viewToString.PartialAsync(url, aPIAccess);
                    return Content(html);
                }

                return View("/Views/Settings/APIAccess.cshtml", aPIAccess);
            }
            catch (Exception ex)
            {
                Log(ex);

                if (returnStringContent)
                    return Content("Error fetching data");


                return View("/Views/Home/maintenance.cshtml", "CreateAPIAccessView");
            }
        }

        [HttpPost("/SaveToken")]
        public async Task<IActionResult> SaveToken([FromBody] TokenData data)
        {
            try
            {
				//var userId = GetUserId();
				//var user = await _unitOfWork.Users.GetAsync(userId);
				//var clients = await GetForUserValidatedAsync(user);

				//var client = clients.FirstOrDefault(x => !String.IsNullOrEmpty(x.AgencyId));

				var client = await GetClientAsync();

				var result = await _mediator.Send(new CreateClientApplicationInterfaceSessionCommand() 
                { 
                    ClientId = client.Id,
                    Description = data.tokenDescription,
                    ExpirationDate = DateTime.Parse(data.tokenExpires),
                    IsActive = data.isActive,
                    Name = data.tokenName
                });

                return Ok(new { result.IsSucceed, result.Result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return Ok(new { IsSucceed = false, message = "Some error occurred during SaveToken." });
            }
        }


		[HttpPost("/GetTokens")]
		public async Task<IActionResult> GetTokens()
		{
			try
			{
				//var userId = GetUserId();
				//var user = await _unitOfWork.Users.GetAsync(userId);
				//var clients = await GetForUserValidatedAsync(user);

				//var client = clients.FirstOrDefault(x => !String.IsNullOrEmpty(x.AgencyId));

				var client = await GetClientAsync();

				var result = await _mediator.Send(new GetAllClientsAccessKeyQuery()
				{
					ClientId = client.Id
				});

				return Ok(new { result.IsSucceed, result.Result });
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message, ex);
				return Ok(new { IsSucceed = false, message = "Some error occurred during SaveToken." });
			}
		}

		public class TokenData
        {
            public string tokenName { get; set; }
            public string tokenDescription { get; set; }
            public string tokenExpires { get; set; }
            public bool isActive { get; set; }
        }

    }
}
