using Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<int> Add(ProductAddViewModel productViewModel);
        Task<List<ProductViewModel>> GetAll();
        Task Delete(int id);
        Task<ProductAddViewModel> Edit(int Id);
        Task AddImages(List<ProductImageViewModel> imageModel);
        Task Update(ProductAddViewModel model);
        Task<ProductViewModel> FindById(int Id);
    }
}
