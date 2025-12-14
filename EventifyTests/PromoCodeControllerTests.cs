using Eventify.Controllers;
using Eventify.DTOs.Admin;
using Eventify.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace EventifyTests
{
    public class PromoCodeControllerTests
    {
        [Fact]
        public async Task CreatePromoCode_AddsPromo()
        {
            var context = TestDbHelper.GetDbContext();
            var controller = new PromoCodeController(context);

            var dto = new PromoCodeAdminDto
            {
                Code = "SAVE10",
                DiscountValue = 10,
                IsPercentage = true,
                ExpiryDate = DateTime.UtcNow.AddDays(10),
                UsageLimit = 100
            };

            var result = await controller.Create(dto);

            Assert.IsType<OkObjectResult>(result);
            Assert.Single(context.Promocodes);
        }

        [Fact]
        public async Task GetAll_ReturnsPromoCodes()
        {
            var context = TestDbHelper.GetDbContext();
            context.Promocodes.Add(new PromoCode { Code = "TEST", DiscountValue = 5 });
            await context.SaveChangesAsync();

            var controller = new PromoCodeController(context);
            var result = await controller.GetAll();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdatePromoCode_ReturnsNotFound_WhenMissing()
        {
            var context = TestDbHelper.GetDbContext();
            var controller = new PromoCodeController(context);

            var result = await controller.Update(99, new PromoCodeAdminDto());

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeletePromoCode_RemovesPromo()
        {
            var context = TestDbHelper.GetDbContext();
            var promo = new PromoCode { Code = "DEL", DiscountValue = 5 };
            context.Promocodes.Add(promo);
            await context.SaveChangesAsync();

            var controller = new PromoCodeController(context);
            var result = await controller.Delete(promo.Id);

            Assert.IsType<NoContentResult>(result);
            Assert.Empty(context.Promocodes);
        }
    }
}
