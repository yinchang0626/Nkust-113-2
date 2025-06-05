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
    public class CardsApiController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper; // Added IMapper

        public CardsApiController(AppDbContext context, IMapper mapper) // Injected IMapper
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/CardsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CardDto>>> GetCards()
        {
            var cards = await _context.Cards.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<CardDto>>(cards));
        }

        // GET: api/CardsApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CardDto>> GetCard(Guid id)
        {
            var card = await _context.Cards
                                 .Include(c => c.AccessGrants) // Keep includes for mapping related data
                                 // .ThenInclude(ag => ag.Device) // This might be too much detail for CardDto, handled by CardAccessGrantBaseDto
                                 .FirstOrDefaultAsync(c => c.Id == id);

            if (card == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CardDto>(card));
        }

        // PUT: api/CardsApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCard(Guid id, UpdateCardDto updateCardDto)
        {
            var card = await _context.Cards.FindAsync(id);
            if (card == null)
            {
                return NotFound();
            }

            // Apply updates from DTO to entity
            _mapper.Map(updateCardDto, card);

            _context.Entry(card).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardExists(id))
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

        // POST: api/CardsApi
        [HttpPost]
        public async Task<ActionResult<CardDto>> PostCard(CreateCardDto createCardDto)
        {
            var card = _mapper.Map<Card>(createCardDto);
            card.Id = Guid.NewGuid(); // Ensure new Guid is generated
            
            _context.Cards.Add(card);
            await _context.SaveChangesAsync();

            var cardDto = _mapper.Map<CardDto>(card);
            return CreatedAtAction(nameof(GetCard), new { id = card.Id }, cardDto);
        }

        // DELETE: api/CardsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(Guid id)
        {
            var card = await _context.Cards.FindAsync(id);
            if (card == null)
            {
                return NotFound();
            }

            var accessGrants = await _context.CardAccessGrants.Where(ag => ag.CardId == id).ToListAsync();
            if (accessGrants.Any())
            {
                _context.CardAccessGrants.RemoveRange(accessGrants);
            }

            _context.Cards.Remove(card);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CardExists(Guid id)
        {
            return _context.Cards.Any(e => e.Id == id);
        }
    }
}
