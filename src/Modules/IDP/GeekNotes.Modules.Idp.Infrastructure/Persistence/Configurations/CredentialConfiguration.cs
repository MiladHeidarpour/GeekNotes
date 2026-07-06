using GeekNotes.Modules.Idp.Domain.Credentials;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekNotes.Modules.Idp.Infrastructure.Persistence.Configurations;

internal sealed class CredentialConfiguration
    : IEntityTypeConfiguration<Credential>
{
    public void Configure(EntityTypeBuilder<Credential> builder)
    {
        builder.ToTable("Credentials");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                x => x.Value,
                value => CredentialId.Create(value));

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.OwnsOne(x => x.PasswordHash, hash =>
        {
            hash.Property(x => x.Value)
                .HasColumnName("PasswordHash")
                .HasMaxLength(500)
                .IsRequired();
        });

        builder.Property(x => x.FailedAccessCount);

        builder.Property(x => x.LockoutEndUtc);

        builder.Property(x => x.CreatedOnUtc);

        builder.Property(x => x.UpdatedOnUtc);

        builder.HasIndex(x => x.UserId)
            .IsUnique();
    }
}
