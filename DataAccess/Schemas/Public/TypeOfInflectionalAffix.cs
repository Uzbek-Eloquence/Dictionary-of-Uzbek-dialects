using System.ComponentModel.DataAnnotations.Schema;

namespace Dictionary.Domain.Entity;

public class TypeOfInflectionalAffix :DataAccess.Models.Entity
{
    [Column("Title", TypeName = "varchar(100)")]
    public string Title { get; set; }
}