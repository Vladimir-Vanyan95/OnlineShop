using Common.ViewModels;
using Data.Models;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> Add(ProductAddViewModel productAdd)
        {
            Product product = new Product
            {
                Name = productAdd.Name,
                Price = productAdd.Price,
                Discount = productAdd.Discount,
                CategoryId = productAdd.CategoryId,
                MainImage = productAdd.MainImage,
                ProductStatus = productAdd.ProductStatus,
                CreatedDate = DateTime.Now

            };
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product.Id;
        }

        public async Task<List<ProductViewModel>> GetAll(int? categoryId)
        {
            var products = await _context.Products.Where(p => (categoryId == null || p.CategoryId == categoryId)).Select
                (p => new ProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    CategoryId = p.CategoryId,
                    Discount = p.Discount,
                    MainImage = p.MainImage,
                    Price = p.Price,
                    ProductStatus = p.ProductStatus
                }).ToListAsync();
            return products;
        }
        public async Task Delete(int Id)
        {
            Product product = await _context.Products.Where(p => p.Id == Id).FirstOrDefaultAsync();
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
        public async Task<ProductAddViewModel> Edit(int Id)
        {
            return await _context.Products.Where(p => p.Id == Id).Select(p => new ProductAddViewModel
            {
                Id = p.Id,
                Name = p.Name,
                CategoryId = p.CategoryId,
                Discount = p.Discount,
                MainImage = p.MainImage,
                Price = p.Price,
                ProductStatus = p.ProductStatus,
                
            }).FirstOrDefaultAsync();
        }
        public async Task AddImages(List<ProductImageViewModel> imageModel)
        {
            var images = imageModel.Select(i => new ProductImage
            {
                FileName = i.FileName,
                ProductId = i.ProductId
            });
            await _context.AddRangeAsync(images);
            await _context.SaveChangesAsync();
        }
        public async Task Update(ProductAddViewModel model)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == model.Id);
            product.Id = model.Id;
            product.Name = model.Name;
            product.CategoryId = model.CategoryId;
            product.Discount = model.Discount;
            product.MainImage = model.MainImage;
            product.Price = model.Price;
            product.UpdatedDate = DateTime.Now;
            await _context.SaveChangesAsync();
        }
        public async Task<ProductViewModel> FindById(int Id)
        {
            return await _context.Products.Where(p => p.Id == Id).Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Discount = p.Discount,
                CategoryId = p.CategoryId,
                MainImage = p.MainImage,
                ProductStatus = p.ProductStatus
            }).FirstOrDefaultAsync();
        }
    }
}
