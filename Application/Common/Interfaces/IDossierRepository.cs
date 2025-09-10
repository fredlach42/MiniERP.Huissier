using MiniERP.Domain.Entities;

namespace MiniERP.Application.Common.Interfaces;

public interface IDossierRepository
{
    Task<IEnumerable<Dossier>> GetAllAsync();
    Task<Dossier?> GetByIdAsync(int id);
    Task<Dossier> CreateAsync(Dossier dossier);
    Task<Dossier> UpdateAsync(Dossier dossier);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<Dossier>> GetByStatusAsync(Domain.Enums.StatutDossier statut);
}