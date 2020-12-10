using Data.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Common.Enums;

namespace Data.Models
{
    public class Product : IModelBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
        public string MainImage { get; set; }
        public ProductStatus ProductStatus { get; set; }
        public Category Category { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
        public ICollection<CartProduct> CartProducts { get; set; }
    }
}
