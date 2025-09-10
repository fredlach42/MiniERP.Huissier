using MiniERP.Domain.Enums;

namespace MiniERP.Application.DTOs;

public class DossierDto
{
    public int Id { get; set; }
    public string NomDebiteur { get; set; } = string.Empty;
    public decimal Montant { get; set; }
    public DateTime DateAudience { get; set; }
    public StatutDossier Statut { get; set; }
    public DateTime DateCreation { get; set; }
    public DateTime? DateCloture { get; set; }
    public string? Notes { get; set; }
}

public class CreateDossierDto
{
    public string NomDebiteur { get; set; } = string.Empty;
    public decimal Montant { get; set; }
    public DateTime DateAudience { get; set; }
    public string? Notes { get; set; }
}

public class UpdateDossierDto
{
    public int Id { get; set; }
    public string NomDebiteur { get; set; } = string.Empty;
    public decimal Montant { get; set; }
    public DateTime DateAudience { get; set; }
    public string? Notes { get; set; }
}