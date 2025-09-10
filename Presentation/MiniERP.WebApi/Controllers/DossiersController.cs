using Microsoft.AspNetCore.Mvc;
using MiniERP.Application.DTOs;
using MiniERP.Application.Common.Interfaces;

namespace MiniERP.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DossiersController : ControllerBase
{
    private readonly IDossierService _dossierService;

    public DossiersController(IDossierService dossierService)
    {
        _dossierService = dossierService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DossierDto>>> GetAll()
    {
        var dossiers = await _dossierService.GetAllDossiersAsync();
        return Ok(dossiers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DossierDto>> GetById(int id)
    {
        var dossier = await _dossierService.GetDossierByIdAsync(id);
        if (dossier == null)
            return NotFound($"Dossier avec ID {id} introuvable.");

        return Ok(dossier);
    }

    [HttpPost]
    public async Task<ActionResult<DossierDto>> Create([FromBody] CreateDossierDto createDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var dossier = await _dossierService.CreateDossierAsync(createDto);
        return CreatedAtAction(nameof(GetById), new { id = dossier.Id }, dossier);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<DossierDto>> Update(int id, [FromBody] UpdateDossierDto updateDto)
    {
        if (id != updateDto.Id)
            return BadRequest("L'ID dans l'URL ne correspond pas à l'ID du dossier.");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var dossier = await _dossierService.UpdateDossierAsync(updateDto);
            return Ok(dossier);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var success = await _dossierService.DeleteDossierAsync(id);
        if (!success)
            return NotFound($"Dossier avec ID {id} introuvable.");

        return NoContent();
    }

    [HttpPost("{id}/cloturer")]
    public async Task<ActionResult<DossierDto>> Cloturer(int id)
    {
        try
        {
            var dossier = await _dossierService.CloturerDossierAsync(id);
            return Ok(dossier);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{id}/suspendre")]
    public async Task<ActionResult<DossierDto>> Suspendre(int id)
    {
        try
        {
            var dossier = await _dossierService.SupendreDossierAsync(id);
            return Ok(dossier);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{id}/remettre-en-cours")]
    public async Task<ActionResult<DossierDto>> RemettreEnCours(int id)
    {
        try
        {
            var dossier = await _dossierService.RemettreEnCoursAsync(id);
            return Ok(dossier);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}