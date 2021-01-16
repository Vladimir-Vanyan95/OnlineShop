using System;
using System.Collections.Generic;
using System.Text;
using Data.Repositories.Interfaces;
using System.Linq;
using Common.ViewModels;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class VariantRepositroy : IVariantRepository
    {
        private readonly AppDbContext _context;
        public VariantRepositroy(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<VariantViewModel>> GetVariants()
        {
            return await _context.Variants.Select(v => new VariantViewModel
            {
                Id = v.Id,
                Name = v.Name
            }).ToListAsync();
        }
        public async Task ProductVariantAdd(ProductVariantViewModel model)
        {
            ProductVariant productVariant = new ProductVariant
            {
                Value = model.Value,
                ProductId = model.ProductId,
                VariantId = model.VariantId,
            };
            await _context.ProductVariants.AddAsync(productVariant);
            await _context.SaveChangesAsync();
        }
        public async Task VariantAdd(VariantViewModel model)
        {
            Variant variant = new Variant()
            {
                Name = model.Name
            };
            await _context.Variants.AddAsync(variant);
            await _context.SaveChangesAsync();
        }
    }
}
