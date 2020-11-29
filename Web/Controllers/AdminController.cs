using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Data.Repositories.Interfaces;
using Common.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AdminController(IProductRepository productRepository, ICategoryRepository categoryRepository,IWebHostEnvironment hostEnvironment)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _webHostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Product()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> ProductAdd()
        {
            ViewBag.Categories = await _categoryRepository.GetAll();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ProductAdd(ProductAddViewModel productAdd, List<IFormFile> ImageFile)
        {
            ViewBag.Categories = await _categoryRepository.GetAll();
            if (ModelState.IsValid)
            {
                var id = await _productRepository.Add(productAdd);
                if (ImageFile != null)
                {
                    List<ProductImageViewModel> images = new List<ProductImageViewModel>();
                    var folderPath = _webHostEnvironment.WebRootPath + $"/images/{id}";
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    foreach (var item in ImageFile)
                    {
                        var savePath = _webHostEnvironment.WebRootPath + $"/images/{id}/{item.FileName}";
                        using (var stream = new FileStream(savePath, FileMode.Create))
                        {
                            item.CopyTo(stream);
                        }
                        images.Add(new ProductImageViewModel
                        {
                            FileName = item.FileName,
                            ProductId = id
                        });
                    }
                    await _productRepository.AddImages(images);
                }
            }
            return View();
        }
        public async Task<IActionResult> Category(int? Id = null)
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
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CategoryAdd(CategoryAddViewModel categoryAdd)
        {
            if (ModelState.IsValid)
            {
                await _categoryRepository.Add(categoryAdd);
                return RedirectToAction("Category");
            }
            return View();
        }
    }
}
