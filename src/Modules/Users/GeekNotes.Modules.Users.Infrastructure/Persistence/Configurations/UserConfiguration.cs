namespace GeekNotes.Modules.Users.Infrastructure.Persistence.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(UserDbContextSchema.TableName);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value))
            .ValueGeneratedNever();


        builder.Property(x => x.FullName)
            .HasMaxLength(200)
            .IsRequired();



        builder.Property(x => x.Avatar)
            .HasMaxLength(500);



        builder.Property(x => x.PasswordHash)
            .HasMaxLength(500)
            .IsRequired();



        builder.Property(x => x.JoinedOnUtc)
            .IsRequired();



        builder.Property(x => x.Email)
            .HasConversion(
                    x => x.Value,
                    x => Email.Create(x));

        builder.Property(x => x.PhoneNumber)
            .HasConversion(
                x => x.Value,
                x => PhoneNumber.Create(x));

        builder.Property(x => x.UserName)
            .HasConversion(
                x => x.Value,
                x => UserName.Create(x));


        builder.OwnsMany(x => x.Roles, role =>
        {
            role.ToTable(UserDbContextSchema.UserRoles);

            role.WithOwner()
                .HasForeignKey(UserDbContextSchema.ForeignKey);

            role.Property(x => x.RoleId);

            role.HasKey(
                UserDbContextSchema.ForeignKey,
                nameof(UserRole.RoleId));
        });
    }
}