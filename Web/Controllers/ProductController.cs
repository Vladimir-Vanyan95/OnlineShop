using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Data.Repositories.Interfaces;
using Data.Models;
using Data.Repositories;

namespace Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IGenericRepository<Category> _categoryRepsoitory;
        private readonly IGenericRepository<Product> _productRepository;
        public ProductController(IGenericRepository<Product> productRepository, IGenericRepository<Category> categoryRepository)
        {
            _categoryRepsoitory = categoryRepository;
            _productRepository = productRepository;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAll();
            return View(products);
        }
        public async Task<IActionResult> Search(string searchString)
        {
            var products = await _productRepository.GetAll();
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.Name.Contains(searchString)).ToList();
            }
            return View("Index", products);
        }
        public async Task<IActionResult> ProductView(int Id)
        {
            var product = await _productRepository.FindById(Id);
            return View(product);
        }
    }
}
