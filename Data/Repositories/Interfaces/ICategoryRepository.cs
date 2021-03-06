﻿using Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task Add(CategoryAddViewModel categoryViewModel);
        Task<List<CategoryViewModel>> GetAll();
        Task<CategoryAddViewModel> FindById(int Id);
        Task Update(CategoryAddViewModel model);
        Task SubcategoryAdd(CategoryAddViewModel model);
    }
}
