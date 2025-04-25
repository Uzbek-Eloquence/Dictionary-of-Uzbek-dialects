using System.ComponentModel.DataAnnotations.Schema;

namespace Dictionary.Domain.Entity;

[Table("inflectional_affix", Schema = "public")]
public class InflectionalAffix : DataAccess.Models.Entity
{
    [Column("Title", TypeName = "varchar(16)")]
    public string Title { get; set; }

    [Column("IsSuffix")]
    public bool IsSuffix { get; set; }

    [Column("FirstPartOfSpeachId")]
    public long FirstPartOfSpeachId { get; set; }

    [Column("TypesOfInflectionalAffixesId")]
    public long TypesOfInflectionalAffixesId { get; set; }

    [ForeignKey(nameof(FirstPartOfSpeachId))]
    public virtual PartOfSpeech FirstPartOfSpeach { get; set; }

    [ForeignKey(nameof(TypesOfInflectionalAffixesId))]
    public virtual TypeOfInflectionalAffix TypesOfInflectionalAffixes { get; set; }
}