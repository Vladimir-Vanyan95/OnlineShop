﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Data.Repositories.Interfaces;

namespace Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }
        public async Task<IActionResult> Index(int? categoryId,int?vendorId)
        {
            var products = await _productRepository.GetAll(categoryId,vendorId);
            return View(products);
        }
        public async Task<IActionResult> ProductView(int Id)
        {
            var product = await _productRepository.FindById(Id);
            return View(product);
        }
    }
}
