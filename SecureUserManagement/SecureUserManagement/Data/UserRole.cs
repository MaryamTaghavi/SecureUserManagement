namespace SecureUserManagement.Data; 

public class UserRole
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int RoleId { get; set; }

    public Authorization.Role Role { get; set; }
    public User User { get; set; }
}
