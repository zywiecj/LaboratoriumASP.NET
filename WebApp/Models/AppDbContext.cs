using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Models;

public class AppDbContext : IdentityDbContext<IdentityUser> //DbContext 
{
    public DbSet<ContactEntity> Contacts { get; set; }
    public DbSet<OrganizationEntity> Organizations { get; set; }
    private string DbPath { get; set; }
    public AppDbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "contacts.db");
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data source={DbPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        var ADMIN_ID = Guid.NewGuid().ToString();
        var ADMIN_ROLE_ID = Guid.NewGuid().ToString();
        var USER_ID = Guid.NewGuid().ToString();
        var USER_ROLE_ID = Guid.NewGuid().ToString();
        //dodaj role user
        modelBuilder.Entity<IdentityRole>()
            .HasData(
                new IdentityRole()
                {
                    Id = ADMIN_ROLE_ID,
                    Name = "admin",
                    NormalizedName = "ADMIN".ToUpper(),
                    ConcurrencyStamp = ADMIN_ROLE_ID
                },
                new IdentityRole()
                    {
                        Id = USER_ROLE_ID,
                        Name = "user",
                        NormalizedName = "user".ToUpper(),
                        ConcurrencyStamp = USER_ROLE_ID
                    }
            );
        var admin = new IdentityUser()
        {
            Id = ADMIN_ID,
            UserName = "jacobk",
            NormalizedUserName = "jacobk".ToUpper(),
            Email = "jacobk@gmail.com".ToUpper(),
            PhoneNumber = "531555512",
            EmailConfirmed = true,
        };
        //utworz uzytkownika
        
        
        var user = new IdentityUser()
        {
            Id = USER_ID,
            UserName = "mp123",
            NormalizedUserName = "mp123".ToUpper(),
            Email = "mp123@gmail.com",
            PhoneNumber = "12312332",
            EmailConfirmed = true,
        };
        
        PasswordHasher<IdentityUser> hasher = new PasswordHasher<IdentityUser>();
        admin.PasswordHash = hasher.HashPassword(admin, "123@");
        user.PasswordHash = hasher.HashPassword(user, "123@");
        modelBuilder.Entity<IdentityUser>()
            .HasData(admin, user);
        modelBuilder.Entity<IdentityUserRole<string>>()
            .HasData(
                new IdentityUserRole<string>()
                {
                    RoleId = ADMIN_ROLE_ID,
                    UserId = ADMIN_ID
                },
                
                new IdentityUserRole<string>()
                {
                    RoleId = USER_ROLE_ID,
                    UserId = USER_ID
                },
                new IdentityUserRole<string>()
                {
                    RoleId = USER_ROLE_ID,
                    UserId = ADMIN_ID
                }
                );
            
        
        
        
        
        modelBuilder.Entity<OrganizationEntity>()
            .ToTable("Organizations")
            .HasData(
                new OrganizationEntity()
                {
                    Id = 101,
                    NIP = "321312321",
                    Name = "Wsei",
                    REGON = "321312321",
                },
                new OrganizationEntity()
                {
                    Id = 102,
                    NIP = "232332323",
                    Name = "Firma",
                    REGON = "2342342423"
                }
            );
        modelBuilder.Entity<OrganizationEntity>()
            .OwnsOne(o => o.Adress)
            .HasData(
                new {OrganizationEntityId = 101, Street = "Św.Filipa",City = "Kraków"},
                new{OrganizationEntityId = 102, Street = "ŚW Igora", City= "Kraków"}
            );
    modelBuilder.Entity<ContactEntity>()
        .Property(c => c.OrganizationId)
        .HasDefaultValue(101);
    modelBuilder.Entity<ContactEntity>()
            .HasData(new ContactEntity
            {
                Id = 1, 
                FirstName = "John", 
                LastName = "Doe", 
                Email = "john.doe@gmail.com",
                phoneNumber = "123 123 321",
                Birthday = new DateOnly(1980,1,1),
                Created = DateTime.Now,
                OrganizationId = 101,
            },new ContactEntity
            {
                Id = 2, 
                FirstName = "John", 
                LastName = "Doe", 
                Email = "john.doe@gmail.com" ,
                phoneNumber = "123 123 321",
                Birthday = new DateOnly(1980,1,1),
                Created = DateTime.Now,
                OrganizationId = 101,
            });
    }
}