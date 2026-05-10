using EduBalance.Data;
using EduBalance.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EduBalance.Controllers
{

    public class StressCheckinsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StressCheckinsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Stress
        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var stressLogs = await _context.StressCheckIns
                .Where(s => s.UserId == userId)
                .OrderByDescending(s => s.DateLogged)
                .ToListAsync();

            return View(stressLogs);
        }

        // GET: Stress/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Stress/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StressCheckIn stress)
        {
            if (!ModelState.IsValid)
            {
                return View(stress);
            }

            stress.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            stress.DateLogged = DateTime.Now;

            _context.StressCheckIns.Add(stress);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Stress/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var stress = await _context.StressCheckIns
                .FirstOrDefaultAsync(s => s.StressCheckInId == id && s.UserId == userId);

            if (stress == null)
            {
                return NotFound();
            }

            return View(stress);
        }

        // GET: Stress/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var stress = await _context.StressCheckIns
                .FirstOrDefaultAsync(s => s.StressCheckInId == id && s.UserId == userId);

            if (stress == null)
            {
                return NotFound();
            }

            return View(stress);
        }

        // POST: Stress/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var stress = await _context.StressCheckIns
                .FirstOrDefaultAsync(s => s.StressCheckInId == id && s.UserId == userId);

            if (stress != null)
            {
                _context.StressCheckIns.Remove(stress);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}