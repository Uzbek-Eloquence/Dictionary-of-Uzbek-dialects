using DataAccess.Schemas.Auth;
using Dictionary.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public partial class EntityContext
{
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Session> Sessions { get; set; }

    #region Public

    public virtual DbSet<DerivationalAffixes> DerivationalAffixes { get; set; }
    public virtual DbSet<InflectionalAffix> InflectionalAffixes { get; set; }
    public virtual DbSet<Dialect> Dialects { get; set; }
    public virtual DbSet<LiteraryWord> LiteraryWords { get; set; }
    public virtual DbSet<PartOfSpeech> PartOfSpeeches { get; set; }
    public virtual DbSet<TypeOfInflectionalAffix> TypeOfInflectionalAffixes { get; set; }
    public virtual DbSet<DialectalWord> DialectalWords { get; set; }
    public virtual DbSet<DialectalDerivationalAffix> DialectalDerivationalAffixes { get; set; }
    public virtual DbSet<DialectalInflectionalAffix> DialectalInflectionalAffixes { get; set; }
    

    #endregion
}