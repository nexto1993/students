using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Student.API.Models;
using System.Security.Claims;

namespace Student.API.Authorization
{

    public class StudentUserClaimsPrincipalFactory(UserManager<User> userManager,
       RoleManager<IdentityRole> roleManager,
       IOptions<IdentityOptions> options)
           : UserClaimsPrincipalFactory<User, IdentityRole>(userManager, roleManager, options)
    {
        public override async Task<ClaimsPrincipal> CreateAsync(User user)
        {
            var id = await GenerateClaimsAsync(user);

           

            if (user.DateOfBirth != null)
            {
                id.AddClaim(new Claim(AppClaimTypes.DateOfBirth, user.DateOfBirth.Value.ToString("yyyy-MM-dd")));
            }

            return new ClaimsPrincipal(id);

        }
    }
}
