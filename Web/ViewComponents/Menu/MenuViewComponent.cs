using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Data.Repositories.Interfaces;
using Data.Repositories;
using Data.Models;

namespace Web.ViewComponents.Menu
{
    [ViewComponent (Name =("_Menu"))]
    public class MenuViewComponent:ViewComponent
    {
        private readonly IGenericRepository<Category> _categoryRepository;
        public MenuViewComponent(IGenericRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model =await _categoryRepository.GetAll();
            if (model.Count > 4)
            {
                model.RemoveRange(4,model.Count-4);
            }
            return View(model);
        }
    }
}
