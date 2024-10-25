using Goodelivery.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Goodelivery.DAL;

public sealed class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        Database.EnsureCreated();
    }

    public DbSet<Order> Orders => Set<Order>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>().HasData(
            new Order { Id = Guid.Parse("a6f47f68-1b4a-4f4a-a91d-78db8bb8b36c"), Weight = 2.5, Region = "Центр", Time = DateTime.Parse("2024-10-01 09:00:00") },
            new Order { Id = Guid.Parse("b8b5cde7-8e8c-4f80-b6e7-bb5464c7f9f0"), Weight = 1.0, Region = "Центр", Time = DateTime.Parse("2024-10-01 09:15:30") },
            new Order { Id = Guid.Parse("7b02ccf7-bfa6-4148-b7ae-4e48e9f1b5c8"), Weight = 3.2, Region = "Центр", Time = DateTime.Parse("2024-10-01 09:30:45") },
            new Order { Id = Guid.Parse("e291dcb1-1f85-4874-b56a-f256a1a5e9e1"), Weight = 2.8, Region = "Центр", Time = DateTime.Parse("2024-10-01 09:45:10") },
            new Order { Id = Guid.Parse("d52c38b7-30e1-44c0-9a4c-1f6a94e9d1b8"), Weight = 5.0, Region = "Север", Time = DateTime.Parse("2024-10-01 09:50:15") },
            new Order { Id = Guid.Parse("4e7c4c0b-1e0f-4c81-81e4-d4b6aeefb1d3"), Weight = 1.5, Region = "Север", Time = DateTime.Parse("2024-10-01 10:00:25") },
            new Order { Id = Guid.Parse("7a981f4c-4016-4af2-a542-e48b02db63d5"), Weight = 4.8, Region = "Север", Time = DateTime.Parse("2024-10-01 10:15:35") },
            new Order { Id = Guid.Parse("3e63d2b7-8729-4932-b5f4-16e6e97c6ac1"), Weight = 3.6, Region = "Юг", Time = DateTime.Parse("2024-10-01 10:30:50") },
            new Order { Id = Guid.Parse("e9438026-1e77-4d36-97b3-fd02e94570e6"), Weight = 5.3, Region = "Юг", Time = DateTime.Parse("2024-10-01 10:45:05") },
            new Order { Id = Guid.Parse("41e3f368-df35-41c1-9ec4-9628496dc44d"), Weight = 4.0, Region = "Юг", Time = DateTime.Parse("2024-10-01 11:00:20") },
            new Order { Id = Guid.Parse("20460f69-fbe7-4f3c-88b2-054169c3031d"), Weight = 2.1, Region = "Запад", Time = DateTime.Parse("2024-10-01 11:15:30") },
            new Order { Id = Guid.Parse("a47a1459-5574-47a5-83a6-241d57c36b55"), Weight = 6.3, Region = "Запад", Time = DateTime.Parse("2024-10-01 11:30:40") },
            new Order { Id = Guid.Parse("5f3f1c90-1d8b-43ec-b7a8-8b536917f8ed"), Weight = 5.5, Region = "Запад", Time = DateTime.Parse("2024-10-01 11:45:55") },
            new Order { Id = Guid.Parse("32b7ec48-d20d-41d8-a191-7a73e577a1f4"), Weight = 1.8, Region = "Восток", Time = DateTime.Parse("2024-10-01 12:00:10") },
            new Order { Id = Guid.Parse("ab8b765c-fc08-49af-bf12-5f62929b28e6"), Weight = 3.9, Region = "Восток", Time = DateTime.Parse("2024-10-01 12:15:25") },
            new Order { Id = Guid.Parse("dab870a0-5a3d-4753-9de6-c6ae648ad34f"), Weight = 2.7, Region = "Восток", Time = DateTime.Parse("2024-10-01 12:30:35") },
            new Order { Id = Guid.Parse("9e409572-e2a1-4c89-b0c0-3d8c5c5d1d48"), Weight = 3.5, Region = "Восток", Time = DateTime.Parse("2024-10-01 12:45:50") },
            new Order { Id = Guid.Parse("6d2f04c2-b128-41aa-8db6-1d2d8f740d41"), Weight = 2.4, Region = "Юго-Восток", Time = DateTime.Parse("2024-10-01 13:00:05") },
            new Order { Id = Guid.Parse("17efcdb1-fb8e-42bc-911b-7f9e6eab8dcb"), Weight = 2.9, Region = "Юго-Восток", Time = DateTime.Parse("2024-10-01 13:15:15") },
            new Order { Id = Guid.Parse("b1e92646-607f-4e1c-9dbb-29884f0b9676"), Weight = 4.1, Region = "Северо-Запад", Time = DateTime.Parse("2024-10-01 13:30:30") },
            new Order { Id = Guid.Parse("69ed6739-3031-41d6-b388-bb6e5a43ab43"), Weight = 3.0, Region = "Северо-Запад", Time = DateTime.Parse("2024-10-01 13:45:45") }
        );

        base.OnModelCreating(modelBuilder);
    }
}