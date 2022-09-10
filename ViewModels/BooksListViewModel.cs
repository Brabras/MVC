using MVC.Models;
using System.Collections;
using System.Collections.Generic;

namespace MVC.ViewModels
{
    public class BooksListViewModel
    {
        public IEnumerable<Book> Books { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
