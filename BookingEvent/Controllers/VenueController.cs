using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookingEvent.Models;
using BookingEvent.Services;

namespace BookingEvent.Controllers
{
    public class VenueController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly BlobService _blobService;

        public VenueController(ApplicationDbContext context, BlobService blobService)
        {
            _context = context;
            _blobService = blobService;
        }

        // INDEX
        public async Task<IActionResult> Index()
        {
            var venues = await _context.Venues.ToListAsync();
            return View(venues);
        }

        // DETAILS
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var venue = await _context.Venues
                .FirstOrDefaultAsync(v => v.VenueId == id);

            if (venue == null) return NotFound();

            return View(venue);
        }

        // CREATE (GET)
        public IActionResult Create()
        {
            return View();
        }

        // CREATE (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Venue venue, IFormFile? ImageFile)
        {
            if (ImageFile == null)
            {
                ModelState.AddModelError("", "Please upload a venue image.");
            }

            if (ModelState.IsValid)
            {
                string imageUrl = await _blobService.UploadFileAsync(ImageFile!);
                venue.ImageUrl = imageUrl;

                _context.Venues.Add(venue);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(venue);
        }

        // EDIT (GET)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var venue = await _context.Venues.FindAsync(id);

            if (venue == null) return NotFound();

            return View(venue);
        }

        // EDIT (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Venue venue, IFormFile? ImageFile)
        {
            if (id != venue.VenueId) return NotFound();

            var existingVenue = await _context.Venues
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.VenueId == id);

            if (existingVenue == null) return NotFound();

            if (ModelState.IsValid)
            {
                if (ImageFile != null)
                {
                    string imageUrl = await _blobService.UploadFileAsync(ImageFile);
                    venue.ImageUrl = imageUrl;
                }
                else
                {
                    venue.ImageUrl = existingVenue.ImageUrl;
                }

                _context.Update(venue);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            venue.ImageUrl = existingVenue.ImageUrl;
            return View(venue);
        }

        // DELETE (GET)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var venue = await _context.Venues
                .FirstOrDefaultAsync(v => v.VenueId == id);

            if (venue == null) return NotFound();

            return View(venue);
        }

        // DELETE (POST)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool hasBookings = await _context.Bookings
                .AnyAsync(b => b.VenueId == id);

            if (hasBookings)
            {
                TempData["Error"] = "This venue cannot be deleted because it has active bookings.";
                return RedirectToAction(nameof(Index));
            }

            var venue = await _context.Venues.FindAsync(id);

            if (venue != null)
            {
                _context.Venues.Remove(venue);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}