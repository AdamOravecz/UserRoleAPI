namespace UserRoleAPI.Models
{
    public class Role
    {
        public Guid Id { get; set; }
        public string RoleName { get; set; }
        public ICollection<RoleUser> RolesUsers { get; set; } = new List<RoleUser>();
    }
}
