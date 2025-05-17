using System.ComponentModel.DataAnnotations.Schema;

namespace Dictionary.Domain.Entity;

[Table("dialectal_word", Schema = "public")]
public class DialectalWord : DataAccess.Models.Entity
{
    [Column("Title", TypeName = "varchar(16)")]
    public string Title { get; set; }

    [Column("LiteraryWordsId")]
    public long LiteraryWordsId { get; set; }

    [Column("DialectsId")]
    public long DialectsId { get; set; }

    [ForeignKey(nameof(LiteraryWordsId))]
    public virtual LiteraryWord LiteraryWords { get; set; }

    [ForeignKey(nameof(DialectsId))]
    public virtual Dialect Dialects { get; set; }
}