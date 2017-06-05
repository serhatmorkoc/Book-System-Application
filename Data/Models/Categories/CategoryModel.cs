using System;
using System.Collections.Generic;
using System.Text;
using Core;

namespace Data.Models.Categories
{
    public class CategoryModel : BaseEntity
    {
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
