using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Data.Repositories.Interfaces;
using Data.Repositories;
using Data.Models;

namespace Web.ViewComponents.Vendors
{
    [ViewComponent(Name = "_Vendors")]
    public class VendorsViewComponent : ViewComponent
    {
        private readonly IGenericRepository<Vendor> _vendorRepository;
        public VendorsViewComponent(IGenericRepository<Vendor> vendorRepository)
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
