using System.ComponentModel.DataAnnotations.Schema;

namespace Dictionary.Domain.Entity;

[Table("literary_word", Schema = "public")]
public class LiteraryWord : DataAccess.Models.Entity
{
    [Column("Title", TypeName = "varchar(256)")]
    public string Title { get; set; }

    [Column("Description", TypeName = "varchar(1024)")]
    public string Description { get; set; }

    [Column("PartOfSpeechId")]
    public long PartOfSpeechId { get; set; }

    [ForeignKey(nameof(PartOfSpeechId))]
    public virtual PartOfSpeech PartOfSpeech { get; set; }
}