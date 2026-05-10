using EduBalance.Data;
using EduBalance.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EduBalance.Controllers
{
    [Authorize]
    public class StudyBalancesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudyBalancesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(await _context.StudyBalances.Where(b => b.UserId == userId).OrderByDescending(b => b.DateRecorded).ToListAsync());
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudyBalance balance)
        {
            ModelState.Remove("UserId");
            ModelState.Remove("User");

            if (ModelState.IsValid)
            {
                balance.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                balance.DateRecorded = DateTime.Now;
                _context.Add(balance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(balance);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var balance = await _context.StudyBalances.FirstOrDefaultAsync(b => b.StudyBalanceId == id && b.UserId == userId);
            if (balance == null) return NotFound();
            return View(balance);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StudyBalance balance)
        {
            if (id != balance.StudyBalanceId) return NotFound();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            ModelState.Remove("UserId");
            ModelState.Remove("User");

            if (ModelState.IsValid)
            {
                balance.UserId = userId;
                _context.Update(balance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(balance);
        }
        // GET: StudyBalances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var balance = await _context.StudyBalances
                .FirstOrDefaultAsync(m => m.StudyBalanceId == id && m.UserId == userId);

            if (balance == null) return NotFound();

            return View(balance);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var balance = await _context.StudyBalances.FirstOrDefaultAsync(b => b.StudyBalanceId == id && b.UserId == userId);
            return balance == null ? NotFound() : View(balance);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var balance = await _context.StudyBalances.FirstOrDefaultAsync(b => b.StudyBalanceId == id && b.UserId == userId);
            if (balance != null)
            {
                _context.StudyBalances.Remove(balance);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}