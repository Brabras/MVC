using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MVC.Models;
using System;
using System.Linq;
using System.Xml.Linq;

namespace MVC.Controllers
{
    public class ValidationController : Controller
    {
        private LibContext _context;
        public  ValidationController(LibContext context)
        {
            _context = context;
        }
        public bool CheckMail(string email)
        {
            return !_context.Persons.Any(p => p.Email == email);
        }
        public bool CheckVategoryName(string name)
        {
            return !_context.Categories.Any(c => c.Name == name);
        }
        public bool CheckBookName(string name)
        {
            return !_context.Books.Any(b => b.Name == name);
        }
        public Person FindPersonByEmail(string email)
        {
            return _context.Persons.FirstOrDefault(p => p.Email == email);
        }
        public DateTime FindDateById(int id)
        {
            return _context.TakeBookInfos.FirstOrDefault(p => p.Id == id).GiveDate;
        }
    }
}
