namespace MiniERP.Domain.Common;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime DateCreation { get; set; }
}