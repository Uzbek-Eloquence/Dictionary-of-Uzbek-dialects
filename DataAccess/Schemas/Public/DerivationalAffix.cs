using System.ComponentModel.DataAnnotations.Schema;

namespace Dictionary.Domain.Entity;

[Table("derivational_affixes", Schema = "public")]
public  class DerivationalAffixes : DataAccess.Models.Entity
{
    [Column("Title", TypeName = "varchar(16)")]
    public string Title { get; set; }

    [Column("IsSuffix")]
    public bool IsSuffix { get; set; }

    [Column("FirstPartOfSpeachId")]
    public long FirstPartOfSpeachId { get; set; }

    [Column("LastPartOfSpeachId")]
    public long LastPartOfSpeechId { get; set; }

    [ForeignKey(nameof(FirstPartOfSpeachId))]
    public virtual PartOfSpeech FirstPartOfSpeach { get; set; }

    [ForeignKey(nameof(LastPartOfSpeechId))]
    public virtual PartOfSpeech LastPartOfSpeech { get; set; }
    
}