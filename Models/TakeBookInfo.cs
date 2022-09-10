using System;

namespace MVC.Models
{
    public class TakeBookInfo
    {
        public int Id { get; set; }
        public DateTime GiveDate { get; set; }
        public DateTime? TakeBackDate { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }

        public int BookId { get; set;}
        public Book Book { get; set; }
    }
}
