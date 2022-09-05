using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.Models;
using System.Collections.Generic;
using System.Linq;

namespace MVC.Controllers
{
    public class OrdersController : Controller
    {
        private readonly MobileContext _context;

        public OrdersController(MobileContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Order> orders = _context.Orders.Include(o=>o.Phone).ToList();
            return View(orders);
        }
        public IActionResult Create(int phoneId)
        {
            Phone phone = _context.Phones.FirstOrDefault(p => p.Id == phoneId);
            return View(new Order { Phone = phone});
        }
        [HttpPost]
        public IActionResult Create(Order order)
        {
            if(order != null)
            {
                _context.Orders.Add(order);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
