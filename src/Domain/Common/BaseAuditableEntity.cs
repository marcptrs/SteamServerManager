namespace SteamServerManager.Domain.Common;

public class BaseAuditableEntity : BaseEntity
{
    public string? CreatedBy { get; set; }

    public DateTime CreatedUtc { get; set; }

    public string? LastModifiedBy { get; set; }

    public DateTime LastModifiedUtc { get; set; }
}