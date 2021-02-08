using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.ViewModels
{
   public class VariantViewModel
    {
        public int Id { get; set; }
        [Required (ErrorMessage ="Please Input Variant Name")]
        public string Name { get; set; }
    }
}
