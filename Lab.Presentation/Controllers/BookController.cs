using Lab.Application.DTOs;
using Lab.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lab.Presentation.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;

        // Inject IBookService thông qua constructor
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // GET: /Book/
        public async Task<IActionResult> Index()
        {
            var books = await _bookService.GetAllBooksAsync();
            return View(books);
        }

        // GET: /Book/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
                return NotFound();

            return View(book);
        }

        // GET: /Book/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Book/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookDTO bookDto)
        {
            if (ModelState.IsValid)
            {
                await _bookService.AddBookAsync(bookDto);
                return RedirectToAction(nameof(Index));
            }
            return View(bookDto);
        }

        // GET: /Book/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
                return NotFound();

            return View(book);
        }

        // POST: /Book/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BookDTO bookDto)
        {
            if (ModelState.IsValid)
            {
                await _bookService.UpdateBookAsync(bookDto);
                return RedirectToAction(nameof(Index));
            }
            return View(bookDto);
        }

        // GET: /Book/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
                return NotFound();

            return View(book);
        }

        // POST: /Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bookService.DeleteBookAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
