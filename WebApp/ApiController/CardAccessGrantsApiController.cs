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
    public class CardAccessGrantsApiController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper; // Added IMapper

        public CardAccessGrantsApiController(AppDbContext context, IMapper mapper) // Injected IMapper
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/CardAccessGrantsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CardAccessGrantDto>>> GetCardAccessGrants()
        {
            var grants = await _context.CardAccessGrants
                                 .Include(ag => ag.Card)  // Keep includes for mapping
                                 .Include(ag => ag.Device) // Keep includes for mapping
                                 .ToListAsync();
            return Ok(_mapper.Map<IEnumerable<CardAccessGrantDto>>(grants));
        }

        // GET: api/CardAccessGrantsApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CardAccessGrantDto>> GetCardAccessGrant(Guid id)
        {
            var cardAccessGrant = await _context.CardAccessGrants
                                                .Include(ag => ag.Card)
                                                .Include(ag => ag.Device)
                                                .FirstOrDefaultAsync(ag => ag.Id == id);

            if (cardAccessGrant == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CardAccessGrantDto>(cardAccessGrant));
        }

        // GET: api/CardAccessGrantsApi/ByCard/cardId
        [HttpGet("ByCard/{cardId}")]
        public async Task<ActionResult<IEnumerable<CardAccessGrantDto>>> GetCardAccessGrantsByCard(Guid cardId)
        {
            var grants = await _context.CardAccessGrants
                                 .Where(ag => ag.CardId == cardId)
                                 .Include(ag => ag.Device)
                                 .ToListAsync();
            return Ok(_mapper.Map<IEnumerable<CardAccessGrantDto>>(grants));
        }

        // GET: api/CardAccessGrantsApi/ByDevice/deviceId
        [HttpGet("ByDevice/{deviceId}")]
        public async Task<ActionResult<IEnumerable<CardAccessGrantDto>>> GetCardAccessGrantsByDevice(Guid deviceId)
        {
            var grants = await _context.CardAccessGrants
                                 .Where(ag => ag.DeviceId == deviceId)
                                 .Include(ag => ag.Card)
                                 .ToListAsync();
            return Ok(_mapper.Map<IEnumerable<CardAccessGrantDto>>(grants));
        }

        // PUT: api/CardAccessGrantsApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCardAccessGrant(Guid id, UpdateCardAccessGrantDto updateDto)
        {
            var cardAccessGrant = await _context.CardAccessGrants.FindAsync(id);
            if (cardAccessGrant == null)
            {
                return NotFound();
            }

            // Validate CardId and DeviceId exist if they are being changed or for completeness
            if (!await _context.Cards.AnyAsync(c => c.Id == updateDto.CardId))
            {
                return BadRequest(new { message = $"Card with Id {updateDto.CardId} not found." });
            }
            if (!await _context.Devices.AnyAsync(d => d.Id == updateDto.DeviceId))
            {
                return BadRequest(new { message = $"Device with Id {updateDto.DeviceId} not found." });
            }

            _mapper.Map(updateDto, cardAccessGrant);
            _context.Entry(cardAccessGrant).State = EntityState.Modified;

            try
            {
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
            catch (DbUpdateException) // ex variable removed as it was unused, or use _ if you need to inspect it without using its value
            {
                // Check if the exception is due to a unique constraint violation
                // This is database-specific; for SQL Server, it might be error number 2601 or 2627
                // For simplicity, we'll return a generic conflict here.
                // You might want to inspect ex.InnerException for more details.
                if (await _context.CardAccessGrants.AnyAsync(ag => ag.Id != id && ag.CardId == cardAccessGrant.CardId && ag.DeviceId == cardAccessGrant.DeviceId))
                {
                    return Conflict(new { message = "Another access grant with the same CardId and DeviceId already exists." });
                }
                throw; // Re-throw if it's not the expected unique constraint violation
            }

            return NoContent();
        }

        // POST: api/CardAccessGrantsApi
        [HttpPost]
        public async Task<ActionResult<CardAccessGrantDto>> PostCardAccessGrant(CreateCardAccessGrantDto createDto)
        {
            if (!await _context.Cards.AnyAsync(c => c.Id == createDto.CardId))
            {
                return BadRequest(new { message = $"Card with Id {createDto.CardId} not found." });
            }
            if (!await _context.Devices.AnyAsync(d => d.Id == createDto.DeviceId))
            {
                return BadRequest(new { message = $"Device with Id {createDto.DeviceId} not found." });
            }

            if (await _context.CardAccessGrants.AnyAsync(ag => ag.CardId == createDto.CardId && ag.DeviceId == createDto.DeviceId))
            {
                return Conflict(new { message = "This access grant already exists." });
            }

            var cardAccessGrant = _mapper.Map<CardAccessGrant>(createDto);
            cardAccessGrant.Id = Guid.NewGuid();

            _context.CardAccessGrants.Add(cardAccessGrant);
            await _context.SaveChangesAsync();

            var grantDto = _mapper.Map<CardAccessGrantDto>(cardAccessGrant); 
            // It's good practice to fetch the created entity again if it has related data that needs to be populated by the DB or EF Core relations
            // For this DTO, we need Card and Device info which might not be on the 'cardAccessGrant' object immediately after Add/SaveChangesAsync
            // unless explicitly loaded or if the mapping profile handles it (which it should if Include was used in a GetById equivalent).
            // However, for CreatedAtAction, the returned DTO should represent the created resource accurately.
            // Let's refine this by fetching the full grant for the DTO.
            var createdGrantWithDetails = await _context.CardAccessGrants
                                                .Include(ag => ag.Card)
                                                .Include(ag => ag.Device)
                                                .FirstOrDefaultAsync(ag => ag.Id == cardAccessGrant.Id);
            
            grantDto = _mapper.Map<CardAccessGrantDto>(createdGrantWithDetails);

            return CreatedAtAction(nameof(GetCardAccessGrant), new { id = cardAccessGrant.Id }, grantDto);
        }

        // DELETE: api/CardAccessGrantsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCardAccessGrant(Guid id)
        {
            var cardAccessGrant = await _context.CardAccessGrants.FindAsync(id);
            if (cardAccessGrant == null)
            {
                return NotFound();
            }

            _context.CardAccessGrants.Remove(cardAccessGrant);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CardAccessGrantExists(Guid id)
        {
            return _context.CardAccessGrants.Any(e => e.Id == id);
        }
    }
}
