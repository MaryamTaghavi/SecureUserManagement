using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SecureUserManagement.Authorization;
using SecureUserManagement.Data;

namespace SecureUserManagement.Infrustructure; 

public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.HasKey(r => r.Id);

        builder.HasData(
            new RolePermission
       {
           Id = 1,
           RoleId = Role.Registered.Id,
           PermissionId = (int)Authorization.Permission.ReadMember
       }
   );
      //  builder.HasData(Create(Role.Registered, Authorization.Permission.ReadMember)) ;
    }

    private static RolePermission Create(Role role, Authorization.Permission permission)
    {
        return new RolePermission
        {
            Id = 1,
            RoleId = role.Id,
            PermissionId = (int)permission,
            Permission = new Data.Permission(),
            Role = null
        };
    }
}
