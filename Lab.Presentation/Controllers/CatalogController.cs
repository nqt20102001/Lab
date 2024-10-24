using Lab.Application.DTOs;
using Lab.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lab.Presentation.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ICatalogService _catalogService;

        // Inject ICatalogService thông qua constructor
        public CatalogController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        // GET: /Catalog/
        public async Task<IActionResult> Index()
        {
            var catalogs = await _catalogService.GetAllCatalogsAsync();
            return View(catalogs);
        }

        // GET: /Catalog/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var catalog = await _catalogService.GetCatalogByIdAsync(id);
            if (catalog == null)
                return NotFound();

            return View(catalog);
        }

        // GET: /Catalog/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Catalog/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CatalogDTO catalogDto)
        {
            if (ModelState.IsValid)
            {
                await _catalogService.AddCatalogAsync(catalogDto);
                return RedirectToAction(nameof(Index));
            }
            return View(catalogDto);
        }

        // GET: /Catalog/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var catalog = await _catalogService.GetCatalogByIdAsync(id);
            if (catalog == null)
                return NotFound();

            return View(catalog);
        }

        // POST: /Catalog/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CatalogDTO catalogDto)
        {
            if (ModelState.IsValid)
            {
                await _catalogService.UpdateCatalogAsync(catalogDto);
                return RedirectToAction(nameof(Index));
            }
            return View(catalogDto);
        }

        // GET: /Catalog/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var catalog = await _catalogService.GetCatalogByIdAsync(id);
            if (catalog == null)
                return NotFound();

            return View(catalog);
        }

        // POST: /Catalog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _catalogService.DeleteCatalogAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
