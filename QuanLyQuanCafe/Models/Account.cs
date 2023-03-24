using System;
using System.Collections.Generic;

namespace QuanLyQuanCafe.Models;

public partial class Account
{
    public string Username { get; set; } = null!;

    public string DisplayName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int Type { get; set; }
}
