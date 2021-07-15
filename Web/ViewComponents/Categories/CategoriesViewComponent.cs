using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Data.Repositories.Interfaces;
using Data.Repositories;
using Data.Models;

namespace Web.ViewComponents.Categories
{
    [ViewComponent (Name ="_Categories")]
    public class CategoriesViewComponent:ViewComponent
    {
        private readonly IGenericRepository<Category> _categoryRepository;
        public CategoriesViewComponent(IGenericRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _categoryRepository.GetAll();
            return View(categories);
        }
    }
}
