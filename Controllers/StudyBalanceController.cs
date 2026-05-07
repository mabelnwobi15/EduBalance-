using EduBalance.Data;
using EduBalance.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EduBalance.Controllers
{
    [Authorize]
    public class StudyBalanceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudyBalanceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StudyBalance
        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var balances = await _context.StudyBalances
                .Where(b => b.UserId == userId)
                .OrderByDescending(b => b.DateRecorded)
                .ToListAsync();

            return View(balances);
        }

        // GET: StudyBalance/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StudyBalance/Create
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

        // GET: StudyBalance/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var balance = await _context.StudyBalances
                .FirstOrDefaultAsync(b => b.StudyBalanceId == id && b.UserId == userId);

            if (balance == null)
            {
                return NotFound();
            }

            return View(balance);
        }

        // GET: StudyBalance/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var balance = await _context.StudyBalances
                .FirstOrDefaultAsync(b => b.StudyBalanceId == id && b.UserId == userId);

            if (balance == null)
            {
                return NotFound();
            }

            return View(balance);
        }

        // POST: StudyBalance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var balance = await _context.StudyBalances
                .FirstOrDefaultAsync(b => b.StudyBalanceId == id && b.UserId == userId);

            if (balance != null)
            {
                _context.StudyBalances.Remove(balance);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}