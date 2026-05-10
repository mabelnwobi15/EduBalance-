using EduBalance.Data;
using EduBalance.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EduBalance.Controllers
{
    [Authorize]
    public class SchedulesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SchedulesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(await _context.Schedules.Where(s => s.UserId == userId).ToListAsync());
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Schedule schedule)
        {
            ModelState.Remove("UserId");
            ModelState.Remove("User");

            if (ModelState.IsValid)
            {
                schedule.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                schedule.DateCreated = DateTime.Now;
                _context.Add(schedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(schedule);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var schedule = await _context.Schedules.FirstOrDefaultAsync(s => s.ScheduleId == id && s.UserId == userId);
            if (schedule == null) return NotFound();
            return View(schedule);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Schedule schedule)
        {
            if (id != schedule.ScheduleId) return NotFound();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            ModelState.Remove("UserId");
            ModelState.Remove("User");

            if (ModelState.IsValid)
            {
                schedule.UserId = userId;
                _context.Update(schedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(schedule);
        }
        // GET: Schedules/Details/5
public async Task<IActionResult> Details(int? id)
{
    if (id == null) return NotFound();

    string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

    var schedule = await _context.Schedules
        .FirstOrDefaultAsync(m => m.ScheduleId == id && m.UserId == userId);

    if (schedule == null) return NotFound();

    return View(schedule);
}
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var schedule = await _context.Schedules.FirstOrDefaultAsync(m => m.ScheduleId == id && m.UserId == userId);
            return schedule == null ? NotFound() : View(schedule);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var schedule = await _context.Schedules.FirstOrDefaultAsync(m => m.ScheduleId == id && m.UserId == userId);
            if (schedule != null)
            {
                _context.Schedules.Remove(schedule);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}