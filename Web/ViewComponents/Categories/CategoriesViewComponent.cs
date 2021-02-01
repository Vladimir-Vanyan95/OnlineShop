using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Data.Repositories.Interfaces;

namespace Web.ViewComponents.Categories
{
    [ViewComponent (Name ="_Categories")]
    public class CategoriesViewComponent:ViewComponent
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoriesViewComponent(ICategoryRepository categoryRepository)
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
