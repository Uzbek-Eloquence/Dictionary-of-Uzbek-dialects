using System.ComponentModel.DataAnnotations.Schema;

namespace Dictionary.Domain.Entity;
[Table("dialect", Schema = "public")]
public class Dialect : DataAccess.Models.Entity
{
    [Column("Title", TypeName = "varchar(100)")]
    public string Title { get; set; }
}