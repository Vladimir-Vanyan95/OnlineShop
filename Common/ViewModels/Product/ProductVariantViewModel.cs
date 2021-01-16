using System;
using System.Collections.Generic;
using System.Text;

namespace Common.ViewModels
{
    public class ProductVariantViewModel
    {
        public int? Id { get; set; }
        public string Value { get; set; }
        public double? Price { get; set; }
        public int ProductId { get; set; }
        public int VariantId { get; set; }
        public string VariantName { get; set; }
    }
}
