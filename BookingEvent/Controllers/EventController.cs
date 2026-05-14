using BookingEvent.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookingEvent.Models;

namespace BookingEvent.Controllers
{
    public class EventController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly BlobService _blobService;

        public EventController(ApplicationDbContext context, BlobService blobService)
        {
            _context = context;
            _blobService = blobService;
        }

        // =========================
        // INDEX - SHOW ALL EVENTS
        // =========================
        public async Task<IActionResult> Index()
        {
            var events = await _context.Events.ToListAsync();
            return View(events);
        }

        // =========================
        // DETAILS
        // =========================
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var eventModel = await _context.Events
                .FirstOrDefaultAsync(e => e.EventId == id);

            if (eventModel == null) return NotFound();

            return View(eventModel);
        }

        // =========================
        // CREATE (GET)
        // =========================
        public IActionResult Create()
        {
            return View();
        }

        // =========================
        // CREATE (POST)
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Event eventModel, IFormFile ImageFile)
        {
            if (ImageFile == null)
            {
                ModelState.AddModelError("", "Please upload an event image.");
            }

            if (ModelState.IsValid)
            {
                string imageUrl = await _blobService.UploadFileAsync(ImageFile);
                eventModel.ImageUrl = imageUrl;

                _context.Events.Add(eventModel);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(eventModel);
        }

        // =========================
        // EDIT (GET)
        // =========================
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var eventModel = await _context.Events.FindAsync(id);

            if (eventModel == null) return NotFound();

            return View(eventModel);
        }

        // =========================
        // EDIT (POST)
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Event eventModel, IFormFile? ImageFile)
        {
            if (id != eventModel.EventId)
                return NotFound();

            if (ModelState.IsValid)
            {
                var existingEvent = await _context.Events
                    .AsNoTracking()
                    .FirstOrDefaultAsync(e => e.EventId == id);

                if (existingEvent == null)
                    return NotFound();

                if (ImageFile != null)
                {
                    string imageUrl = await _blobService.UploadFileAsync(ImageFile);
                    eventModel.ImageUrl = imageUrl;
                }
                else
                {
                    eventModel.ImageUrl = existingEvent.ImageUrl;
                }

                _context.Update(eventModel);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(eventModel);
        }

        // =========================
        // DELETE (GET)
        // =========================
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var eventModel = await _context.Events
                .FirstOrDefaultAsync(e => e.EventId == id);

            if (eventModel == null) return NotFound();

            return View(eventModel);
        }

        // =========================
        // DELETE (POST)
        // =========================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool hasBookings = await _context.Bookings
                .AnyAsync(b => b.EventId == id);

            if (hasBookings)
            {
                TempData["Error"] = "This event cannot be deleted because it has active bookings.";
                return RedirectToAction(nameof(Index));
            }

            var eventModel = await _context.Events.FindAsync(id);

            if (eventModel != null)
            {
                _context.Events.Remove(eventModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}