using System.ComponentModel.DataAnnotations.Schema;


namespace Dictionary.Domain.Entity;

[Table("dialectal_inflectional_affix", Schema = "public")]
public class DialectalInflectionalAffix : DataAccess.Models.Entity
{
    [Column("Title", TypeName = "varchar(16)")]
    public string Title { get; set; }

    [Column("IsSuffix")]
    public bool IsSuffix { get; set; }

    [Column("InflectionalAffixesId")]
    public long InflectionalAffixesId { get; set; }

    [Column("DialectsId")]
    public long DialectsId { get; set; }

    [ForeignKey(nameof(InflectionalAffixesId))]
    public virtual InflectionalAffix InflectionalAffixes { get; set; }

    [ForeignKey(nameof(DialectsId))]
    public virtual Dialect Dialects { get; set; }
}