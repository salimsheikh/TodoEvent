using Microsoft.EntityFrameworkCore;
using TodoEvent.Models;
using TodoEvent.Services;

namespace TodoEvent.Seeders
{
    public static class EventSeeder
    {
        public static async Task StartSeeder(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            if (await context.Event.AsNoTracking().AnyAsync())
            {
                return;
            }

            var now = DateTime.UtcNow;

            var events = new List<Event>
            {
                new Event
                {
                    Title = "Team Meeting",
                    Description = "Monthly team sync meeting",
                    Start = DateTime.Today.AddHours(10),
                    End = DateTime.Today.AddHours(11),
                    AllDay = false,
                    CreateAt = now
                },
                new Event
                {
                    Title = "Client Demo",
                    Description = "Product demo for client",
                    Start = DateTime.Today.AddDays(1).AddHours(15),
                    End = DateTime.Today.AddDays(1).AddHours(16),
                    AllDay = false,
                    CreateAt = now
                },
                new Event
                {
                    Title = "Company Holiday",
                    Description = "National holiday",
                    Start = new DateTime(DateTime.Today.Year, 8, 15),
                    End = null,
                    AllDay = true,
                    CreateAt = now
                },
                new Event
                {
                    Title = "Training Day",
                    Description = ".NET & Laravel training session",
                    Start = DateTime.Today.AddDays(5),
                    End = DateTime.Today.AddDays(5),
                    AllDay = true,
                    CreateAt = now
                }
            };

            await context.Event.AddRangeAsync(events);
            await context.SaveChangesAsync();


        }
    }
}
