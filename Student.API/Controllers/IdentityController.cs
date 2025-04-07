using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Student.API.Dto;
using Student.API.Exceptions;
using Student.API.Models;


namespace Student.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class IdentityController(ApplicationDbContext applicationDbContext, IUserContext userContext, UserManager<User> userManager, RoleManager<IdentityRole> roleManager) : ControllerBase
    {
        [HttpPatch("user")]
        public async Task<IActionResult> UpdateUserDetails(UserUpdateDto userDto)
        {
            var user = userContext.GetCurrentUser();
            var dbuser = await applicationDbContext.Users.FindAsync(user!.Id);
            if (dbuser == null)
            {
                return NotFound();
            }
            dbuser.DateOfBirth = userDto.DateOfBirth;
            await applicationDbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost("role")]
        public async Task<IActionResult> CreateRole(IdentityRole role)
        {
            await applicationDbContext.Roles.AddAsync(role);
            await applicationDbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("userRole")]
        //[Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> AssignUserRole(AssignUserRoleDto assignUserRoleDto)
        {
            var user = await userManager.FindByEmailAsync(assignUserRoleDto.UserEmail) ?? throw new NotFoundException(nameof(User), assignUserRoleDto.UserEmail);
            var role = await roleManager.FindByNameAsync(assignUserRoleDto.RoleName)
            ?? throw new NotFoundException(nameof(IdentityRole), assignUserRoleDto.RoleName);
            await userManager.AddToRoleAsync(user, role.Name!);
            return NoContent();
        }

        [HttpDelete("userRole")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> UnassignUserRole(UnassignUserRoleDto unassignUserRoleDto)
        {

            var user = await userManager.FindByEmailAsync(unassignUserRoleDto.UserEmail) ?? throw new NotFoundException(nameof(User), unassignUserRoleDto.UserEmail);
            var role = await roleManager.FindByNameAsync(unassignUserRoleDto.RoleName)
            ?? throw new NotFoundException(nameof(IdentityRole), unassignUserRoleDto.RoleName);
            await userManager.RemoveFromRoleAsync(user, role.Name!);
            return NoContent();
        }
    }
}
