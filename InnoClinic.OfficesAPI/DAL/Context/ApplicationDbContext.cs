using DAL.Configuration;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Context;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public required DbSet<Office> Offices { get; init; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new OfficeConfiguration());
    }
}
