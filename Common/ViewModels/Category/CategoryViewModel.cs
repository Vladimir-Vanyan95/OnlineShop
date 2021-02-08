using System;
using System.Collections.Generic;
using System.Text;
using Common.ViewModels;

namespace Common.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public ICollection<SubCategoryViewModel> SubCategories { get; set; }
    }
}
