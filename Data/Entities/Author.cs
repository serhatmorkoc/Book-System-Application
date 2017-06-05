using System;
using System.Collections.Generic;
using Core;

namespace Data.Entities
{
    public partial class Author : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
