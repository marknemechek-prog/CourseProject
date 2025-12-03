using Microsoft.EntityFrameworkCore;

namespace ServerApp.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Item> Items => Set<Item>();
    public DbSet<UserAction> Actions => Set<UserAction>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<Item>().ToTable("Items");
        modelBuilder.Entity<UserAction>().ToTable("Actions");

        modelBuilder.Entity<Item>()
            .HasOne(i => i.User)
            .WithMany(u => u.Items)
            .HasForeignKey(i => i.UserId);

        modelBuilder.Entity<UserAction>()
            .HasOne(a => a.User)
            .WithMany(u => u.Actions)
            .HasForeignKey(a => a.UserId);

        modelBuilder.Entity<UserAction>()
            .HasOne(a => a.Item)
            .WithMany(i => i.Actions)
            .HasForeignKey(a => a.ItemId);

        // ---- фіксований час для сид-даних ----
        var seedTime = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        // Seed-дані
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Name = "Іван Петров", Email = "ivan.petrov@uni.edu", PasswordHash = "HASH1" },
            new User { Id = 2, Name = "Олена Іваненко", Email = "olena.ivanenko@uni.edu", PasswordHash = "HASH2" }
        );

        modelBuilder.Entity<Item>().HasData(
            new Item { Id = 1, Name = "Програмування 1", Description = "C# базовий курс", Semester = 1, UserId = 1 },
            new Item { Id = 2, Name = "Бази даних", Description = "SQL та БД", Semester = 3, UserId = 1 },
            new Item { Id = 3, Name = "Комп'ютерні мережі", Description = "Основи мереж", Semester = 4, UserId = 2 }
        );

        modelBuilder.Entity<UserAction>().HasData(
            new UserAction { Id = 1, UserId = 1, ItemId = 1, ActionType = "Create", ActionDetails = "Створити курс", Status = "Approved", CreatedAt = seedTime },
            new UserAction { Id = 2, UserId = 1, ItemId = 2, ActionType = "Delete", ActionDetails = "Обʼєднати з іншим", Status = "Pending", CreatedAt = seedTime },
            new UserAction { Id = 3, UserId = 2, ItemId = 3, ActionType = "Create", ActionDetails = "Новий курс з мереж", Status = "Rejected", CreatedAt = seedTime }
        );
    }
}
