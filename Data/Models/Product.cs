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
        public double Price { get; set; }
        public double Discount { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        public int VendorId { get; set; }
        [ForeignKey("VendorId")]
        public Vendor Vendor { get; set; }
        public string MainImage { get; set; }
        public ProductStatus ProductStatus { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
        public ICollection<ProductVariant> ProductVariants { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
