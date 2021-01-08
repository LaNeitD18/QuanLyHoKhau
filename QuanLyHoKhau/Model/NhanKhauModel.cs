using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyHoKhau.Model
{
    public partial class NHANKHAU
    {
        public NHANKHAU(NHANKHAU nhanKhau)
        {
            CopyInfo(nhanKhau);
        }

        public void CopyInfo(NHANKHAU nhanKhau)
        {
            if (nhanKhau == null)
            {
                CMND = null;
                MASHK = null;
                QuanHeVoiChuHo = null;
                ChoOHienNay = null;
                BanChinhThuc = true;
                IsDeleted = false;
            }
            else
            {
                CMND = nhanKhau.CMND;
                MASHK = nhanKhau.MASHK;
                QuanHeVoiChuHo = nhanKhau.QuanHeVoiChuHo;
                ChoOHienNay = nhanKhau.ChoOHienNay;
                BanChinhThuc = nhanKhau.BanChinhThuc;
                IsDeleted = nhanKhau.IsDeleted;
            }
        }

    }
}
