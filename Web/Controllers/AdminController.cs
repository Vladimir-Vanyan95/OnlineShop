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
        private readonly IVariantRepository _variantRepository;
        public AdminController(IProductRepository productRepository, ICategoryRepository categoryRepository, IWebHostEnvironment hostEnvironment,IVariantRepository variantRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _webHostEnvironment = hostEnvironment;
            _variantRepository = variantRepository;
        }
        public async Task<IActionResult> Edit(int Id)
        {
            ViewBag.Categories = await _categoryRepository.GetAll();
            var model = await _productRepository.Edit(Id);
            return View("ProductAdd", model);
        }
        public async Task<IActionResult> Product(int Id = 0)
        {
            if (Id > 0)
            {
                await _productRepository.Delete(Id);
            }
            var products = await _productRepository.GetAll(null);
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> ProductAdd()
        {
            ViewBag.categories = await _categoryRepository.GetAll();
            ViewBag.variants = await _variantRepository.GetVariants();
            ProductAddViewModel model = new ProductAddViewModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ProductAdd(ProductAddViewModel productAdd, List<IFormFile> ImageFile)
        {
            productAdd.MainImage = ImageFile.FirstOrDefault().FileName;
            if (ModelState.IsValid)
            {
                int ProdcutId = 0;
                if (productAdd.Id > 0)
                {
                    ProdcutId = productAdd.Id;
                    await _productRepository.Update(productAdd);
                }
                else { ProdcutId = await _productRepository.Add(productAdd); }
                if (ImageFile != null)
                {
                    List<ProductImageViewModel> images = new List<ProductImageViewModel>();
                    var folderPath = _webHostEnvironment.WebRootPath + $"/images/{ProdcutId}";
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    foreach (var item in ImageFile)
                    {
                        var savePath = _webHostEnvironment.WebRootPath + $"/images/{ProdcutId}/{item.FileName}";
                        using (var stream = new FileStream(savePath, FileMode.Create))
                        {
                            item.CopyTo(stream);
                        }
                        images.Add(new ProductImageViewModel
                        {
                            FileName = item.FileName,
                            ProductId = ProdcutId
                        });
                    }
                    await _productRepository.AddImages(images);
                }
                return RedirectToAction("VariantAdd", new { Id = ProdcutId });
            }
            return View(productAdd);
        }
        public async Task<IActionResult> ProductView(int Id)
        {
            var product = await _productRepository.FindById(Id);
            return View(product);
        }
        
    }
}