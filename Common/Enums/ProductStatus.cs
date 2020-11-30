using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Common.Enums
{
    public enum ProductStatus
    {
        [Display(Name ="New")]
        New=1, 
        [Display(Name ="Discounted")]
        Discounted,
        [Display(Name ="Best Choise")]
        BestChoise,
        [Display(Name ="Super")]
        Super
    }
}
