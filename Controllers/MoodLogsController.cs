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

        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(await _context.MoodLogs
                .Where(m => m.UserId == userId)
                .OrderByDescending(m => m.DateLogged)
                .ToListAsync());
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MoodLog mood)
        {
            ModelState.Remove("UserId");
            ModelState.Remove("User");

            if (ModelState.IsValid)
            {
                mood.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                mood.DateLogged = DateTime.Now;
                _context.Add(mood);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mood);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var moodLog = await _context.MoodLogs.FirstOrDefaultAsync(m => m.MoodLogId == id && m.UserId == userId);
            if (moodLog == null) return NotFound();
            return View(moodLog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MoodLog mood)
        {
            if (id != mood.MoodLogId) return NotFound();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            ModelState.Remove("UserId");
            ModelState.Remove("User");

            if (ModelState.IsValid)
            {
                mood.UserId = userId; // Re-assign for security
                _context.Update(mood);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mood);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var mood = await _context.MoodLogs.FirstOrDefaultAsync(m => m.MoodLogId == id && m.UserId == userId);
            return mood == null ? NotFound() : View(mood);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var mood = await _context.MoodLogs.FirstOrDefaultAsync(m => m.MoodLogId == id && m.UserId == userId);
            if (mood != null)
            {
                _context.MoodLogs.Remove(mood);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}