using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyHoKhau.Model;

namespace QuanLyHoKhau.ViewModel
{
    class ChinhSuaTamTru_VM: NhapGiayTamTru_VM
    {
        #region UI
        private string _maPhieu;
        public string MaPhieu
        {
            get
            {
                return _maPhieu;
            }
            set
            {
                _maPhieu = value;
            }
        }
        #endregion

        public bool Accept()
        {
            if (CheckValidInfo() != null)
                return false;

            GIAYTAMTRU phieu = DataProvider.Ins.DB.GIAYTAMTRUs.Find(MaPhieu);

            phieu.MaCongAn = GlobalState.Ins().maCongAn;
            phieu.CMND = this.maNhanKhau;
            phieu.NgayKhaiBao = DateTime.Now;
            phieu.NgayBatDau = this.startDate;
            phieu.NgayKetThuc = this.endDate;
            phieu.NoiDangKyTamTru = this.noiDKTamTru;

            if ((this.endDate - this.startDate).Days > GlobalState.Ins().maxFreeDaysStay)
            {
                phieu.MaGiayTamVang = this.maPhieuTamVang;
            }
            else
            {
                phieu.MaGiayTamVang = null;
            }

            try
            {
                DataProvider.Ins.DB.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }

            ResetAllField();
            return true;
        }

        public void InitMaPhieu(Object PhieuTamVangObject)
        {
            GIAYTAMTRU phieu = PhieuTamVangObject as GIAYTAMTRU;

            this.MaPhieu = phieu.MaGiayTamTru;
            this.maNhanKhau = phieu.CMND;
            this.startDate = phieu.NgayBatDau.Value;
            this.endDate = phieu.NgayKetThuc.Value;
            this.noiDKTamTru = phieu.NoiDangKyTamTru;

            OnPropertyChanged("MaPhieu");
            OnPropertyChanged("maNhanKhau");
            OnPropertyChanged("startDate");
            OnPropertyChanged("endDate");
            OnPropertyChanged("noiDKTamTru");
        }
    }
}
