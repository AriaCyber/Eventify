using Eventify.Data;
using Microsoft.EntityFrameworkCore;

namespace EventifyTests
{
    public static class TestDbHelper
    {
        public static AppDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var context = new AppDbContext(options);
            return context;
        }
    }
}
