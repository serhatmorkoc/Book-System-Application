using System;
using System.Collections.Generic;
using Core;

namespace Data.Entities
{
    public partial class Book : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public int Hardcover { get; set; }
        public string Publisher { get; set; }
        public string Language { get; set; }
        public string Isbn { get; set; }
        public string ProductDimensions { get; set; }
        public bool Published { get; set; }
        public int CategoryId { get; set; }
        public int AuthorId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
