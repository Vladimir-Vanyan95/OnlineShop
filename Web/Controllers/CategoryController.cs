using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Data.Repositories.Interfaces;
using Common.ViewModels;
using Microsoft.AspNetCore.Authorization;
namespace Web.Controllers
{
    [Authorize (Roles ="admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<IActionResult> Categories(int? Id = null)
        {
            if (Id != null)
            {
                await _categoryRepository.Delete(Id);
            }
            var categories = await _categoryRepository.GetAll();
            return View(categories);
        }
        [HttpGet]
        public IActionResult CategoryAdd()
        {
            CategoryAddViewModel model = new CategoryAddViewModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CategoryAdd(CategoryAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id > 0)
                {
                    await _categoryRepository.Update(model);
                }
                else
                {
                    await _categoryRepository.Add(model);
                }
                return RedirectToAction("Categories");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> CategoryEdit(int Id)
        {
            var model= await _categoryRepository.FindById(Id);
            return View("CategoryAdd", model);
        }
    }
}
