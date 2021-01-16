using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Common.ViewModels;
using Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class VariantController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IVariantRepository _variantRepository;
        public VariantController(IProductRepository productRepository, IVariantRepository variantRepository)
        {
            _productRepository = productRepository;
            _variantRepository = variantRepository;
        }
        public async Task<IActionResult> VariantAdd()
        {
            return View();
        }
        public async Task<IActionResult> VariantView()
        {
            var variants = await _variantRepository.GetVariants();
            return View(variants);
        }
        [HttpGet]
        public async Task<IActionResult> ProductVariantAdd(int Id)
        {
            ProductVariantViewModel model = new ProductVariantViewModel() { ProductId = Id };
            ViewBag.variants = await _variantRepository.GetVariants();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ProductVariantAdd(ProductVariantViewModel model)
        {
            ViewBag.variants = await _variantRepository.GetVariants();
            await _variantRepository.ProductVariantAdd(model);
            return View();
        }
    }
}
