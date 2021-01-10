using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyHoKhau.Model
{
    public partial class NGUOI
    {
        public NGUOI(NGUOI nguoi)
        {
            CopyInfo(nguoi);
        }

        public void CopyInfo(NGUOI nguoi)
        {
            if (nguoi == null)
            {
                CMND = null;
                Ten = null;
                NgaySinh = DateTime.Now;
                GioiTinh = null;
                NoiSinh = null;
                QueQuan = null;
                DanToc = null;
                TonGiao = null;
                NgheNghiep = null;
                IsDeleted = false;
            }
            else
            {
                CMND = nguoi.CMND;
                Ten = nguoi.Ten;
                NgaySinh = nguoi.NgaySinh;
                GioiTinh = nguoi.GioiTinh;
                NoiSinh = nguoi.NoiSinh;
                QueQuan = nguoi.QueQuan;
                DanToc = nguoi.DanToc;
                TonGiao = nguoi.TonGiao;
                NgheNghiep = nguoi.NgheNghiep;
                IsDeleted = nguoi.IsDeleted;
            }
        }
    }
}
