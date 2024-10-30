using Microsoft.AspNetCore.Identity;

namespace Uzd2
{
    public class SeedData
    {
        public static async Task SeedRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Define roles
            var roles = new List<string> { "Admin", "houseOwner" };

            foreach (var role in roles)
            {
                // If the role doesn't exist, create it
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
