using System;
using System.Collections.Generic;
using System.Text;
using Core;
using Data.Models.Authors;
using Data.Models.Categories;

namespace Data.Models.Books
{
    public class BookListModel : BaseEntity
    {
        public BookListModel()
        {
            CategoryModel = new CategoryModel();
            AuthorModel = new AuthorModel();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public int Hardcover { get; set; }
        public string Publisher { get; set; }
        public string Language { get; set; }
        public int Stock { get; set; }
        public string Isbn { get; set; }
        public string ProductDimensions { get; set; }
        public bool Published { get; set; }
        public DateTime CreatedDate { get; set; }

        public CategoryModel CategoryModel { get; set; }
        public AuthorModel AuthorModel { get; set; }
    }
}
