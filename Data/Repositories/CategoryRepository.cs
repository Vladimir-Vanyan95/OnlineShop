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
                Name = categoryViewModel.Name,
                CreatedDate=DateTime.Now
            };
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }
        public async Task<CategoryAddViewModel>FindById(int Id)
        {
            return await _context.Categories.Where(c => c.Id == Id).Select(c => new CategoryAddViewModel
            {
                Id = c.Id,
                Name = c.Name
            }).FirstOrDefaultAsync();
        }
        public async Task Update(CategoryAddViewModel model)
        {
            var category = await _context.Categories.Where(c => c.Id == model.Id).FirstOrDefaultAsync();
            category.Id = model.Id;
            category.Name = model.Name;
            category.UpdatedDate = DateTime.Now;
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
