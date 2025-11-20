using TruyenHayPro.Infrastructure.Entities.Base;
using TruyenHayPro.Infrastructure.Entities.Product;

namespace TruyenHayPro.Infrastructure.Entities;

public class NovelCategory : AuditedEntity
{
    public Guid NovelId { get; set; }
    public Novel Novel { get; set; }

    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
}