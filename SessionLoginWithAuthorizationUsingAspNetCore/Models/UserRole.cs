using System;
using System.Collections.Generic;

namespace SessionLoginWithAuthorizationUsingAspNetCore.Models;

public partial class UserRole
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? RoleId { get; set; }
}
