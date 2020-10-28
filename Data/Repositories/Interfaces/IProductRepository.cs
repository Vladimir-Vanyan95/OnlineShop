using Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories.Interfaces
{
    public interface IProductRepository
    {
        void Add(ProductViewModel productViewModel);
        List<ProductViewModel> GetAll();
        void Delete(int id);
    }
}
