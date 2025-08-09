using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SecureUserManagement.Authorization;

namespace SecureUserManagement.Infrustructure; 

public class PermissionConfiguration : IEntityTypeConfiguration<Data.Permission>
{
    public void Configure(EntityTypeBuilder<Data.Permission> builder)
    {
        builder.HasKey(r => r.Id);

        var permissions = Enum.GetValues<Permission>()
            .Select(p => new Data.Permission
            {
                Id = (int)p,
                Name = p.ToString()               
            });

        // سید کردن دیتای اولیه
        builder.HasData(permissions);
    }
}
