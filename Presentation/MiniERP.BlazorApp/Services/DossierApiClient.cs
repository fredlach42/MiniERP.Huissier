using System.Net.Http.Json;
using MiniERP.BlazorApp.Models;

namespace MiniERP.BlazorApp.Services;

public class DossierApiClient : IDossierApiClient
{
    private readonly HttpClient _httpClient;

    public DossierApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<DossierDto>> GetAllAsync()
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<List<DossierDto>>("dossiers");
            return response ?? new List<DossierDto>();
        }
        catch (HttpRequestException)
        {
            // En cas d'erreur réseau, retourner liste vide
            return new List<DossierDto>();
        }
    }

    public async Task<DossierDto?> GetByIdAsync(int id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<DossierDto>($"dossiers/{id}");
        }
        catch (HttpRequestException)
        {
            return null;
        }
    }

    public async Task<DossierDto> CreateAsync(CreateDossierDto dto)
    {
        var response = await _httpClient.PostAsJsonAsync("dossiers", dto);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<DossierDto>() 
            ?? throw new InvalidOperationException("Erreur lors de la création");
    }

    public async Task<DossierDto> UpdateAsync(int id, UpdateDossierDto dto)
    {
        var response = await _httpClient.PutAsJsonAsync($"dossiers/{id}", dto);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<DossierDto>() 
            ?? throw new InvalidOperationException("Erreur lors de la mise à jour");
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"dossiers/{id}");
            return response.IsSuccessStatusCode;
        }
        catch (HttpRequestException)
        {
            return false;
        }
    }

    public async Task<DossierDto> CloturerAsync(int id)
    {
        var response = await _httpClient.PostAsync($"dossiers/{id}/cloturer", null);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<DossierDto>() 
            ?? throw new InvalidOperationException("Erreur lors de la clôture");
    }

    public async Task<DossierDto> SuspendreAsync(int id)
    {
        var response = await _httpClient.PostAsync($"dossiers/{id}/suspendre", null);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<DossierDto>() 
            ?? throw new InvalidOperationException("Erreur lors de la suspension");
    }

    public async Task<DossierDto> RemettreEnCoursAsync(int id)
    {
        var response = await _httpClient.PostAsync($"dossiers/{id}/remettre-en-cours", null);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<DossierDto>() 
            ?? throw new InvalidOperationException("Erreur lors de la remise en cours");
    }
}