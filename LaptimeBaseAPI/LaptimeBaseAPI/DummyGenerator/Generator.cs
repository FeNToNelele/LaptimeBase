using Microsoft.AspNetCore.Identity;

var hasher = new PasswordHasher<IdentityUser>();
var user = new IdentityUser();
string hashedPassword = hasher.HashPassword(user, "admin");
Console.WriteLine(hashedPassword);