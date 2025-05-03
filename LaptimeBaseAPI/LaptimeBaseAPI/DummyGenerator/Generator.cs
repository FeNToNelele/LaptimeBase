using Microsoft.AspNetCore.Identity;

namespace LaptimeBaseAPI.DummyGenerator;

class Generator
{
    public void Generate()
    {
        var hasher = new PasswordHasher<IdentityUser>();
        var user = new IdentityUser();
        string hashedPassword = hasher.HashPassword(user, "admin");
        Console.WriteLine(hashedPassword);
    }
}