using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Data.Repositories.Interfaces;
using Common.ViewModels;

namespace Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        public AdminController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        //[HttpGet]
        //public IActionResult ProductAdd()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public async Task<IActionResult> ProductAdd(ProductAddViewModel productAdd)
        //{
        //    return View();
        //}
        public async Task<IActionResult> Category(int? Id=null)
        {
            if (Id != null)
            {
                await _categoryRepository.Delete(Id);
            }
            ViewBag.AllCategories =await _categoryRepository.GetAll();
            return View();
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
