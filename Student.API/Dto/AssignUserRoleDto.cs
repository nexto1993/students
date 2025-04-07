namespace Student.API.Dto
{
    public class AssignUserRoleDto
    {
        public string UserEmail { get; set; } = default!;
        public string RoleName { get; set; } = default!;
    }
}
