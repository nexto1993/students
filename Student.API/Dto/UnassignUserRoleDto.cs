namespace Student.API.Dto
{
    public class UnassignUserRoleDto
    {
        public string UserEmail { get; set; } = default!;
        public string RoleName { get; set; } = default!;
    }
}
