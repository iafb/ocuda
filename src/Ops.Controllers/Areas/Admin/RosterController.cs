﻿using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ocuda.Ops.Controllers.Abstract;
using Ocuda.Ops.Controllers.Areas.Admin.ViewModels.Roster;
using Ocuda.Ops.Controllers.Filters;
using Ocuda.Ops.Service.Interfaces.Ops.Services;
using Ocuda.Utility.Keys;

namespace Ocuda.Ops.Controllers.Areas.Admin
{
    [Area("Admin")]
    [Authorize(Policy = nameof(ClaimType.SiteManager))]
    [Route("[area]/[controller]")]
    public class RosterController : BaseController<RosterController>
    {
        private readonly IRosterService _rosterService;

        public RosterController(ServiceFacades.Controller<RosterController> context,
            IRosterService rosterService) : base(context)
        {
            _rosterService = rosterService
                ?? throw new ArgumentNullException(nameof(rosterService));
        }

        [Route("[action]")]
        [RestoreModelState]
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        [Route("[action]")]
        [SaveModelState]
        public async Task<IActionResult> Upload(UploadViewModel model)
        {
            if (ModelState.IsValid)
            {
                var tempFile = Path.GetTempFileName();
                using (var fileStream = new FileStream(tempFile, FileMode.Create))
                {
                    await model.Roster.CopyToAsync(fileStream);
                }

                try
                {
                    int insertedRecordCount
                        = await _rosterService.ImportRosterAsync(CurrentUserId, tempFile);
                    AlertInfo = $"Successfully inserted {insertedRecordCount} roster records.";
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error inserting roster data: {Message}", ex);
                    AlertDanger = "An error occured while uploading the roster.";
                }
                finally
                {
                    System.IO.File.Delete(Path.Combine(Path.GetTempPath(), tempFile));
                }

                return RedirectToAction(nameof(Upload));
            }
            else
            {
                var filenameErrors = ModelState[nameof(model.FileName)]?.Errors?.ToList();
                if (filenameErrors?.Count > 0)
                {
                    foreach (var error in filenameErrors)
                    {
                        ModelState[nameof(model.Roster)].Errors.Add(error);
                    }
                }
            }

            return RedirectToAction(nameof(Upload));
        }
    }
}
