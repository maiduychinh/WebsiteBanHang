using WebBQA.Models;

namespace WebBQA.Repository
{
    public class LoaiSpRepository : ILoaiSpRepository
    {
        private readonly QuanLyQaContext _context;
        public LoaiSpRepository(QuanLyQaContext context)
        {
            _context = context;
        }
        public LoaiSp Add(LoaiSp loaiSp)
        {
            _context.LoaiSps.Add(loaiSp);
            _context.SaveChanges();
            return loaiSp;
        }

        public LoaiSp Delete(string maLoaiSp)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LoaiSp> GetAllLoaiSp()
        {
            return _context.LoaiSps;
        }

        public LoaiSp GetLoaiSp(string maLoaiSp)
        {
            return _context.LoaiSps.Find(maLoaiSp);
        }

        public LoaiSp Update(LoaiSp loaiSp)
        {
            _context.Update(loaiSp);
            _context.SaveChanges();
            return loaiSp;
        }
    }
}
