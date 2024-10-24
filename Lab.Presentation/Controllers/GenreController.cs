using Lab.Application.DTOs;
using Lab.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lab.Presentation.Controllers
{
    public class GenreController : Controller
    {
        private readonly IGenreService _genreService;

        // Inject IGenreService thông qua constructor
        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        // GET: /Genre/
        public async Task<IActionResult> Index()
        {
            var genres = await _genreService.GetAllGenresAsync();
            return View(genres);
        }

        // GET: /Genre/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var genre = await _genreService.GetGenreByIdAsync(id);
            if (genre == null)
                return NotFound();

            return View(genre);
        }

        // GET: /Genre/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Genre/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GenreDTO genreDto)
        {
            if (ModelState.IsValid)
            {
                await _genreService.AddGenreAsync(genreDto);
                return RedirectToAction(nameof(Index));
            }
            return View(genreDto);
        }

        // GET: /Genre/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var genre = await _genreService.GetGenreByIdAsync(id);
            if (genre == null)
                return NotFound();

            return View(genre);
        }

        // POST: /Genre/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(GenreDTO genreDto)
        {
            if (ModelState.IsValid)
            {
                await _genreService.UpdateGenreAsync(genreDto);
                return RedirectToAction(nameof(Index));
            }
            return View(genreDto);
        }

        // GET: /Genre/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var genre = await _genreService.GetGenreByIdAsync(id);
            if (genre == null)
                return NotFound();

            return View(genre);
        }

        // POST: /Genre/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _genreService.DeleteGenreAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
