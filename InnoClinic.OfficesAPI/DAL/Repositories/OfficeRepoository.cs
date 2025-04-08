using DAL.Context;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace DAL.Repositories;

public class OfficeRepoository(ApplicationDbContext dbContext) : IOfficeRepository
{
    public async Task<Office> CreateOfficeAsync(Office office, CancellationToken cancellationToken = default)
    {
        await dbContext.Offices.AddAsync(office, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return office;
    }

    public async Task<List<Office>> GetOfficesAsync(CancellationToken cancellationToken = default) =>
        await dbContext.Offices.ToListAsync(cancellationToken);

    public async Task<Office?> GetOfficeByIdAsync(ObjectId id, CancellationToken cancellationToken = default) =>
        await dbContext.Offices.FindAsync(id, cancellationToken);

    public async Task<Office> UpdateOfficeAsync(Office office, CancellationToken cancellationToken = default)
    {
        dbContext.Offices.Update(office);
        await dbContext.SaveChangesAsync(cancellationToken);
        return office;
    } 
    
    public async Task DeleteOfficeAsync(Office office, CancellationToken cancellationToken = default)
    {
        dbContext.Offices.Remove(office);
        await dbContext.SaveChangesAsync(cancellationToken);    
    }
}

