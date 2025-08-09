using SecureUserManagement.Data;

namespace SecureUserManagement.Authorization; 

public sealed class Role : Enumeration<Role>
{
    public static readonly Role Registered = new (1 , "Registered");
    public Role(int id, string name) : base (id, name)
    {

    }

    public ICollection<Data.Permission> Permissions { get; set; }
    public ICollection<User> Users { get; set; }

}
