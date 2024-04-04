using System;
using System.Collections.Generic;

namespace SessionLoginWithAuthorizationUsingAspNetCore.Models;

public partial class Account
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string? Password { get; set; }

    public string? FullName { get; set; }
}
