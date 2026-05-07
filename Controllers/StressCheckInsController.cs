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

        // GET: Stress
        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var stressLogs = await _context.StressCheckIn
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
            ModelState.Remove("UserId");
            ModelState.Remove("User");

            if (ModelState.IsValid)
            {

                stress.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                stress.DateLogged = DateTime.Now;

                _context.StressCheckIn.Add(stress);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(stress);

        }
        // GET: Stress/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var stress = await _context.StressCheckIn
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

            var stress = await _context.StressCheckIn
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

            var stress = await _context.StressCheckIn
                .FirstOrDefaultAsync(s => s.StressCheckInId == id && s.UserId == userId);

            if (stress != null)
            {
                _context.StressCheckIn.Remove(stress);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}