using System;
using System.Collections.Generic;

namespace QuanLyQuanCafe.Models;

public partial class Bill
{
    public int Id { get; set; }

    public DateTime DateCheckin { get; set; }

    public DateTime? DateCheckout { get; set; }

    public int TableId { get; set; }

    public int Status { get; set; }

    public int? Discount { get; set; }

    public virtual ICollection<BillInfo> BillInfos { get; } = new List<BillInfo>();

    public virtual TableFood Table { get; set; } = null!;
}
