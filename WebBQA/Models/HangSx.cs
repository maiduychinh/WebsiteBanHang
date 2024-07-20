using System;
using System.Collections.Generic;

namespace WebBQA.Models;

public partial class HangSx
{
    public string MaHangSx { get; set; } = null!;

    public string? HangSx1 { get; set; }

    public virtual ICollection<DanhMucSp> DanhMucSps { get; set; } = new List<DanhMucSp>();
}
