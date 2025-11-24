using System.ComponentModel.DataAnnotations;
using TruyenHayPro.Core.Entities.Base;

namespace TruyenHayPro.Core.Entities;

public class NovelTag : AuditedEntity
{
    [Required] public Guid NovelId { get; set; }
    public Novel Novel { get; set; }

    [Required] public Guid TagId { get; set; }
    public Tag Tag { get; set; }
}