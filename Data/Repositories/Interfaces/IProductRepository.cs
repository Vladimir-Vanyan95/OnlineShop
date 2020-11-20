using Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task Add(ProductViewModel productViewModel);
        Task<List<ProductViewModel>> GetAll();
        Task Delete(int id);
    }
}
