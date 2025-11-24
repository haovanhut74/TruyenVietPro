using TruyenHayPro.Core.Entities.Base;

namespace TruyenHayPro.Core.Entities;

public class NovelCategory : AuditedEntity
{
    public Guid NovelId { get; set; }
    public Novel Novel { get; set; }

    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
}