using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Common.ViewModels;
using Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Data.Repositories;
using Data.Models;

namespace Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class VariantController : Controller
    {
        private readonly IGenericRepository<Variant> _variantRepository;
        public VariantController(IGenericRepository<Product> productRepository, IGenericRepository<Variant> variantRepository)
        {
            _variantRepository = variantRepository;
        }
        [HttpGet]
        public IActionResult VariantAdd()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> VariantAdd(VariantViewModel model)
        {
            if (ModelState.IsValid)
            {
                Variant variant = new Variant
                {
                    Name = model.Name,
                    CreatedDate = DateTime.Now
                };
                await _variantRepository.Add(variant);
                return RedirectToAction("VariantView");
            }
            return View(model);
        }
        public async Task<IActionResult> VariantView()
        {
            var variants = await _variantRepository.GetAll();
            return View(variants);
        }
       
    }
}
