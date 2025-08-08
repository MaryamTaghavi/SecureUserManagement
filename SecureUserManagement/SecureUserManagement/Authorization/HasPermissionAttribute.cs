using Microsoft.AspNetCore.Authorization;

namespace SecureUserManagement.Authorization; 

// اگر کلاسی از یک کلاس پدر ارث بری کند و کلاس پدر یک سازنده با ورودی در کانیتراکتور داشته باشد
// در کانستراکتور فرزند با کلید واژه base میتوان ورودی 
// را از کلاس فرزند به پدر پاس داد
public sealed class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(Permission permission) : base(policy:permission.ToString())
    {

    }
}

// 1) Define roles
// 2) Assign to members
// 3) For each role, which permissions that role has.
