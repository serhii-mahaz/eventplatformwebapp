using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MSA.EventPlatform.DataContext;
using MSA.EventPlatform.Domain.Events;
using MSA.EventPlatform.Domain.Users;
using System;

namespace MSA.EventPlatform.DataSeeder
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var settings = config.Get<DataSeederSettings>();

            var connectionString = config.GetConnectionString("DefaultConnection");
            var recreateDb = settings.DatabaseOptions.RecreateDB;
            var seedData = settings.DatabaseOptions.SeedData;

            Console.WriteLine($"Database options: RecreateDB={recreateDb}, SeedData={seedData}");

            var optionsBuilder = new DbContextOptionsBuilder<EventPlatformDbContext>();
            optionsBuilder.UseNpgsql(connectionString);

            await using var dbContext = new EventPlatformDbContext(optionsBuilder.Options);

            if (recreateDb)
            {
                if (await dbContext.Database.CanConnectAsync())
                {
                    Console.WriteLine("Cleaning existing database...");
                    await dbContext.Database.ExecuteSqlRawAsync("DROP SCHEMA public CASCADE; CREATE SCHEMA public;");
                }

                Console.WriteLine("Creating new database...");
                await dbContext.Database.EnsureCreatedAsync();
            }
            else
            {
                Console.WriteLine("Applying pending migrations...");
                await dbContext.Database.MigrateAsync();
            }

            if (seedData == "fake")
            {
                Console.WriteLine("Seeding fake data...");
                await SeedFakeData(dbContext, settings);
                Console.WriteLine("Data seeding completed!");
            }
            else
            {
                Console.WriteLine("Skipping data seeding.");
            }
        }

        static async Task SeedFakeData(EventPlatformDbContext dbContext, DataSeederSettings settings)
        {
            var fakeUsers = GenerateFakeUsers(settings.GeneratorsOptions.Users);
            await dbContext.Users.AddRangeAsync(fakeUsers);
            List<EventCategory> eventCategories = CreatePredefinedEventCategories();
            await dbContext.EventCategories.AddRangeAsync(eventCategories);
            await dbContext.SaveChangesAsync();

            Console.WriteLine($"Added {fakeUsers.Count} users");
            Console.WriteLine($"Added {eventCategories.Count} event categories");
        }
        
        static List<User> GenerateFakeUsers(int count)
        {
            var faker = new Faker<User>()
                .CustomInstantiator(f => new User { UserId = 0 })
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.UserName, f => f.Internet.UserName())
                .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                .RuleFor(u => u.LastName, f => f.Name.LastName())
                .RuleFor(u => u.AvatarUrl, f => f.Internet.Avatar())
                .RuleFor(u => u.PasswordHash, f => BCrypt.Net.BCrypt.HashPassword("123456"))
                .RuleFor(u => u.CreatedAt, f => DateTime.UtcNow)
                .RuleFor(u => u.UpdatedAt, f => DateTime.UtcNow);

            return faker.Generate(count);
        }

        static List<EventCategory> CreatePredefinedEventCategories()
        {
            return
            [
                new EventCategory { Name = "Concert" },
                new EventCategory { Name = "Conference" },
                new EventCategory { Name = "Workshop" },
                new EventCategory { Name = "Webinar" },
                new EventCategory { Name = "Festival" }
            ];
        }
    }
}
