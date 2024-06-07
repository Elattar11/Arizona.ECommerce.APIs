using Arizona.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arizona.Infrastructure.Identity
{
    public static class ApplicationIdentityDataSeed
    {
        public static async Task SeedUsersAsync(UserManager<ApplicationUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new ApplicationUser()
                {
                    DisplayName = "Mohamed Attar",
                    Email = "mohamed.attar@gmail.com",
                    UserName = "Elattar",
                    PhoneNumber = "01112007553"
                };

                await userManager.CreateAsync(user, "P@ssw0rd");
            }
        }
    }
}
