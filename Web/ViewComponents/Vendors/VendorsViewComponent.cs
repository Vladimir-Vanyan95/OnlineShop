using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Data.Repositories.Interfaces;

namespace Web.ViewComponents.Vendors
{
    [ViewComponent(Name = "_Vendors")]
    public class VendorsViewComponent : ViewComponent
    {
        private readonly IVendorRepository _vendorRepository;
        public VendorsViewComponent(IVendorRepository vendorRepository)
        {
            _vendorRepository = vendorRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var vendors = await _vendorRepository.GetAll();
            return View(vendors);
        }
    }
}
