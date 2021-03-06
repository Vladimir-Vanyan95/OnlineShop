﻿using Common.ViewModels;
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
            var categoryName = await _context.Categories.Select(p => new { p.Name }).FirstOrDefaultAsync();
            Product product = new Product
            {
                Name = productAdd.Name,
                Price = productAdd.Price,
                Discount = productAdd.Discount,
                CategoryId = productAdd.CategoryId,
                MainImage = productAdd.MainImage,
                ProductStatus = productAdd.ProductStatus,
                VendorId = productAdd.VendorId,
                CategoryName = categoryName.Name,
                CreatedDate = DateTime.Now,
            };
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product.Id;
        }
        public async Task<List<ProductViewModel>> GetAll(int? categoryId, int? vendorId)
        {
            var products = await _context.Products.Where(p => ((categoryId == null || p.CategoryId == categoryId) && (vendorId == null || p.VendorId == vendorId))).Select
                (p => new ProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    CategoryId = p.CategoryId,
                    Discount = p.Discount,
                    MainImage = p.MainImage,
                    Price = p.Price,
                    VendorId = p.VendorId,
                    ProductStatus = p.ProductStatus,
                    CategoryName=p.CategoryName
                }).ToListAsync();
            return products;
        }
        public async Task Delete(int? Id)
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
                VendorId = p.VendorId

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
            product.VendorId = model.VendorId;
            await _context.SaveChangesAsync();
        }
        public async Task<ProductViewModel> FindById(int Id)
        {
            var model = await _context.Products.Where(p => p.Id == Id).Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Discount = p.Discount,
                CategoryId = p.CategoryId,
                MainImage = p.MainImage,
                ProductStatus = p.ProductStatus,
                VendorId = p.VendorId,
                CategoryName=p.CategoryName
            }).FirstOrDefaultAsync();
            model.VariantModels = await _context.ProductVariants.Where(v => v.ProductId == Id).Select(v => new ProductVariantViewModel
            {
                Id = v.Id,
                Value = v.Value,
                ProductId = v.ProductId,
                VariantId = v.VariantId,
            }).ToListAsync();
            VariantViewModel variantModel = new VariantViewModel();
            foreach (var item in model.VariantModels)
            {
                variantModel = await _context.Variants.Where(v => v.Id == item.VariantId).Select(v => new VariantViewModel
                {
                    Name = v.Name
                }).FirstOrDefaultAsync();
                item.VariantName = variantModel.Name;
            }
            return model;
        }
        public async Task <Tuple<int,double>> AddToCart(int productId)
        {
            var check = await _context.Carts.Where(c => c.ProductId == productId).FirstOrDefaultAsync();
            if (check == null)
            {
                Cart cart = new Cart
                {
                    Count = 1,
                    ProductId = productId
                };
                await _context.Carts.AddAsync(cart);
            }
            else
            {
                check.Count=check.Count+1;
            }
            await _context.SaveChangesAsync();
            var currentCart = await _context.Carts/*.Where(c=>c.ProductId==productId)*/.Select(c => new { c.Count, c.Product.Price }).ToListAsync();
            int allcount = currentCart.Sum(c => c.Count);
            double allprice = currentCart.Sum(c => c.Count * c.Price);
            return Tuple.Create(allcount,allprice);
        }
    }
}
