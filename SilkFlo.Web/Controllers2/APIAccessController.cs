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

namespace SilkFlo.Web.Controllers
{
    public class APIAccessController : AbstractAPI
    {
        private readonly IConfiguration _configuration;
		static Dictionary<string, string> ImportStatus;
        public APIAccessController(IUnitOfWork unitOfWork, ViewToString viewToString, IAuthorizationService authorization, IConfiguration configuration)
            : base(unitOfWork, viewToString, authorization)
        {
            _configuration = configuration;
            ImportStatus = new Dictionary<string, string>();
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
                    tab = "business-units";

                var aPIAccess = new ViewModels.Settings.APIAccess(
                    tab,
                    client.IsPractice);


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

    }
}
