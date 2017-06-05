using System;
using System.Collections.Generic;
using System.Text;
using Core;

namespace Data.Models.Books
{
    public class BookModel : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Hardcover { get; set; }
        public string Publisher { get; set; }
        public int Stock { get; set; }
        public string Language { get; set; }
        public string Isbn { get; set; }
        public string ProductDimensions { get; set; }

    }
}
