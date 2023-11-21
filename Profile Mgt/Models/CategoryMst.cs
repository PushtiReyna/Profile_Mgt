using System;
using System.Collections.Generic;

namespace Profile_Mgt.Models;

public partial class CategoryMst
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public string CategoryImage { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool IsDelete { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int UpdateBy { get; set; }

    public DateTime? UpdatedOn { get; set; }
}
