using System.ComponentModel.DataAnnotations;
using TruyenHayPro.Infrastructure.Entities;
using TruyenHayPro.Infrastructure.Entities.Base;
using TruyenHayPro.Infrastructure.Entities.Product;


public class NovelTag : AuditedEntity
{
    [Required] public Guid NovelId { get; set; }
    public Novel Novel { get; set; }

    [Required] public Guid TagId { get; set; }
    public Tag Tag { get; set; }
}