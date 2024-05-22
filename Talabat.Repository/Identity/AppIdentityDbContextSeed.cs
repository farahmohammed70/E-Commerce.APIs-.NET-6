using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Identity
{
    public static class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if(!userManager .Users.Any())
            {
                var user = new AppUser()
                {
                    DisplayName = "Farah Mohammad",
                    Email = "Farah@Gmail.com",
                    UserName = "Farah.Mohammad",
                    PhoneNumber = "01022962992"
                };
                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}
