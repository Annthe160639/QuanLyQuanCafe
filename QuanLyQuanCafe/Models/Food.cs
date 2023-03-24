using System;
using System.Collections.Generic;

namespace QuanLyQuanCafe.Models;

public partial class Food
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int CategoryId { get; set; }

    public double Price { get; set; }

    public virtual ICollection<BillInfo> BillInfos { get; } = new List<BillInfo>();

    public virtual FoodCategory Category { get; set; } = null!;
}
