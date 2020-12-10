using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Common.Enums;

namespace Common.ViewModels
{
    public class ProductAddViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please input Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please input Price")]
        public double Price { get; set; }
        [Required(ErrorMessage = "Please input Discount")]
        public double Discount { get; set; }
        //[Required(ErrorMessage ="Please select Image")]
        public string MainImage { get; set; }
        public ProductStatus  ProductStatus{ get; set; }
        public int CategoryId { get; set; }
    }
}
