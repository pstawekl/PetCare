using Microsoft.EntityFrameworkCore;
using PetCare;
using System.Collections.Generic;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Pet> Pets { get; set; }
    public DbSet<Visit> Visits { get; set; }
    public DbSet<Reminder> Reminders { get; set; }
    public DbSet<User> Users { get; set; }

}
