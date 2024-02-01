/*********************************************************
       Code Generated By  .  .  .  .  Delaney's ScriptBot
       WWW .  .  .  .  .  .  .  .  .  www.scriptbot.io
       Template Name.  .  .  .  .  .  Project Green 3.0
       Template Version.  .  .  .  .  20210407 014
       Author .  .  .  .  .  .  .  .  Delaney

                      ,        ,--,_
                       \ _ ___/ /\|
                       ( )__, )
                      |/_  '--,
                        \ `  / '

 *********************************************************/

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using SilkFlo.Data.Core;

namespace SilkFlo.Web.Controllers.Administration.Shared
{
    public partial class AutomationGoalController : Abstract
    {
        public AutomationGoalController(IUnitOfWork unitOfWork,
                                  Services.ViewToString viewToString,
                                  IAuthorizationService authorisation) : base(unitOfWork, viewToString, authorisation) { }


        [HttpGet("/Administration/Shared/AutomationGoal")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Index()
        {
            try
            {
                IEnumerable<Data.Core.Domain.Shared.AutomationGoal> cores;

                cores = (await _unitOfWork.SharedAutomationGoals
                                         .GetAllAsync())
                                         .ToArray();

                var models = new List<Models.Shared.AutomationGoal>();

                if(cores != null)
                {
                    cores = cores.OrderBy(m => m.Sort)
                                 .ThenBy(m => m.Name);

                    foreach(var core in cores)
                    {
                        var model = new Models.Shared.AutomationGoal(core);
                        models.Add(model);
                    }
                }

                var summary = new Models.Summary<Models.Shared.AutomationGoal>
                {
                    Models = models,
                    Count = models.Count,
                };

                return View("/Views/Administration/Shared/AutomationGoal/Index.cshtml", summary);
            }
            catch (Exception ex)
            {
                _unitOfWork.Log(ex);

                var summary = new Models.Summary<Models.Shared.AutomationGoal>
                {
                    Models = null,
                    Count = 0,
                };

                ModelState.AddModelError(
                        "Error",
                        Models.Log.Message_DatabaseErrorFetchList);

                return View("/Views/Administration/Shared/AutomationGoal/Index.cshtml", summary);
            }
        }
        [HttpGet("/Administration/Shared/AutomationGoal/Delete/{id}")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Delete (string id)
        {
            if (id == null)
                return Redirect("/Administration/shared/automationGoal");

            var automationGoal = await _unitOfWork.SharedAutomationGoals.GetAsync(id);

            if (automationGoal == null)
                return Redirect("/Administration/Shared/AutomationGoal");
            else
            {
                if (automationGoal.CreatedById != null)
                    automationGoal.CreatedBy = await _unitOfWork.Users.GetAsync(automationGoal.CreatedById);

                if (automationGoal.UpdatedById != null)
                    automationGoal.UpdatedBy = await _unitOfWork.Users.GetAsync(automationGoal.UpdatedById);

                var model = new Models.Shared.AutomationGoal(automationGoal);

                return View("/Views/Administration/Shared/AutomationGoal/delete.cshtml", model);
            }
        }
        [HttpPost("/Administration/Shared/AutomationGoal/Delete")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Delete(Models.Shared.AutomationGoal model)
        {
            if (model == null)
                return Redirect("/Administration/Shared/AutomationGoal");


            var userId = GetUserId();

            if (userId == null)
            {
                ModelState.AddModelError("Error",
                                         "UserId is missing.");

                return View("/Views/Administration/Shared/AutomationGoal/Delete.cshtml", model);
            }


            if (model.Id == null)
            {
                var message = "[POST] The primary key ID is missing.";

                ModelState.AddModelError("Error",
                                         message);

                _unitOfWork.Log(message,
                                Severity.Warning);

                await _unitOfWork.CompleteAsync();

                return View("/Views/Administration/Shared/AutomationGoal/Delete.cshtml", model);
            }


            try
            {
                await _unitOfWork.SharedAutomationGoals
                                 .RemoveAsync(model.Id);

                await _unitOfWork.CompleteAsync();

                return Redirect(
                    "/Administration/Shared/AutomationGoal");
            }
            catch (ChildDependencyException ex)
            {
                ModelState.AddModelError("Error",
                                         ex.Message);
                return View("/Views/Administration/Shared/AutomationGoal/Delete.cshtml", model);
            }
            catch (NotFoundException ex)
            {
                ModelState.AddModelError("Error",
                                         ex.Message);
                return View("/Views/Administration/Shared/AutomationGoal/Delete.cshtml", model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error",
                                         "Cannot delete the AutomationGoal.");

                _unitOfWork.Log(ex);
                await _unitOfWork.CompleteAsync();
                return View("/Views/Administration/Shared/AutomationGoal/Delete.cshtml", model);
            }
        }

        [HttpGet("/Administration/Shared/AutomationGoal/Edit/{id}")]
        [HttpGet("/Administration/Shared/AutomationGoal/Edit")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Edit(string id)
        {
            Data.Core.Domain.Shared.AutomationGoal automationGoal;

            if (string.IsNullOrWhiteSpace(id))
            {
                automationGoal = new Data.Core.Domain.Shared.AutomationGoal
                {
                    Id = Guid.NewGuid().ToString(),
                };
            }
            else
            {
                automationGoal = await _unitOfWork.SharedAutomationGoals.GetAsync(id);

                if (automationGoal == null)
                    return Redirect(
                        "/Administration/Shared/AutomationGoal");
            }

            var model = new Models.Shared.AutomationGoal(automationGoal);
            await model.GetCreatedAndUpdated(_unitOfWork);

            return View("/Views/Administration/Shared/AutomationGoal/Edit.cshtml", model);
        }


        [HttpPost("/Administration/Shared/AutomationGoal/Edit")]
        [Authorize(Policy = Policy.Administrator)]
        public async Task<IActionResult> Edit(Models.Shared.AutomationGoal model)
        {
            try
            {
                // Get the userId
                var userId = GetUserId();


                // Is the userId present.
                // If not return to view.                
                if (userId == null)
                {
                    ModelState.AddModelError(
                            "Error",
                            "userId is missing.");

                    return View("/Views/Administration/Shared/AutomationGoal/Edit.cshtml", model);
                }


                // Is the model valid?
                // If not return to view.
                bool isValid = Validate(_unitOfWork, model);

                var feedback = new ViewModels.Feedback();
                feedback = await model.CheckUniqueAsync(_unitOfWork, feedback);
                if (!feedback.IsValid)
                {
                    isValid = false;
                    foreach (var feedbackElement in feedback.Elements)
                        model.Errors.Add(feedbackElement.Value);
                }

                if (!isValid)
                {
                    return View("/Views/Administration/Shared/AutomationGoal/Edit.cshtml", model);
                }


                // Process the forms content.
                await _unitOfWork.SharedAutomationGoals.AddAsync(model.GetCore());


                if(await _unitOfWork.CompleteAsync() == DataStoreResult.Success)
                    return Redirect(
                        "/Administration/Shared/AutomationGoal");

                return View("/Views/Administration/Shared/AutomationGoal/Edit.cshtml", model);
            }
            catch (Exception ex)
            {
                _unitOfWork.Log(ex);

                ModelState.AddModelError(
                    "Error",
                    Models.Log.Message_CouldNotSave);

                return View("/Views/Administration/Shared/AutomationGoal/Edit.cshtml", model);
            }
        }

        [HttpPost("/Administration/Shared/AutomationGoal/Cancel")]
        public IActionResult Cancel()
        {
            return Redirect(
                "/Administration/Shared/AutomationGoal");
        }
    }
}