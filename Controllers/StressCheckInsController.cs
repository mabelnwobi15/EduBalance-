using EduBalance.Data;
using EduBalance.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EduBalance.Controllers
{
    [Authorize]
    public class StressCheckInsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StressCheckInsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(await _context.StressCheckIn.Where(s => s.UserId == userId).OrderByDescending(s => s.DateLogged).ToListAsync());
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StressCheckIn stress)
        {
            ModelState.Remove("UserId");
            ModelState.Remove("User");

            if (ModelState.IsValid)
            {
                stress.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                stress.DateLogged = DateTime.Now;
                _context.Add(stress);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stress);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var stress = await _context.StressCheckIn.FirstOrDefaultAsync(s => s.StressCheckInId == id && s.UserId == userId);
            if (stress == null) return NotFound();
            return View(stress);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StressCheckIn stress)
        {
            if (id != stress.StressCheckInId) return NotFound();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            ModelState.Remove("UserId");
            ModelState.Remove("User");

            if (ModelState.IsValid)
            {
                stress.UserId = userId;
                _context.Update(stress);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stress);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var stress = await _context.StressCheckIn.FirstOrDefaultAsync(s => s.StressCheckInId == id && s.UserId == userId);
            return stress == null ? NotFound() : View(stress);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var stress = await _context.StressCheckIn.FirstOrDefaultAsync(s => s.StressCheckInId == id && s.UserId == userId);
            if (stress != null)
            {
                _context.StressCheckIn.Remove(stress);
                await _context.SaveChangesAsync();
            }
        return RedirectToAction(nameof(Index));
        }
        // GET: StressCheckins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Get the current student's ID
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Find the log, but only if it belongs to THIS user (Security)
            var stressLog = await _context.StressCheckIn
                .FirstOrDefaultAsync(m => m.StressCheckInId == id && m.UserId == userId);

            if (stressLog == null)
            {
                return NotFound();
            }

            return View(stressLog);
        }
    }
}