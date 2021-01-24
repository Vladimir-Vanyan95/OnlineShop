using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.ViewModels;

namespace Data.Repositories.Interfaces
{
    public interface IVendorRepository
    {
        Task<List<VendorViewModel>> GetAll();
        Task Add(VendorAddViewModel model);
        Task Delete(int? Id);
    }
}
