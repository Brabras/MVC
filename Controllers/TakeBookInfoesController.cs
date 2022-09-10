using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC.Models;
using MVC.ViewModels;

namespace MVC.Controllers
{
    public class TakeBookInfoesController : Controller
    {
        private readonly LibContext _context;

        public TakeBookInfoesController(LibContext context)
        {
            _context = context;
        }

        // GET: TakeBookInfoes
        public async Task<IActionResult> Index()
        {
            var libContext = _context.TakeBookInfos.Include(t => t.Book).Include(t => t.Person);
            return View(await libContext.ToListAsync());
        }

        // GET: TakeBookInfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var takeBookInfo = await _context.TakeBookInfos
                .Include(t => t.Book)
                .Include(t => t.Person)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (takeBookInfo == null)
            {
                return NotFound();
            }

            return View(takeBookInfo);
        }

        // GET: TakeBookInfoes/Create
        public async Task<IActionResult> Create(int id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(t => t.Id == id);
            if (book != null)
                return View(book);
            return RedirectToAction("Index");
        }
        public IActionResult ProfileEntering()
        {
            return View();
        }

        public IActionResult Profile(string email)
        {
            ValidationController vc = new ValidationController(_context);
            Person ps = vc.FindPersonByEmail(email);
            if (ps == null)
                ModelState.AddModelError("Name", "Пользователь с таким Email не зарегистрирован");
            else
            {
                var ptbi = new PersonTakeBookInfoesModel
                {
                    Person = ps,
                    TakeBookInfoes = _context.TakeBookInfos.Include(p=>p.Person).Include(t => t.Book).Where(p=>p.PersonId == ps.Id)
                };
                return View(ptbi);
            }
            return View();
        }

        public async Task<IActionResult> Giveback(int id)
        {
            var info = await _context.TakeBookInfos.Include(i=>i.Person).Include(i=>i.Book).FirstOrDefaultAsync(i => i.Id == id);
            var actualBook = info.Book;
            actualBook.IsGiven = false;
            ValidationController vc = new ValidationController(_context);
            info.TakeBackDate = DateTime.Now;
            info.GiveDate = vc.FindDateById(id);
            _context.Update(info);
            _context.Update(actualBook);
            await _context.SaveChangesAsync();
            return RedirectToAction("Profile", new {info.Person.Email});
        }

        // POST: TakeBookInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int bookId, string email)
        {
            ValidationController vc = new ValidationController(_context);
            Person ps = vc.FindPersonByEmail(email);
            if (ps == null)
                ModelState.AddModelError("Name", "Пользователь с таким Email не зарегистрирован");
            else if (_context.TakeBookInfos.Where(t => t.PersonId == ps.Id).Where(t=>t.TakeBackDate==null).Count() > 2)
                    ModelState.AddModelError("Name", "Вы уже взяли максимальное количество книг," +
                        "\nдочитайте, верните, тогда получите другую.");
                else if (ModelState.IsValid)
                {
                    var actualBook = await _context.Books.FirstOrDefaultAsync(b => b.Id == bookId);
                    if (actualBook.IsGiven)
                    {
                        ModelState.AddModelError("Name", "Эту книгу уже взяли, возьмите другую");
                    }
                    else
                    {
                        actualBook.IsGiven = true;
                        TakeBookInfo info = new TakeBookInfo
                        {
                            GiveDate = DateTime.Now,
                            TakeBackDate = null,
                            PersonId = ps.Id,
                            BookId = bookId
                        };
                        _context.Update(actualBook);
                        _context.Add(info);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
            return View(await _context.Books.FirstOrDefaultAsync(t => t.Id == bookId));
        }

        // GET: TakeBookInfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var takeBookInfo = await _context.TakeBookInfos.FindAsync(id);
            if (takeBookInfo == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "About", takeBookInfo.BookId);
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "Email", takeBookInfo.PersonId);
            return View(takeBookInfo);
        }

        // POST: TakeBookInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GiveDate,TakeBackDate,PersonId,BookId")] TakeBookInfo takeBookInfo)
        {
            if (id != takeBookInfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(takeBookInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TakeBookInfoExists(takeBookInfo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "About", takeBookInfo.BookId);
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "Email", takeBookInfo.PersonId);
            return View(takeBookInfo);
        }

        // GET: TakeBookInfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var takeBookInfo = await _context.TakeBookInfos
                .Include(t => t.Book)
                .Include(t => t.Person)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (takeBookInfo == null)
            {
                return NotFound();
            }

            return View(takeBookInfo);
        }

        // POST: TakeBookInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var takeBookInfo = await _context.TakeBookInfos.FindAsync(id);
            _context.TakeBookInfos.Remove(takeBookInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TakeBookInfoExists(int id)
        {
            return _context.TakeBookInfos.Any(e => e.Id == id);
        }
    }
}
