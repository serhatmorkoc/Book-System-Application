using System;
using System.Collections.Generic;
using Core;

namespace Data.Entities
{
    public partial class User : BaseEntity
    {
        public string UserGuid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
