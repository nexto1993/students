using Microsoft.AspNetCore.Identity;

namespace Student.API.Models
{
    public class User : IdentityUser
    {
        public DateOnly? DateOfBirth { get; set; }
    }
}
