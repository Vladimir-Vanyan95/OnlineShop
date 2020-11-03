using Data.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class Cart : IModelBase
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<CartProduct> CartProducts { get; set; }
    }
}
