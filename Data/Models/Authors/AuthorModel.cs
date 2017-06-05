using System;
using System.Collections.Generic;
using System.Text;
using Core;

namespace Data.Models.Authors
{
   public  class AuthorModel : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
