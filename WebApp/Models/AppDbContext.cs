using Microsoft.EntityFrameworkCore;

namespace WebApp.Models;

public class AppDbContext : DbContext
{
    public DbSet<ContactEntity> Contacts { get; set; }
    private string DbPath { get; set; }

    public AppDbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Combine(path, "contacts.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data source={DbPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ContactEntity>()
            .HasData(
                new ContactEntity()
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@gmail.com",
                    phoneNumber = "123123123",
                    Birthday = new DateOnly(year: 2000, month:10, day:10),
                    Created = DateTime.Now
                },
        new ContactEntity()
            {
                Id = 2,
                FirstName = "Jacek",
                LastName = "Mazur",
                Email = "john.mazur@gmail.com",
                phoneNumber = "696123123",
                Birthday = new DateOnly(year: 2000, month:11, day:10),
                Created = DateTime.Now
            }
                );
    }
}