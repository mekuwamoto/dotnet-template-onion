using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Onion.Template.Domain.Entities;

namespace Onion.Template.Persistence.Mappings;

public class UserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("TB_USER");
        builder.HasKey(p => p.Id);
        builder
            .Property(p => p.Id)
            .HasColumnName("ID_USER");
        builder
            .Property(p => p.FirstName)
            .HasColumnName("DS_FIRST_NAME");
        builder
            .Property(p => p.LastName)
            .HasColumnName("DS_LAST_NAME");
        builder
            .Property(p => p.Email)
            .HasColumnName("DS_EMAIL");
        builder
            .Property(p => p.Password)
            .HasColumnName("DS_PWD");
    }
}