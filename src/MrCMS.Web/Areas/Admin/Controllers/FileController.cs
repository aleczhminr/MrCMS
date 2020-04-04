﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MrCMS.Entities.Documents.Media;
using MrCMS.Web.Areas.Admin.Models;
using MrCMS.Web.Areas.Admin.Services;
using MrCMS.Website.Controllers;

namespace MrCMS.Web.Areas.Admin.Controllers
{
    public class FileController : MrCMSAdminController
    {
        private readonly IFileAdminService _fileService;

        public FileController(IFileAdminService fileService)
        {
            _fileService = fileService;
        }


        [HttpPost]
        [ActionName("Files")]
        public async Task<JsonResult> Files_Post(int id) 
        {
            var list = new List<ViewDataUploadFilesResult>();
            foreach (var file in Request.Form?.Files ?? new FormFileCollection())
            {
                if (_fileService.IsValidFileType(file.FileName))
                {
                    ViewDataUploadFilesResult dbFile = await _fileService.AddFile(file.OpenReadStream(), file.FileName,
                        file.ContentType, file.Length, id);
                    list.Add(dbFile);
                }
            }

            return Json(list.ToArray());
        }


        [HttpPost]
        [ActionName("Delete")]
        //public ActionResult Delete_POST(MediaFile file)
        public async Task<ActionResult> Delete_POST(int id)
        {
            var file = _fileService.GetFile(id);
            if (file == null)
                return RedirectToAction("Index", "MediaCategory");
            
            int categoryId = file.MediaCategory.Id;
            await _fileService.DeleteFile(file);
            return RedirectToAction("Show", "MediaCategory", new { Id = categoryId });
        }

        [HttpGet]
        public ActionResult Delete(MediaFile file)
        {
            return View("Delete", file);
        }

        [HttpPost]
        public async Task<string> UpdateSEO(MediaFile mediaFile, string title, string description)
        {
            try
            {
                mediaFile.Title = title;
                mediaFile.Description = description;
            await    _fileService.UpdateFile(mediaFile);

                return "Changes saved";
            }
            catch (Exception ex)
            {
                return string.Format("There was an error saving the SEO values: {0}", ex.Message);
            }
        }

        public ActionResult Edit(MediaFile file)
        {
            return View("Edit", file);
        }

        [HttpPost]
        [ActionName("Edit")]
        public async Task<ActionResult> Edit_POST(MediaFile file)
        {
            await _fileService.UpdateFile(file);

            return file.MediaCategory != null
                ? RedirectToAction("Show", "MediaCategory", new {file.MediaCategory.Id})
                : RedirectToAction("Index", "MediaCategory");
        }
    }
}