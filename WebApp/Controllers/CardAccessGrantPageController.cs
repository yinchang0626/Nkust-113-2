using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class CardAccessGrantPageController : Controller
    {
        private readonly AppDbContext _context;

        public CardAccessGrantPageController(AppDbContext context)
        {
            _context = context;
        }

        // GET: CardAccessGrantPage
        public async Task<IActionResult> Index()
        {
            var cardAccessGrants = await _context.CardAccessGrants
                .Include(c => c.Card)
                .Include(c => c.Device)
                .ToListAsync();
            return View(cardAccessGrants);
        }

        // GET: CardAccessGrantPage/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cardAccessGrant = await _context.CardAccessGrants
                .Include(c => c.Card)
                .Include(c => c.Device)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cardAccessGrant == null)
            {
                return NotFound();
            }

            return View(cardAccessGrant);
        }

        // GET: CardAccessGrantPage/Create
        public IActionResult Create()
        {
            ViewData["CardId"] = new SelectList(_context.Cards, "Id", "DisplayName");
            ViewData["DeviceId"] = new SelectList(_context.Devices, "Id", "DisplayName");
            return View();
        }

        // POST: CardAccessGrantPage/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CardId,DeviceId,Remark")] CardAccessGrant cardAccessGrant)
        {
            if (ModelState.IsValid)
            {
                cardAccessGrant.Id = Guid.NewGuid();
                _context.Add(cardAccessGrant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CardId"] = new SelectList(_context.Cards, "Id", "DisplayName", cardAccessGrant.CardId);
            ViewData["DeviceId"] = new SelectList(_context.Devices, "Id", "DisplayName", cardAccessGrant.DeviceId);
            return View(cardAccessGrant);
        }

        // GET: CardAccessGrantPage/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cardAccessGrant = await _context.CardAccessGrants.FindAsync(id);
            if (cardAccessGrant == null)
            {
                return NotFound();
            }
            ViewData["CardId"] = new SelectList(_context.Cards, "Id", "DisplayName", cardAccessGrant.CardId);
            ViewData["DeviceId"] = new SelectList(_context.Devices, "Id", "DisplayName", cardAccessGrant.DeviceId);
            return View(cardAccessGrant);
        }

        // POST: CardAccessGrantPage/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,CardId,DeviceId,Remark")] CardAccessGrant cardAccessGrant)
        {
            if (id != cardAccessGrant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cardAccessGrant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CardAccessGrantExists(cardAccessGrant.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CardId"] = new SelectList(_context.Cards, "Id", "DisplayName", cardAccessGrant.CardId);
            ViewData["DeviceId"] = new SelectList(_context.Devices, "Id", "DisplayName", cardAccessGrant.DeviceId);
            return View(cardAccessGrant);
        }

        // GET: CardAccessGrantPage/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cardAccessGrant = await _context.CardAccessGrants
                .Include(c => c.Card)
                .Include(c => c.Device)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cardAccessGrant == null)
            {
                return NotFound();
            }

            return View(cardAccessGrant);
        }

        // POST: CardAccessGrantPage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var cardAccessGrant = await _context.CardAccessGrants.FindAsync(id);
            if (cardAccessGrant != null)
            {
                _context.CardAccessGrants.Remove(cardAccessGrant);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CardAccessGrantExists(Guid id)
        {
            return _context.CardAccessGrants.Any(e => e.Id == id);
        }
    }
}
