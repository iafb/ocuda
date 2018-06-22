﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ocuda.Ops.Controllers.Abstract;
using Ocuda.Ops.Controllers.Areas.Admin.ViewModels.Links;
using Ocuda.Ops.Service;
using Ocuda.Utility.Models;

namespace Ocuda.Ops.Controllers.Areas.Admin
{
    [Area("Admin")]
    public class LinksController : BaseController
    {
        private readonly LinkService _linkService;

        public LinksController(LinkService linkService)
        {
            _linkService = linkService ?? throw new ArgumentNullException(nameof(linkService));
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var linkList = await _linkService.GetLinksAsync();

            var paginateModel = new PaginateModel()
            {
                ItemCount = await _linkService.GetLinkCountAsync(),
                CurrentPage = page,
                ItemsPerPage = 2
            };

            if (paginateModel.MaxPage > 0 && paginateModel.CurrentPage > paginateModel.MaxPage)
            {
                return RedirectToRoute(
                    new
                    {
                        page = paginateModel.LastPage ?? 1
                    });
            }

            var viewModel = new IndexViewModel()
            {
                PaginateModel = paginateModel,
                Links = linkList.Skip((page - 1) * paginateModel.ItemsPerPage)
                                        .Take(paginateModel.ItemsPerPage)
            };

            return View(viewModel);
        }

        public IActionResult Create()
        {
            var viewModel = new DetailViewModel()
            {
                Action = nameof(Create)
            };

            return View("Detail", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DetailViewModel model)
        {
            model.Link.SectionId = 1; //TODO: Fix SectionId, CreatedBy
            model.Link.CreatedBy = 1;

            if (ModelState.IsValid)
            {
                try
                {
                    var newLink = await _linkService.CreateAsync(model.Link);
                    ShowAlertSuccess($"Added link: {newLink.Name}");
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ShowAlertDanger("Unable to add link: ", ex.Message);
                }
            }

            model.Action = nameof(Create);
            return View("Detail", model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var viewModel = new DetailViewModel()
            {
                Action = nameof(Edit),
                Link = await _linkService.GetByIdAsync(id)
            };

            return View("Detail", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DetailViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.Link.SectionId = 1; //TODO: Use actual SectionId
                    var link = await _linkService.EditAsync(model.Link);
                    ShowAlertSuccess($"Updated link: {link.Name}");
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ShowAlertDanger("Unable to update link: ", ex.Message);
                }
            }

            model.Action = nameof(Edit);
            return View("Detail", model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(IndexViewModel model)
        {
            try
            {
                await _linkService.DeleteAsync(model.Link.Id);
                ShowAlertSuccess("Link deleted successfully.");
            }
            catch (Exception ex)
            {
                ShowAlertDanger("Unable to delete link: ", ex.Message);
            }

            return RedirectToAction(nameof(Index), new { page = model.PaginateModel.CurrentPage });
        }

        public IActionResult Categories(int page = 1)
        {
            var categoryList = _linkService.GetLinkCategories();

            var paginateModel = new PaginateModel()
            {
                ItemCount = categoryList.Count(),
                CurrentPage = page,
                ItemsPerPage = 2
            };

            if (paginateModel.MaxPage > 0 && paginateModel.CurrentPage > paginateModel.MaxPage)
            {
                return RedirectToRoute(
                    new
                    {
                        page = paginateModel.LastPage ?? 1
                    });
            }

            var viewModel = new CategoriesViewModel()
            {
                PaginateModel = paginateModel,
                Categories = categoryList.Skip((page - 1) * paginateModel.ItemsPerPage)
                                            .Take(paginateModel.ItemsPerPage)
            };

            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoriesViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newCategory = await _linkService.CreateLinkCategoryAsync(model.Category);
                    ShowAlertSuccess($"Added link category: {newCategory.Name}");
                }
                catch (Exception ex)
                {
                    ShowAlertDanger("Unable to add category: ", ex.Message);
                }
            }

            return RedirectToAction(nameof(Categories), new { page = model.PaginateModel.CurrentPage });
        }

        [HttpPost]
        public async Task<IActionResult> EditCategory(CategoriesViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var category = await _linkService.EditLinkCategoryAsync(model.Category);
                    ShowAlertSuccess($"Updated link category: {category.Name}");
                }
                catch (Exception ex)
                {
                    ShowAlertDanger("Unable to update category: ", ex.Message);
                }
            }

            return RedirectToAction(nameof(Categories), new { page = model.PaginateModel.CurrentPage });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(CategoriesViewModel model)
        {
            try
            {
                await _linkService.DeleteLinkCategoryAsync(model.Category.Id);
                ShowAlertSuccess("Link category deleted successfully.");
            }
            catch (Exception ex)
            {
                ShowAlertDanger("Unable to delete category: ", ex.Message);
            }

            return RedirectToAction(nameof(Categories), new { page = model.PaginateModel.CurrentPage });
        }
    }
}
