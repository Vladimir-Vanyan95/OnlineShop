using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.ViewModels;
using Data.Repositories.Interfaces;
using Data.Models;
using System.Linq;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class VendorRepository:IVendorRepository
    {
        private readonly AppDbContext _context;
        public VendorRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<VendorViewModel>> GetAll()
        {
            return await _context.Vendors.Select(v => new VendorViewModel
            {
                Id = v.Id,
                Name = v.Name,
                Image = v.Image
            }).ToListAsync();
        }
        public async Task Delete(int? Id)
        {
            var model = await _context.Vendors.Where(v => v.Id == Id).FirstOrDefaultAsync();
              _context.Remove(model);
            await _context.SaveChangesAsync();

        }
        public async Task Add(VendorAddViewModel model)
        {
            Vendor vendor = new Vendor()
            {
                Name = model.Name,
                Image = model.Image
            };
            await _context.Vendors.AddAsync(vendor);
            await _context.SaveChangesAsync();
        }
    }
}
