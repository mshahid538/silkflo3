using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.Extensions.Logging;
using Silkflo.API.Services.Ideas.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Web.APIControllers.External
{
    /// <summary>
    /// This is a sample class.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Consumes("application/json")]
    public class IdeaController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<IdeaController> _logger;

        /// <summary>
        /// This is an Ideas class used for ideas processing.
        /// </summary>
        public IdeaController(IMediator mediator, ILogger<IdeaController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }


        /// <summary>
        /// Get a list of ideas.
        /// </summary>
        /// <returns>List of ideas.</returns>
        /// <response code="200">Returns the list of ideas.</response>
        [HttpGet]
        [ProducesResponseType(typeof(Data.Core.Domain.Business.Idea), 200)]
        public async Task<IActionResult> GetIdeas()
        {
            try
            {
                if(!Request.Headers.ContainsKey("x-silkflo-api"))
                    return Unauthorized();

                var secretToken = Request.Headers["x-silkflo-api"];
                var result = await _mediator.Send(new GetAllClientIdeasQuery() { SecretToken = secretToken });

                return Ok(result);
            }
            catch(ArgumentException arx)
            {
                _logger.LogInformation(arx.Message);
                return Unauthorized();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500, "An unexpected error occurred");
            }
        }
    }
}
