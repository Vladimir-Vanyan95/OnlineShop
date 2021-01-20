using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Common.ViewModels
{
    public class ProductVariantViewModel
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Input Value")]
        public string Value { get; set; }
        public int ProductId { get; set; }
        public int VariantId { get; set; }
        public string VariantName { get; set; }
    }
}
