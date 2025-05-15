using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class CardPageController : Controller
    {
        private readonly AppDbContext _context;

        public CardPageController(AppDbContext context)
        {
            _context = context;
        }

        // GET: CardPage
        public async Task<IActionResult> Index()
        {
            var cards = await _context.Cards.ToListAsync();
            return View(cards);
        }

        // GET: CardPage/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var card = await _context.Cards
                .Include(c => c.AccessGrants)
                .ThenInclude(ag => ag.Device)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        // GET: CardPage/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CardPage/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DisplayName,CardNumber,MemberName,MemberId,IsDisabled,EnabledFrom,EnabledTo,Remark")] Card card)
        {
            if (ModelState.IsValid)
            {
                var toAdd = new Card(
                    id: card.Id,
                    displayName: card.DisplayName,
                    cardNumber: card.CardNumber,
                    memberId: card.MemberId);

                toAdd.SetEnabledDateTime(card.EnabledFrom, card.EnabledTo);

                //card.Id = Guid.NewGuid();
                _context.Add(toAdd);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(card);
        }

        // GET: CardPage/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var card = await _context.Cards.FindAsync(id);
            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        // POST: CardPage/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,DisplayName,CardNumber,MemberName,MemberId,IsDisabled,EnabledFrom,EnabledTo,Remark")] Card card)
        {
            if (id != card.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(card);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CardExists(card.Id))
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
            return View(card);
        }

        // GET: CardPage/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var card = await _context.Cards
                .FirstOrDefaultAsync(m => m.Id == id);
            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        // POST: CardPage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var card = await _context.Cards.FindAsync(id);
            if (card != null)
            {
                _context.Cards.Remove(card);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: CardPage/ManageWhitelist/5
        public async Task<IActionResult> ManageWhitelist(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var card = await _context.Cards
                .Include(c => c.AccessGrants)
                .ThenInclude(ag => ag.Device)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (card == null)
            {
                return NotFound();
            }

            // 獲取所有設備
            var allDevices = await _context.Devices.ToListAsync();

            // 建構視圖模型
            var viewModel = new ManageWhitelistViewModel
            {
                Card = card,
                AllDevices = allDevices,
                SelectedDeviceIds = card.AccessGrants.Select(ag => ag.DeviceId).ToList()
            };

            return View(viewModel);
        }

        // POST: CardPage/ManageWhitelist/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageWhitelist(Guid id, List<Guid> selectedDevices)
        {
            var card = await _context.Cards
                .Include(c => c.AccessGrants)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (card == null)
            {
                return NotFound();
            }

            // 獲取目前卡片的所有授權
            var currentGrants = await _context.CardAccessGrants
                .Where(ag => ag.CardId == id)
                .ToListAsync();

            // 刪除不再被選中的授權
            var grantsToRemove = currentGrants
                .Where(g => !selectedDevices.Contains(g.DeviceId))
                .ToList();

            foreach (var grant in grantsToRemove)
            {
                _context.CardAccessGrants.Remove(grant);
            }

            // 添加新的授權
            foreach (var deviceId in selectedDevices)
            {
                if (!currentGrants.Any(g => g.DeviceId == deviceId))
                {
                    _context.CardAccessGrants.Add(new CardAccessGrant
                    {
                        Id = Guid.NewGuid(),
                        CardId = id,
                        DeviceId = deviceId,
                        Remark = "Added on " + DateTime.Now.ToString("yyyy-MM-dd")
                    });
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = id });
        }

        private bool CardExists(Guid id)
        {
            return _context.Cards.Any(e => e.Id == id);
        }
    }    // 視圖模型
    public class ManageWhitelistViewModel
    {
        public required Card Card { get; set; }
        public required List<Device> AllDevices { get; set; }
        public required List<Guid> SelectedDeviceIds { get; set; }
    }
}
