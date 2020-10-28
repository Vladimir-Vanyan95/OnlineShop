using Common.ViewModels;
using Data.Models;
using Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(ProductViewModel productAdd)
        {
            Product product = new Product
            {
                Name = productAdd.Name,
                Price = productAdd.Price,
                Discount = productAdd.Discount,
                CategoryId = productAdd.CategoryId
            };
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public List<ProductViewModel> GetAll()
        {
            var allList = _context.Products.Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Discount = p.Discount,
                CategoryId = p.CategoryId
            }).ToList();
            return allList;

        }

        public void Delete(int id)
        {
            Product product = _context.Products.Where(p => p.Id == id).FirstOrDefault();
            _context.Products.Remove(product);
            _context.SaveChanges();
        }
    }
}
