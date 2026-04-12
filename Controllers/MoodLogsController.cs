using EduBalance.Data;
using EduBalance.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EduBalance.Controllers
{
    [Authorize]
    public class MoodLogsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MoodLogsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MoodLogs
        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var moods = await _context.MoodLogs
                .Where(m => m.UserId == userId)
                .OrderByDescending(m => m.DateLogged)
                .ToListAsync();

            return View(moods);
        }

        // GET: MoodLogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MoodLogs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MoodLog mood)
        {
            if (!ModelState.IsValid)
            {
                return View(mood);
            }

            mood.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            mood.DateLogged = DateTime.Now;

            _context.MoodLogs.Add(mood);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: MoodLogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var mood = await _context.MoodLogs
                .FirstOrDefaultAsync(m => m.MoodLogId == id && m.UserId == userId);

            if (mood == null)
            {
                return NotFound();
            }

            return View(mood);
        }

        // GET: MoodLogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var mood = await _context.MoodLogs
                .FirstOrDefaultAsync(m => m.MoodLogId == id && m.UserId == userId);

            if (mood == null)
            {
                return NotFound();
            }

            return View(mood);
        }

        // POST: MoodLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var mood = await _context.MoodLogs
                .FirstOrDefaultAsync(m => m.MoodLogId == id && m.UserId == userId);

            if (mood != null)
            {
                _context.MoodLogs.Remove(mood);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}