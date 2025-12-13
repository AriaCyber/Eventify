using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Eventify.Data;
using Eventify.Models;
using Eventify.DTOs.Admin;

namespace Eventify.Controllers
{
    [ApiController]
    [Route("api/admin/promocodes")]
    public class PromoCodeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PromoCodeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/admin/promocodes
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var promoCodes = await _context.Promocodes
                .Select(p => new PromoCodeAdminDto
                {
                    Id = p.Id,
                    Code = p.Code,
                    DiscountValue = p.DiscountValue,
                    IsPercentage = p.IsPercentage,
                    ExpiryDate = p.ExpiryDate,
                    UsageLimit = p.UsageLimit,
                    Uses = p.Uses
                })
                .ToListAsync();

            return Ok(promoCodes);
        }

        // POST: api/admin/promocodes
        [HttpPost]
        public async Task<IActionResult> Create(PromoCodeAdminDto dto)
        {
            var promo = new PromoCode
            {
                Code = dto.Code,
                DiscountValue = dto.DiscountValue,
                IsPercentage = dto.IsPercentage,
                ExpiryDate = dto.ExpiryDate,
                UsageLimit = dto.UsageLimit,
                Uses = 0
            };

            _context.Promocodes.Add(promo);
            await _context.SaveChangesAsync();

            return Ok(promo);
        }

        // PUT: api/admin/promocodes/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PromoCodeAdminDto dto)
        {
            var promo = await _context.Promocodes.FindAsync(id);
            if (promo == null)
                return NotFound();

            promo.Code = dto.Code;
            promo.DiscountValue = dto.DiscountValue;
            promo.IsPercentage = dto.IsPercentage;
            promo.ExpiryDate = dto.ExpiryDate;
            promo.UsageLimit = dto.UsageLimit;

            await _context.SaveChangesAsync();
            return Ok(promo);
        }

        // DELETE: api/admin/promocodes/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var promo = await _context.Promocodes.FindAsync(id);
            if (promo == null)
                return NotFound();

            _context.Promocodes.Remove(promo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
