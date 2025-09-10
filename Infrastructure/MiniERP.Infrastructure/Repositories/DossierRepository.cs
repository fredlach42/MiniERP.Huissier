using Microsoft.EntityFrameworkCore;
using MiniERP.Application.Common.Interfaces;
using MiniERP.Domain.Entities;
using MiniERP.Domain.Enums;
using MiniERP.Infrastructure.Data;

namespace MiniERP.Infrastructure.Repositories;

public class DossierRepository : IDossierRepository
{
    private readonly HuissierDbContext _context;

    public DossierRepository(HuissierDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Dossier>> GetAllAsync()
    {
        return await _context.Dossiers
            .OrderBy(d => d.DateCreation)
            .ToListAsync();
    }

    public async Task<Dossier?> GetByIdAsync(int id)
    {
        return await _context.Dossiers
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<Dossier> CreateAsync(Dossier dossier)
    {
        _context.Dossiers.Add(dossier);
        await _context.SaveChangesAsync();
        return dossier;
    }

    public async Task<Dossier> UpdateAsync(Dossier dossier)
    {
        _context.Dossiers.Update(dossier);
        await _context.SaveChangesAsync();
        return dossier;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var dossier = await _context.Dossiers.FindAsync(id);
        if (dossier == null)
            return false;

        _context.Dossiers.Remove(dossier);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Dossier>> GetByStatusAsync(StatutDossier statut)
    {
        return await _context.Dossiers
            .Where(d => d.Statut == statut)
            .OrderBy(d => d.DateCreation)
            .ToListAsync();
    }
}