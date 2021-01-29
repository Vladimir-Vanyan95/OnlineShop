using System;
using System.Collections.Generic;
using System.Text;
using Common.Enums;

namespace Common.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double? Discount { get; set; }
        public int NewPrice =>(int)Price-(int)Discount * ((int)Price / 100);
        public string MainImage { get; set; }
        public List<ProductVariantViewModel> VariantModels { get; set; }
        public ProductStatus ProductStatus { get; set; }
        public int CategoryId { get; set; }
        public int VendorId { get; set; }
    }
}
