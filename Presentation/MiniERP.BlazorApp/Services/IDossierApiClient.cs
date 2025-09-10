using MiniERP.BlazorApp.Models;

namespace MiniERP.BlazorApp.Services;

public interface IDossierApiClient
{
    Task<List<DossierDto>> GetAllAsync();
    Task<DossierDto?> GetByIdAsync(int id);
    Task<DossierDto> CreateAsync(CreateDossierDto dto);
    Task<DossierDto> UpdateAsync(int id, UpdateDossierDto dto);
    Task<bool> DeleteAsync(int id);
    Task<DossierDto> CloturerAsync(int id);
    Task<DossierDto> SuspendreAsync(int id);
    Task<DossierDto> RemettreEnCoursAsync(int id);
}