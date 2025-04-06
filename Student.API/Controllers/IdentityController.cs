using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Student.API.Dto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Student.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController(ApplicationDbContext applicationDbContext, IUserContext userContext) : ControllerBase
    {
        [HttpPatch("user")]
        [Authorize]
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
    }
}
