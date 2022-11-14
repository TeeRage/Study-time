//For exporting seed user data into database, this is ran in Program.cs
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUsers(DataContext context) 
        {
            if(await context.Users.AnyAsync()) return; 

            var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json"); //Read seed data
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
            foreach (var user in users) 
            {
                using var hmac = new HMACSHA512();
                user.UserName = user.UserName.ToLower();
                //Hardcoding same pw for all dummy test data users
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Passw0rd"));
                user.PasswordSalt = hmac.Key;

                context.Users.Add(user); //Add tracking for users
            }

            await context.SaveChangesAsync();
        }
    }
}