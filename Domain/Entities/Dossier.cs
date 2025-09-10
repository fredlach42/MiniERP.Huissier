using MiniERP.Domain.Common;
using MiniERP.Domain.Enums;

namespace MiniERP.Domain.Entities;

public class Dossier : BaseEntity
{
    public string NomDebiteur { get; set; } = string.Empty;
    public decimal Montant { get; set; }
    public DateTime DateAudience { get; set; }
    public StatutDossier Statut { get; set; } = StatutDossier.Ouvert;
    public DateTime? DateCloture { get; set; }
    public string? Notes { get; set; }

    // Méthodes métier (business logic)
    public bool PeutEtreClôture()
    {
        return Statut == StatutDossier.EnCours;
    }

    public void Clôturer()
    {
        if (!PeutEtreClôture())
            throw new InvalidOperationException("Le dossier ne peut pas être clôturé dans son état actuel.");

        Statut = StatutDossier.Cloture;
        DateCloture = DateTime.Now;
    }

    public void Suspendre()
    {
        if (Statut != StatutDossier.EnCours)
            throw new InvalidOperationException("Seuls les dossiers en cours peuvent être suspendus.");

        Statut = StatutDossier.Suspendu;
    }

    public void RemettrEnCours()
    {
        if (Statut != StatutDossier.Suspendu)
            throw new InvalidOperationException("Seuls les dossiers suspendus peuvent être remis en cours.");

        Statut = StatutDossier.EnCours;
    }
}