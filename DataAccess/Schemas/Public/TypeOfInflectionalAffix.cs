using System.ComponentModel.DataAnnotations.Schema;

namespace Dictionary.Domain.Entity;

[Table("type_of_inflectional_affix", Schema = "public")]
public class TypeOfInflectionalAffix :DataAccess.Models.Entity
{
    [Column("Title", TypeName = "varchar(100)")]
    public string Title { get; set; }
}