using System;
using System.Collections.Generic;

namespace Profile_Mgt.Models;

public partial class SubcategoryMst
{
    public int SubcategoryId { get; set; }

    public string SubcategoryName { get; set; } = null!;

    public int CategoryId { get; set; }

    public string SubcategoryImage { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool IsDelete { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int UpdateBy { get; set; }

    public DateTime? UpdatedOn { get; set; }
}
