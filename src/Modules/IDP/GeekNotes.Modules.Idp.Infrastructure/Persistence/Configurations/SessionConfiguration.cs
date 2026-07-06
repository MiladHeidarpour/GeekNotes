using GeekNotes.Modules.Idp.Domain.Sessions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeekNotes.Modules.Idp.Infrastructure.Persistence.Configurations;

internal sealed class SessionConfiguration
    : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.ToTable("Sessions");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                x => x.Value,
                value => SessionId.Create(value));

        builder.Property(x => x.UserId);

        builder.OwnsOne(x => x.RefreshTokenHash, hash =>
        {
            hash.Property(x => x.Value)
                .HasColumnName("RefreshTokenHash")
                .HasMaxLength(500);
        });

        builder.OwnsOne(x => x.DeviceId, device =>
        {
            device.Property(x => x.Value)
                .HasColumnName("DeviceId")
                .HasMaxLength(200);
        });

        builder.Property(x => x.UserAgent)
            .HasMaxLength(500);

        builder.Property(x => x.IpAddress)
            .HasMaxLength(50);

        builder.Property(x => x.CreatedOnUtc);

        builder.Property(x => x.ExpiresOnUtc);

        builder.Property(x => x.RevokedOnUtc);
    }
}
