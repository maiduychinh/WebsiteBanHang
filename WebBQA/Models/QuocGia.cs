using System;
using System.Collections.Generic;

namespace WebBQA.Models;

public partial class QuocGia
{
    public string MaNuocSx { get; set; } = null!;

    public string? TenNuoc { get; set; }

    public virtual ICollection<DanhMucSp> DanhMucSps { get; set; } = new List<DanhMucSp>();
}
