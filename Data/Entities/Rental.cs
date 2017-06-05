using System;
using System.Collections.Generic;
using Core;

namespace Data.Entities
{
    public partial class Rental : BaseEntity
    {
 
        public int UserId { get; set; }
        public int BookId { get; set; }
        public DateTime ReserveDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public bool Returned { get; set; }
    }
}
