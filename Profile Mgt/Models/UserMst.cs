using System;
using System.Collections.Generic;

namespace Profile_Mgt.Models;

public partial class UserMst
{
    public int Id { get; set; }

    public string Firstname { get; set; } = null!;

    public string? Middlename { get; set; }

    public string Lastname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime Dob { get; set; }

    public string Address { get; set; } = null!;

    public int Pincode { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string ProfileImage { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool IsDelete { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int UpdateBy { get; set; }

    public DateTime? UpdatedOn { get; set; }
}
