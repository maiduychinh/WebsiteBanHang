using System;
using System.Collections.Generic;

namespace WebBQA.Models;

public partial class ChatLieu
{
    public string MaChatLieu { get; set; } = null!;

    public string ChatLieu1 { get; set; } = null!;

    public virtual ICollection<DanhMucSp> DanhMucSps { get; set; } = new List<DanhMucSp>();
}
