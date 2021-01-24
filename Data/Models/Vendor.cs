using System;
using System.Collections.Generic;
using System.Text;
using Data.Models.Interfaces;

namespace Data.Models
{
    public class Vendor : IModelBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public ICollection<Product> Products { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
