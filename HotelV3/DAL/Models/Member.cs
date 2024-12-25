using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Member
{
    public int MemberId { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Role { get; set; } = null!;

    public int Status { get; set; }
    public override string ToString()
    {
        return $"{Name} - {Email}"; // Customize this as needed
    }
}
