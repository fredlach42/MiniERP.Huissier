using MiniERP.Application.DTOs;
using MiniERP.Application.Common.Interfaces;
using MiniERP.Domain.Entities;

namespace MiniERP.Application.UseCases.Dossiers;

public class DossierService : IDossierService
{
    private readonly IDossierRepository _dossierRepository;

    public DossierService(IDossierRepository dossierRepository)
    {
        _dossierRepository = dossierRepository;
    }

    public async Task<IEnumerable<DossierDto>> GetAllDossiersAsync()
    {
        var dossiers = await _dossierRepository.GetAllAsync();
        return dossiers.Select(MapToDto);
    }

    public async Task<DossierDto?> GetDossierByIdAsync(int id)
    {
        var dossier = await _dossierRepository.GetByIdAsync(id);
        return dossier != null ? MapToDto(dossier) : null;
    }

    public async Task<DossierDto> CreateDossierAsync(CreateDossierDto createDto)
    {
        var dossier = new Dossier
        {
            NomDebiteur = createDto.NomDebiteur,
            Montant = createDto.Montant,
            DateAudience = createDto.DateAudience,
            Notes = createDto.Notes,
            DateCreation = DateTime.Now
        };

        var createdDossier = await _dossierRepository.CreateAsync(dossier);
        return MapToDto(createdDossier);
    }

    public async Task<DossierDto> UpdateDossierAsync(UpdateDossierDto updateDto)
    {
        var dossier = await _dossierRepository.GetByIdAsync(updateDto.Id);
        if (dossier == null)
            throw new ArgumentException($"Dossier avec ID {updateDto.Id} introuvable.");

        dossier.NomDebiteur = updateDto.NomDebiteur;
        dossier.Montant = updateDto.Montant;
        dossier.DateAudience = updateDto.DateAudience;
        dossier.Notes = updateDto.Notes;

        var updatedDossier = await _dossierRepository.UpdateAsync(dossier);
        return MapToDto(updatedDossier);
    }

    public async Task<bool> DeleteDossierAsync(int id)
    {
        return await _dossierRepository.DeleteAsync(id);
    }

    public async Task<DossierDto> CloturerDossierAsync(int id)
    {
        var dossier = await _dossierRepository.GetByIdAsync(id);
        if (dossier == null)
            throw new ArgumentException($"Dossier avec ID {id} introuvable.");

        dossier.Clôturer();

        var updatedDossier = await _dossierRepository.UpdateAsync(dossier);
        return MapToDto(updatedDossier);
    }

    public async Task<DossierDto> SupendreDossierAsync(int id)
    {
        var dossier = await _dossierRepository.GetByIdAsync(id);
        if (dossier == null)
            throw new ArgumentException($"Dossier avec ID {id} introuvable.");

        dossier.Suspendre();

        var updatedDossier = await _dossierRepository.UpdateAsync(dossier);
        return MapToDto(updatedDossier);
    }

    public async Task<DossierDto> RemettreEnCoursAsync(int id)
    {
        var dossier = await _dossierRepository.GetByIdAsync(id);
        if (dossier == null)
            throw new ArgumentException($"Dossier avec ID {id} introuvable.");

        dossier.RemettrEnCours();

        var updatedDossier = await _dossierRepository.UpdateAsync(dossier);
        return MapToDto(updatedDossier);
    }

    private static DossierDto MapToDto(Dossier dossier)
    {
        return new DossierDto
        {
            Id = dossier.Id,
            NomDebiteur = dossier.NomDebiteur,
            Montant = dossier.Montant,
            DateAudience = dossier.DateAudience,
            Statut = dossier.Statut,
            DateCreation = dossier.DateCreation,
            DateCloture = dossier.DateCloture,
            Notes = dossier.Notes
        };
    }
}