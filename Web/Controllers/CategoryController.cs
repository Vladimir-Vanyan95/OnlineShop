using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Data.Repositories.Interfaces;
using Common.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Data.Repositories;
using Data.Models;

namespace Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class CategoryController : Controller
    {
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IWebHostEnvironment _webHostEnvironment; 
        public CategoryController(IGenericRepository<Category> categoryRepository,IWebHostEnvironment webHostEnvironment)
        {
            _categoryRepository = categoryRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Categories()
        {
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
        public async Task<IActionResult> CategoryAdd(CategoryAddViewModel model,IFormFile ImageFile)
        {
            if (ModelState.IsValid)
            {
                Category category = new Category
                {
                    Name = model.Name,
                    Image = ImageFile.FileName,
                    CreatedDate = DateTime.Now
                };
                if (ImageFile != null)
                {
                    model.Image = ImageFile.FileName;
                    var folderPath = _webHostEnvironment.WebRootPath + "/images/Categories";
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    var savePath = _webHostEnvironment.WebRootPath + $"/images/Categories/{ImageFile.FileName}";
                    using(var stream=new FileStream(savePath, FileMode.Create))
                    {
                        ImageFile.CopyTo(stream);
                    }
                }
                if (model.Id > 0)
                {
                     _categoryRepository.Update(category);
                }
                else
                {
                    await _categoryRepository.Add(category);
                }
                return RedirectToAction("Categories");
            }
            return View(model);
        }
      
        [HttpGet]
        public async Task<IActionResult> CategoryEdit(int Id)
        {
            var model = await _categoryRepository.FindById(Id);
            return View("CategoryAdd", model);
        }
        public async Task GetCategoriesViewBag()
        {
            ViewBag.categories = await _categoryRepository.GetAll();
        }
    }
}
