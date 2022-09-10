using MVC.Models;
using System.Collections.Generic;

namespace MVC.ViewModels
{
    public class PersonTakeBookInfoesModel
    {
        public Person Person { get; set; }
        public IEnumerable<TakeBookInfo> TakeBookInfoes { get; set; }
    }
}
