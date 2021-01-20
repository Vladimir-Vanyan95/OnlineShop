using System;
using System.Collections.Generic;
using System.Text;
using Common.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class ProductVariant
    {
        public int Id { get; set; }
        public int VariantId { get; set; }
        public int ProductId { get; set; }
        public string Value { get; set; }
        [ForeignKey("VariantId")]
        public Variant Variant { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
