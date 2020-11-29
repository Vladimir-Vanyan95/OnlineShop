using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Common.ViewModels
{
  public  class ProductAddViewModel
    {
        [Required(ErrorMessage ="Please input Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please input Price")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Please Input Discount")]
        public decimal Discount { get; set; }
        public string MainImage { get; set; }
        public int CategoryId { get; set; }
    }
}
