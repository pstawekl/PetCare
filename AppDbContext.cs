using Microsoft.EntityFrameworkCore;
using PetCare;
using System.Collections.Generic;

// Kontekst bazy danych aplikacji
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Pet> Pets { get; set; } // Zbiór zwierząt
    public DbSet<Visit> Visits { get; set; } // Zbiór wizyt
    public DbSet<Reminder> Reminders { get; set; } // Zbiór przypomnień
    public DbSet<User> Users { get; set; } // Zbiór użytkowników
}
