using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using WebApp.Data;
using WebApp.Models;
using WebApp.Models.Dtos;
using AutoMapper;

namespace WebApp.Controllers
{
    public class DevicePageController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public DevicePageController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: DevicePage
        public async Task<IActionResult> Index()
        {
            var devices = await _context.Devices.ToListAsync();
            var deviceDtos = _mapper.Map<List<DeviceDto>>(devices);
            return View(deviceDtos);
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
            var deviceDto = _mapper.Map<DeviceDto>(device);
            return View(deviceDto);
        }

        // GET: DevicePage/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DevicePage/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateDeviceDto createDeviceDto)
        {
            if (ModelState.IsValid)
            {
                var device = _mapper.Map<Device>(createDeviceDto);
                device.Id = Guid.NewGuid(); // Server generates ID
                _context.Add(device);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(createDeviceDto);
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
            var updateDeviceDto = _mapper.Map<UpdateDeviceDto>(device);
            return View(updateDeviceDto);
        }

        // POST: DevicePage/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UpdateDeviceDto updateDeviceDto)
        {
            var deviceToUpdate = await _context.Devices.FindAsync(id);
            if (deviceToUpdate == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _mapper.Map(updateDeviceDto, deviceToUpdate);
                try
                {
                    _context.Update(deviceToUpdate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeviceExists(id))
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
            return View(updateDeviceDto);
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
            var deviceDto = _mapper.Map<DeviceDto>(device);
            return View(deviceDto);
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
