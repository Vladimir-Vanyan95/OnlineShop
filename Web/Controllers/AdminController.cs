using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Common.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Data.Repositories;
using Data.Models;

namespace Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<Variant> _variantRepository;
        private readonly IGenericRepository<Vendor> _vendorRepository;
        public AdminController(IGenericRepository<Product> productRepository, IGenericRepository<Category> categoryRepository, IWebHostEnvironment hostEnvironment, IGenericRepository<Variant> variantRepository, IGenericRepository<Vendor> vendorRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _webHostEnvironment = hostEnvironment;
            _variantRepository = variantRepository;
            _vendorRepository = vendorRepository;
        }
        public async Task<IActionResult> ProductDelete(int Id)
        {
            await _productRepository.Delete(Id);
            return RedirectToAction("Product");
        }
        public async Task<IActionResult> Product()
        {
            var products = await _productRepository.GetAll();
            return View(products);
        }
        [HttpGet]
        public async Task<IActionResult> ProductAdd()
        {
            await CallViewBags();
            ProductAddViewModel model = new ProductAddViewModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ProductAdd(ProductAddViewModel productAdd, List<IFormFile> ImageFile)
        {

            if (ImageFile.Any())
            {
                productAdd.MainImage = ImageFile.FirstOrDefault().FileName;
            }
            if (ModelState.IsValid)
            {
                int ProdcutId = 0;
                Product product = new Product
                {
                    Name = productAdd.Name,
                    Discount = productAdd.Discount,
                    CategoryId = productAdd.CategoryId,
                    Price = productAdd.Price,
                    VendorId = productAdd.VendorId,
                    CategoryName = productAdd.CategoryName,
                    ProductStatus = productAdd.ProductStatus,
                    CreatedDate = DateTime.Now
                };
                await _productRepository.Add(product);
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
                }
                await CallViewBags();
                return RedirectToAction("ProductVariantAdd", "Variant", new { Id = ProdcutId });
            }
            ViewBag.categories = await _categoryRepository.GetAll();
            ViewBag.vendors = await _vendorRepository.GetAll();
            return View(productAdd);
        }
        public async Task<IActionResult> ProductView(int Id)
        {
            var product = await _productRepository.FindById(Id);
            return View(product);
        }
        public async Task CallViewBags()
        {
            ViewBag.categories = await _categoryRepository.GetAll();
            ViewBag.vendors = await _vendorRepository.GetAll();
        }
    }
}