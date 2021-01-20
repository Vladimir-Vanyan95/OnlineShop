using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Common.ViewModels
{
   public class CategoryAddViewModel
    {
        public int Id { get; set; }
        [Required (ErrorMessage ="Name is empty")]
        public string Name { get; set; }
    }
}
