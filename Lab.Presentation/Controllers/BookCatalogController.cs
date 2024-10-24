using Lab.Application.DTOs;
using Lab.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lab.Presentation.Controllers
{
    public class BookCatalogController : Controller
    {
        private readonly IBookCatalogService _bookCatalogService;

        // Inject IBookCatalogService thông qua constructor
        public BookCatalogController(IBookCatalogService bookCatalogService)
        {
            _bookCatalogService = bookCatalogService;
        }

        // GET: /BookCatalog/
        public async Task<IActionResult> Index()
        {
            var bookCatalogs = await _bookCatalogService.GetAllAsync();
            return View(bookCatalogs);
        }

        // GET: /BookCatalog/Details/5/1
        public async Task<IActionResult> Details(int bookId, int catalogId)
        {
            var bookCatalog = await _bookCatalogService.GetByIdAsync(bookId, catalogId);
            if (bookCatalog == null)
                return NotFound();

            return View(bookCatalog);
        }

        // GET: /BookCatalog/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /BookCatalog/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookCatalogDTO bookCatalogDto)
        {
            if (ModelState.IsValid)
            {
                await _bookCatalogService.AddAsync(bookCatalogDto);
                return RedirectToAction(nameof(Index));
            }
            return View(bookCatalogDto);
        }

        // GET: /BookCatalog/Delete/5/1
        public async Task<IActionResult> Delete(int bookId, int catalogId)
        {
            var bookCatalog = await _bookCatalogService.GetByIdAsync(bookId, catalogId);
            if (bookCatalog == null)
                return NotFound();

            return View(bookCatalog);
        }

        // POST: /BookCatalog/Delete/5/1
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int bookId, int catalogId)
        {
            await _bookCatalogService.DeleteAsync(bookId, catalogId);
            return RedirectToAction(nameof(Index));
        }
    }
}
