using DataAccess.Enums;
using Dictionary.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public partial class EntityContext(
    DbContextOptions<EntityContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<DerivationalAffixes>()
            .Property(d=>d.Status)
            .HasDefaultValue(EntityStatus.Active);
        modelBuilder.Entity<DerivationalAffixes>()
            .Property(d=>d.CreatedDate)
            .HasDefaultValue(DateTime.Now);
        modelBuilder.Entity<DerivationalAffixes>()
            .HasIndex(x => x.Title);
        
        modelBuilder.Entity<Dialect>()
            .Property(d=>d.Status)
            .HasDefaultValue(EntityStatus.Active);
        modelBuilder.Entity<Dialect>()
            .Property(d=>d.CreatedDate)
            .HasDefaultValue(DateTime.Now);
        modelBuilder.Entity<Dialect>()
            .HasIndex(x => x.Title);
        
        modelBuilder.Entity<DialectalDerivationalAffix>()
            .Property(d=>d.Status)
            .HasDefaultValue(EntityStatus.Active);
        modelBuilder.Entity<DialectalDerivationalAffix>()
            .Property(d=>d.CreatedDate)
            .HasDefaultValue(DateTime.Now);
        modelBuilder.Entity<DialectalDerivationalAffix>()
            .HasIndex(x => x.Title);
        
        modelBuilder.Entity<DialectalInflectionalAffix>()
            .Property(d=>d.Status)
            .HasDefaultValue(EntityStatus.Active);
        modelBuilder.Entity<DialectalInflectionalAffix>()
            .Property(d=>d.CreatedDate)
            .HasDefaultValue(DateTime.Now);
        modelBuilder.Entity<DialectalInflectionalAffix>()
            .HasIndex(x => x.Title);
        
        modelBuilder.Entity<DialectalWord>()
            .Property(d=>d.Status)
            .HasDefaultValue(EntityStatus.Active);
        modelBuilder.Entity<DialectalWord>()
            .Property(d=>d.CreatedDate)
            .HasDefaultValue(DateTime.Now);
        modelBuilder.Entity<DialectalWord>()
            .HasIndex(x => x.Title);
        
        modelBuilder.Entity<InflectionalAffix>()
            .Property(d=>d.Status)
            .HasDefaultValue(EntityStatus.Active);
        modelBuilder.Entity<InflectionalAffix>()
            .Property(d=>d.CreatedDate)
            .HasDefaultValue(DateTime.Now);
        modelBuilder.Entity<InflectionalAffix>()
            .HasIndex(x => x.Title);
        
        modelBuilder.Entity<LiteraryWord>()
            .Property(d=>d.Status)
            .HasDefaultValue(EntityStatus.Active);
        modelBuilder.Entity<LiteraryWord>()
            .Property(d=>d.CreatedDate)
            .HasDefaultValue(DateTime.Now);
        modelBuilder.Entity<LiteraryWord>()
            .HasIndex(x => x.Title);
        
        modelBuilder.Entity<PartOfSpeech>()
            .Property(d=>d.Status)
            .HasDefaultValue(EntityStatus.Active);
        modelBuilder.Entity<PartOfSpeech>()
            .Property(d=>d.CreatedDate)
            .HasDefaultValue(DateTime.Now);
        modelBuilder.Entity<PartOfSpeech>()
            .HasIndex(x => x.Title);
        
        modelBuilder.Entity<TypeOfInflectionalAffix>()
            .Property(d=>d.Status)
            .HasDefaultValue(EntityStatus.Active);
        modelBuilder.Entity<TypeOfInflectionalAffix>()
            .Property(d=>d.CreatedDate)
            .HasDefaultValue(DateTime.Now);
        modelBuilder.Entity<TypeOfInflectionalAffix>()
            .HasIndex(x => x.Title);
    }

}