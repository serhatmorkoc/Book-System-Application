using System;
using System.Collections.Generic;
using Core;

namespace Data.Entities
{
    public partial class Category : BaseEntity
    {
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
