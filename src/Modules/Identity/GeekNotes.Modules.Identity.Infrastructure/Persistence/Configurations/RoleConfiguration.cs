using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeekNotes.Modules.Identity.Infrastructure.Persistence.Configurations;

internal class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable(IdentityDbContextSchema.RoleDbSchema.TableName);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .ValueGeneratedNever()
               .HasConversion(
                    id => id.Value,
                    value => RoleId.Create(value));


        builder.OwnsOne(x => x.Name, name =>
        {
            name.Property(x => x.Value)
                        .HasColumnName("Name")
                        .HasMaxLength(64)
                        .IsUnicode(false)
                        .IsRequired();

            name.HasIndex(x => x.Value)
                .IsUnique();
        });


        builder.Property(x => x.Title)
               .IsRequired()
               .HasMaxLength(100)
               .IsUnicode(true);

        builder.OwnsMany(x => x.Permissions, permission =>
        {
            permission.ToTable(IdentityDbContextSchema.RoleDbSchema.Permissions);

            permission.WithOwner().HasForeignKey(IdentityDbContextSchema.RoleDbSchema.ForeignKey);

            permission.Property(x => x.Name)
                      .HasColumnName("Permission")
                      .HasMaxLength(128)
                      .IsUnicode(false)
                      .IsRequired();

            permission.HasKey(IdentityDbContextSchema.RoleDbSchema.ForeignKey, "Name");
        });
    }
}
