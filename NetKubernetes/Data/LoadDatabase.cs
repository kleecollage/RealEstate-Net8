using Microsoft.AspNetCore.Identity;
using NetKubernetes.Models;

namespace NetKubernetes.Data;

public class LoadDatabase
{
    public static async Task InsertData(AppDbContext context, UserManager<User> userManager)
    {
        if (!userManager.Users.Any())
        {
            var user = new User
            {
                Name = "John",
                Lastname = "Doe",
                Email = "john.doe@mail.com",
                UserName = "j.doe",
                Phone = "01 55 15 78 16"
            };
            await userManager.CreateAsync(user, password: "Admin123*");
        }

        if (!context.Estates.Any())
        {
            context.Estates.AddRange(
                new Estate
                {
                    Name = "House Beach",
                    Address = "133 Sun av.",
                    Price = 45000M,
                    CreatedAt = DateTime.Now,
                },
                new Estate
                {
                    Name = "Winter House",
                    Address = "75 Rock av.",
                    Price = 3500M,
                    CreatedAt = DateTime.Now,
                }
            );
        }
        
        await context.SaveChangesAsync();
    }
    
    
}