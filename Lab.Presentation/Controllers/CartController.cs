using Lab.Application.DTOs;
using Lab.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lab.Presentation.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        // Inject ICartService thông qua constructor
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        // GET: /Cart/
        public async Task<IActionResult> Index()
        {
            var carts = await _cartService.GetAllCartsAsync();
            return View(carts);
        }

        // GET: /Cart/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var cart = await _cartService.GetCartByIdAsync(id);
            if (cart == null)
                return NotFound();

            return View(cart);
        }

        // GET: /Cart/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Cart/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CartDTO cartDto)
        {
            if (ModelState.IsValid)
            {
                await _cartService.AddCartAsync(cartDto);
                return RedirectToAction(nameof(Index));
            }
            return View(cartDto);
        }

        // GET: /Cart/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var cart = await _cartService.GetCartByIdAsync(id);
            if (cart == null)
                return NotFound();

            return View(cart);
        }

        // POST: /Cart/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CartDTO cartDto)
        {
            if (ModelState.IsValid)
            {
                await _cartService.UpdateCartAsync(cartDto);
                return RedirectToAction(nameof(Index));
            }
            return View(cartDto);
        }

        // GET: /Cart/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var cart = await _cartService.GetCartByIdAsync(id);
            if (cart == null)
                return NotFound();

            return View(cart);
        }

        // POST: /Cart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _cartService.DeleteCartAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
