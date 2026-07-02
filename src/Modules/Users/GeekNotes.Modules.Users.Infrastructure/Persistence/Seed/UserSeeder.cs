namespace GeekNotes.Modules.Users.Infrastructure.Persistence.Seed;

public static class UserSeeder
{
    public static async Task SeedAsync(UserContext dbContext)
    {
        if (await dbContext.Users.AnyAsync())
        {
            return;
        }

        var user = User.Create(
            Email.Create("mld.heidarpour@gmail.com"),
            PhoneNumber.Create("09030826556"),
            UserName.Create("mld.heidarpour"),
            "Milad Heidarpour",
            "mhC#asp.netcore",
            new List<Guid>()
            {
                Guid.Parse("fdc6c3c4-0821-4aec-ba87-bec6db17ad09"),
                Guid.Parse("9f909d47-3c7b-4112-a57a-6fd2fc7bc74c"),
                Guid.Parse("e8197339-0b33-4408-9b40-63aa518e500f"),
            });

        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();
    }
}
