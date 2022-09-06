using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.Models;
using System.Collections.Generic;
using System.Linq;

namespace MVC.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ShopContext _context;

        public OrdersController(ShopContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Order> orders = _context.Orders.Include(o=>o.Product).ToList();
            return View(orders);
        }
        public IActionResult Create(int productId)
        {
            Product product = _context.Product.FirstOrDefault(p => p.Id == productId);
            return View(new Order { Product = product });
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
