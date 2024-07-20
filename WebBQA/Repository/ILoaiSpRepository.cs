using WebBQA.Models;
namespace WebBQA.Repository
{
    public interface ILoaiSpRepository
    {
        LoaiSp Add(LoaiSp loaiSp);
        LoaiSp Update(LoaiSp loaiSp);
        LoaiSp Delete(string maLoaiSp);
        LoaiSp GetLoaiSp(string maLoaiSp);
        IEnumerable<LoaiSp> GetAllLoaiSp();
    }
}