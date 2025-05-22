using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data;
using WebApp.Models;
using WebApp.Models.Dtos; // Added DTO namespace

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesApiController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper; // Added IMapper

        public DevicesApiController(AppDbContext context, IMapper mapper) // Injected IMapper
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/DevicesApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeviceDto>>> GetDevices()
        {
            var devices = await _context.Devices.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<DeviceDto>>(devices));
        }

        // GET: api/DevicesApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DeviceDto>> GetDevice(Guid id)
        {
            var device = await _context.Devices
                                   .Include(d => d.AccessGrants) // Keep includes for mapping related data
                                   // .ThenInclude(ag => ag.Card) // This might be too much detail for DeviceDto
                                   .FirstOrDefaultAsync(d => d.Id == id);

            if (device == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<DeviceDto>(device));
        }

        // PUT: api/DevicesApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDevice(Guid id, UpdateDeviceDto updateDeviceDto)
        {
            var device = await _context.Devices.FindAsync(id);
            if (device == null)
            {
                return NotFound();
            }

            _mapper.Map(updateDeviceDto, device);
            _context.Entry(device).State = EntityState.Modified;

            try
            {
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

            return NoContent();
        }

        // POST: api/DevicesApi
        [HttpPost]
        public async Task<ActionResult<DeviceDto>> PostDevice(CreateDeviceDto createDeviceDto)
        {
            var device = _mapper.Map<Device>(createDeviceDto);
            device.Id = Guid.NewGuid(); // Ensure new Guid is generated

            _context.Devices.Add(device);
            await _context.SaveChangesAsync();

            var deviceDto = _mapper.Map<DeviceDto>(device);
            return CreatedAtAction(nameof(GetDevice), new { id = device.Id }, deviceDto);
        }

        // DELETE: api/DevicesApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDevice(Guid id)
        {
            var device = await _context.Devices.FindAsync(id);
            if (device == null)
            {
                return NotFound();
            }

            var accessGrants = await _context.CardAccessGrants.Where(ag => ag.DeviceId == id).ToListAsync();
            if (accessGrants.Any())
            {
                _context.CardAccessGrants.RemoveRange(accessGrants);
            }

            _context.Devices.Remove(device);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DeviceExists(Guid id)
        {
            return _context.Devices.Any(e => e.Id == id);
        }
    }
}
