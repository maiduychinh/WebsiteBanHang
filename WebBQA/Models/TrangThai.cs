using System;
using System.Collections.Generic;

namespace WebBQA.Models;

public partial class TrangThai
{
    public string MaTrangThai { get; set; } = null!;

    public string? TenTrangThai { get; set; }

    public virtual ICollection<HoaDonBan> HoaDonBans { get; set; } = new List<HoaDonBan>();
}
