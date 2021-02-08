using Data.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Category : IModelBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
        public string Image { get; set; }
        public int? CategoryId { get; set; }
        [ForeignKey ("CategoryId")]
        public Category ParentCategory { get; set; }
        public ICollection<Category> ChildCategories { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
