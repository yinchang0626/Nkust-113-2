using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data;
using WebApp.Models;
using WebApp.Models.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering; // Required for SelectList

namespace WebApp.Controllers
{
    public class CardAccessGrantPageController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CardAccessGrantPageController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: CardAccessGrantPage
        public async Task<IActionResult> Index()
        {
            var cardAccessGrants = await _context.CardAccessGrants
                .Include(c => c.Card)
                .Include(c => c.Device)
                .ToListAsync();
            var cardAccessGrantDtos = _mapper.Map<List<CardAccessGrantDto>>(cardAccessGrants);
            return View(cardAccessGrantDtos);
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
            var cardAccessGrantDto = _mapper.Map<CardAccessGrantDto>(cardAccessGrant);
            return View(cardAccessGrantDto);
        }

        // GET: CardAccessGrantPage/Create
        public async Task<IActionResult> Create() // Made async
        {
            ViewData["CardId"] = new SelectList(_mapper.Map<List<CardBaseDto>>(await _context.Cards.ToListAsync()), "Id", "DisplayName");
            ViewData["DeviceId"] = new SelectList(_mapper.Map<List<DeviceBaseDto>>(await _context.Devices.ToListAsync()), "Id", "DisplayName");
            return View();
        }

        // POST: CardAccessGrantPage/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCardAccessGrantDto createDto)
        {
            if (ModelState.IsValid)
            {
                // Check for duplicate grant before mapping and adding
                if (await _context.CardAccessGrants.AnyAsync(ag => ag.CardId == createDto.CardId && ag.DeviceId == createDto.DeviceId))
                {
                    ModelState.AddModelError(string.Empty, "This access grant already exists.");
                    ViewData["CardId"] = new SelectList(_mapper.Map<List<CardBaseDto>>(await _context.Cards.ToListAsync()), "Id", "DisplayName", createDto.CardId);
                    ViewData["DeviceId"] = new SelectList(_mapper.Map<List<DeviceBaseDto>>(await _context.Devices.ToListAsync()), "Id", "DisplayName", createDto.DeviceId);
                    return View(createDto);
                }

                var cardAccessGrant = _mapper.Map<CardAccessGrant>(createDto);
                cardAccessGrant.Id = Guid.NewGuid();
                _context.Add(cardAccessGrant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CardId"] = new SelectList(_mapper.Map<List<CardBaseDto>>(await _context.Cards.ToListAsync()), "Id", "DisplayName", createDto.CardId);
            ViewData["DeviceId"] = new SelectList(_mapper.Map<List<DeviceBaseDto>>(await _context.Devices.ToListAsync()), "Id", "DisplayName", createDto.DeviceId);
            return View(createDto);
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
            var updateDto = _mapper.Map<UpdateCardAccessGrantDto>(cardAccessGrant);
            ViewData["CardId"] = new SelectList(_mapper.Map<List<CardBaseDto>>(await _context.Cards.ToListAsync()), "Id", "DisplayName", updateDto.CardId);
            ViewData["DeviceId"] = new SelectList(_mapper.Map<List<DeviceBaseDto>>(await _context.Devices.ToListAsync()), "Id", "DisplayName", updateDto.DeviceId);
            return View(updateDto);
        }

        // POST: CardAccessGrantPage/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UpdateCardAccessGrantDto updateDto)
        {
            var cardAccessGrantToUpdate = await _context.CardAccessGrants.FindAsync(id);

            if (cardAccessGrantToUpdate == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Check for duplicate grant if CardId or DeviceId changed
                if ((cardAccessGrantToUpdate.CardId != updateDto.CardId || cardAccessGrantToUpdate.DeviceId != updateDto.DeviceId) &&
                    await _context.CardAccessGrants.AnyAsync(ag => ag.Id != id && ag.CardId == updateDto.CardId && ag.DeviceId == updateDto.DeviceId))
                {
                    ModelState.AddModelError(string.Empty, "Another access grant with the same CardId and DeviceId already exists.");
                    ViewData["CardId"] = new SelectList(_mapper.Map<List<CardBaseDto>>(await _context.Cards.ToListAsync()), "Id", "DisplayName", updateDto.CardId);
                    ViewData["DeviceId"] = new SelectList(_mapper.Map<List<DeviceBaseDto>>(await _context.Devices.ToListAsync()), "Id", "DisplayName", updateDto.DeviceId);
                    return View(updateDto);
                }

                _mapper.Map(updateDto, cardAccessGrantToUpdate);
                try
                {
                    _context.Update(cardAccessGrantToUpdate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CardAccessGrantExists(id))
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
            ViewData["CardId"] = new SelectList(_mapper.Map<List<CardBaseDto>>(await _context.Cards.ToListAsync()), "Id", "DisplayName", updateDto.CardId);
            ViewData["DeviceId"] = new SelectList(_mapper.Map<List<DeviceBaseDto>>(await _context.Devices.ToListAsync()), "Id", "DisplayName", updateDto.DeviceId);
            return View(updateDto);
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
            var cardAccessGrantDto = _mapper.Map<CardAccessGrantDto>(cardAccessGrant);
            return View(cardAccessGrantDto);
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
