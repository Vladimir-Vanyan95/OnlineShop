using Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        void Add(CategoryViewModel categoryViewModel);
        List<CategoryViewModel> GetAll();
        void Delete(int id);
    }
}
