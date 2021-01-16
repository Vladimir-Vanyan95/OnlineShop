using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.ViewModels;

namespace Data.Repositories.Interfaces
{
   public interface IVariantRepository
    {
        Task<List<VariantViewModel>> GetVariants();
        Task ProductVariantAdd(ProductVariantViewModel model);
        Task VariantAdd(VariantViewModel model);
    }
}
