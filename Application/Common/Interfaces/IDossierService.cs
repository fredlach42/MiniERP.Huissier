using MiniERP.Application.DTOs;

namespace MiniERP.Application.Common.Interfaces;

public interface IDossierService
{
    Task<IEnumerable<DossierDto>> GetAllDossiersAsync();
    Task<DossierDto?> GetDossierByIdAsync(int id);
    Task<DossierDto> CreateDossierAsync(CreateDossierDto createDto);
    Task<DossierDto> UpdateDossierAsync(UpdateDossierDto updateDto);
    Task<bool> DeleteDossierAsync(int id);
    Task<DossierDto> CloturerDossierAsync(int id);
    Task<DossierDto> SupendreDossierAsync(int id);
    Task<DossierDto> RemettreEnCoursAsync(int id);
}