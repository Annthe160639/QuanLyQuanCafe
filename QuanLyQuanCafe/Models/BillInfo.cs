using System;
using System.Collections.Generic;

namespace QuanLyQuanCafe.Models;

public partial class BillInfo
{
    public int Id { get; set; }

    public int BillId { get; set; }

    public int FoodId { get; set; }

    public int Count { get; set; }

    public virtual Bill Bill { get; set; } = null!;

    public virtual Food Food { get; set; } = null!;
}
