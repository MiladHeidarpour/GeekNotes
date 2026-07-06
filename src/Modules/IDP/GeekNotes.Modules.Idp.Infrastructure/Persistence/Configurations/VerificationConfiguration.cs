using GeekNotes.Modules.Idp.Domain.Verifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeekNotes.Modules.Idp.Infrastructure.Persistence.Configurations;

internal sealed class VerificationConfiguration
    : IEntityTypeConfiguration<Verification>
{
    public void Configure(EntityTypeBuilder<Verification> builder)
    {
        builder.ToTable("Verifications");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                x => x.Value,
                value => VerificationId.Create(value));

        builder.Property(x => x.UserId);

        builder.Property(x => x.Type);

        builder.OwnsOne(x => x.Code, code =>
        {
            code.Property(x => x.Value)
                .HasColumnName("Code")
                .HasMaxLength(20);
        });

        builder.Property(x => x.CreatedOnUtc);

        builder.Property(x => x.ExpiresOnUtc);

        builder.Property(x => x.UsedOnUtc);
    }
}
