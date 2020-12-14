﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Data.Repositories.Interfaces;

namespace Web.ViewComponents.Menu
{
    [ViewComponent (Name =("_Menu"))]
    public class MenuViewComponent:ViewComponent
    {
        private readonly ICategoryRepository _categoryRepository;
        public MenuViewComponent(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model =await _categoryRepository.GetAll();
            return View(model);
        }
    }
}