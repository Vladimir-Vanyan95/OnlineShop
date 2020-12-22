using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.ViewModels;
using Data.Repositories.Interfaces;
using Data.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task Add(CategoryAddViewModel categoryViewModel)
        {
            Category category = new Category
            {
                Name = categoryViewModel.Name
            };
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int? categoryId = null)
        {
            Category category = await _context.Categories.Where(c => c.Id == categoryId).FirstOrDefaultAsync();
            _context.Categories.Remove(category);
            var products = await _context.Products.Where(p => p.CategoryId == categoryId).ToListAsync();
            _context.Products.RemoveRange(products);
            await _context.SaveChangesAsync();
        }
        public async Task<List<CategoryViewModel>> GetAll()
        {
            return await _context.Categories.Select(c => new CategoryViewModel
            {
                Id = c.Id,
                Name = c.Name
            }).ToListAsync();
        }
    }
}
