using System;
using System.Collections.Generic;
using System.Text;

namespace Common.ViewModels
{
  public  class ProductAddViewModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int CategoryId { get; set; }
    }
}
