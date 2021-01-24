using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Common.ViewModels
{
   public class VendorAddViewModel
    {
        [Required(ErrorMessage =("Please input Name"))]
        public string Name { get; set; }
        public string Image { get; set; }
    }
}
