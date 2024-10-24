using Lab.Application.DTOs;
using Lab.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lab.Presentation.Controllers
{
    public class CartDetailController : Controller
    {
        private readonly ICartDetailService _cartDetailService;

        // Inject ICartDetailService thông qua constructor
        public CartDetailController(ICartDetailService cartDetailService)
        {
            _cartDetailService = cartDetailService;
        }

        // GET: /CartDetail/
        public async Task<IActionResult> Index()
        {
            var cartDetails = await _cartDetailService.GetAllCartDetailsAsync();
            return View(cartDetails);
        }

        // GET: /CartDetail/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var cartDetail = await _cartDetailService.GetCartDetailByIdAsync(id);
            if (cartDetail == null)
                return NotFound();

            return View(cartDetail);
        }

        // GET: /CartDetail/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /CartDetail/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CartDetailDTO cartDetailDto)
        {
            if (ModelState.IsValid)
            {
                await _cartDetailService.AddCartDetailAsync(cartDetailDto);
                return RedirectToAction(nameof(Index));
            }
            return View(cartDetailDto);
        }

        // GET: /CartDetail/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var cartDetail = await _cartDetailService.GetCartDetailByIdAsync(id);
            if (cartDetail == null)
                return NotFound();

            return View(cartDetail);
        }

        // POST: /CartDetail/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CartDetailDTO cartDetailDto)
        {
            if (ModelState.IsValid)
            {
                await _cartDetailService.UpdateCartDetailAsync(cartDetailDto);
                return RedirectToAction(nameof(Index));
            }
            return View(cartDetailDto);
        }

        // GET: /CartDetail/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var cartDetail = await _cartDetailService.GetCartDetailByIdAsync(id);
            if (cartDetail == null)
                return NotFound();

            return View(cartDetail);
        }

        // POST: /CartDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _cartDetailService.DeleteCartDetailAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
