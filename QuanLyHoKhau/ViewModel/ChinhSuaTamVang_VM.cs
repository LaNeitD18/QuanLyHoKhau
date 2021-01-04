using QuanLyHoKhau.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyHoKhau.ViewModel
{
    class ChinhSuaTamVang_VM: NhapGiayTamVang_VM
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


        public new bool Accept()
        {
            if (CheckValidInfo() != null) return false;

            PHIEUKHAIBAOTAMVANG phieu = DataProvider.Ins.DB.PHIEUKHAIBAOTAMVANGs.Find(MaPhieu);

            phieu.MaCongAn = GlobalState.Ins().maCongAn;
            phieu.CMND = this.maNhanKhau;
            phieu.NgayKhaiBao = DateTime.Now;
            phieu.NgayBatDau = this.startDate;
            phieu.NgayKetThuc = this.endDate;
            phieu.LyDo = this.reason;

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
            PHIEUKHAIBAOTAMVANG phieu = PhieuTamVangObject as PHIEUKHAIBAOTAMVANG;

            this.MaPhieu = phieu.MaPhieuKhaiBao;
            this.maNhanKhau = phieu.CMND;
            this.startDate = phieu.NgayBatDau;
            this.endDate = phieu.NgayKetThuc;
            this.reason = phieu.LyDo;

            OnPropertyChanged("MaPhieu");
            OnPropertyChanged("maNhanKhau");
            OnPropertyChanged("startDate");
            OnPropertyChanged("endDate");
            OnPropertyChanged("reason");
        }
    }
}
