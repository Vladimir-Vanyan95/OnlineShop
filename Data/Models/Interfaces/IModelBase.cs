using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models.Interfaces
{
    public interface IModelBase
    {
        DateTime CreatedDate { get; set; }
        DateTime UpdatedDate { get; set; }
        bool IsDeleted { get; set; }
    }
}
