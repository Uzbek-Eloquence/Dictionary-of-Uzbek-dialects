using System.ComponentModel.DataAnnotations.Schema;

namespace Dictionary.Domain.Entity;

[Table("part_of_speech", Schema = "public")]
public class PartOfSpeech : DataAccess.Models.Entity
{
    [Column("Title", TypeName = "varchar(100)")]
    public string Title { get; set; }
}