using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class DevicePageController : Controller
    {
        private readonly AppDbContext _context;

        public DevicePageController(AppDbContext context)
        {
            _context = context;
        }

        // GET: DevicePage
        public async Task<IActionResult> Index()
        {
            var devices = await _context.Devices.ToListAsync();
            return View(devices);
        }

        // GET: DevicePage/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = await _context.Devices
                .Include(d => d.AccessGrants)
                .ThenInclude(ag => ag.Card)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (device == null)
            {
                return NotFound();
            }

            return View(device);
        }

        // GET: DevicePage/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DevicePage/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DisplayName,DeviceCode,RemoteId,IpAddress")] Device device)
        {
            if (ModelState.IsValid)
            {
                device.Id = Guid.NewGuid();
                _context.Add(device);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(device);
        }

        // GET: DevicePage/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = await _context.Devices.FindAsync(id);
            if (device == null)
            {
                return NotFound();
            }
            
            return View(device);
        }

        // POST: DevicePage/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,DisplayName,DeviceCode,RemoteId,IpAddress")] Device device)
        {
            if (id != device.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(device);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeviceExists(device.Id))
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
            return View(device);
        }

        // GET: DevicePage/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = await _context.Devices
                .FirstOrDefaultAsync(m => m.Id == id);
            if (device == null)
            {
                return NotFound();
            }

            return View(device);
        }

        // POST: DevicePage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var device = await _context.Devices.FindAsync(id);
            if (device != null)
            {
                _context.Devices.Remove(device);
                await _context.SaveChangesAsync();
            }
            
            return RedirectToAction(nameof(Index));
        }

        private bool DeviceExists(Guid id)
        {
            return _context.Devices.Any(e => e.Id == id);
        }
    }
}
