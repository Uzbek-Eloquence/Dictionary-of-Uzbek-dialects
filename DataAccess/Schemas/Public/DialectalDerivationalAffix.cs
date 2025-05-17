using System.ComponentModel.DataAnnotations.Schema;


namespace Dictionary.Domain.Entity;

[Table("dialectal_derivational_affix", Schema = "public")]
public class DialectalDerivationalAffix : DataAccess.Models.Entity
{
    [Column("Title", TypeName = "varchar(16)")]
    public string Title { get; set; }

    [Column("IsSuffix")]
    public bool IsSuffix { get; set; }

    [Column("DerivationalAffixesId")]
    public long DerivationalAffixesId { get; set; }

    [Column("DialectsId")]
    public long DialectsId { get; set; }

    [ForeignKey(nameof(DerivationalAffixesId))]
    public virtual DerivationalAffixes DerivationalAffixes { get; set; }

    [ForeignKey(nameof(DialectsId))]
    public virtual Dialect Dialects { get; set; }
}