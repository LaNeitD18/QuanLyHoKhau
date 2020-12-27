using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using QuanLyHoKhau.Model;

namespace QuanLyHoKhau.ViewModel
{
    class NhapGiayTamVang_VM : BaseViewModel
    {
        #region UI

        private string _maNhanKhau;
        public string maNhanKhau
        {
            get
            {
                return _maNhanKhau;
            }

            set
            {
                _maNhanKhau = value;
            }
        }

        private DateTime _startDate = DateTime.Today;
        public DateTime startDate
        {
            get
            {
                return _startDate;
            }
            set
            {
                _startDate = value;
            }
        }

        private DateTime _endDate = DateTime.Today;
        public DateTime endDate
        {
            get
            {
                return _endDate;
            }
            set
            {
                _endDate = value;
            }
        }

        private string _reason;
        public string reason
        {
            get
            {
                return _reason;
            }
            set
            {
                _reason = value;
            }
        }

        private ICommand _acceptCommand;
        public ICommand acceptCommand
        {
            get
            {
                if(_acceptCommand == null)
                {
                    _acceptCommand = new RelayCommand(
                        param => this.Accept()
                    ); 
                }

                return _acceptCommand;
            }

            set
            {
                _acceptCommand = value;
            }
        }

        private ICommand _cancelCommand;
        public ICommand cancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                {
                    _cancelCommand = new RelayCommand(
                        param => this.Cancel()
                    );
                }

                return _cancelCommand;
            }

            set
            {
                _cancelCommand = value;
            }
        }

        #endregion

        #region Functions

        public string CheckValidInfo()
        {
            if (this.endDate < this.startDate) return "Ngày bắt đầu phải bé hơn ngày kết thúc";

            if (DataProvider.Ins.DB.NHANKHAUs.Find(this.maNhanKhau) == null) return "Không tìm thấy mã nhân khẩu";

            return null;
        }

        public bool Accept()
        {
            if (CheckValidInfo() != null) return false;

            string DiaPhuong = DataProvider.Ins.DB.CONGANs.Find(GlobalState.Ins().maCongAn).MaDiaPhuong;

            PHIEUKHAIBAOTAMVANG phieu = new PHIEUKHAIBAOTAMVANG();

            phieu.MaPhieuKhaiBao = this.maNhanKhau + "-" + this.startDate.Day.ToString() + "/" + this.startDate.Month.ToString() + "/" + this.startDate.Year.ToString();
            phieu.MaSoLuuTamVang = DataProvider.Ins.DB.SOLUUTAMVANGs.Where(x => x.DiaPhuong == DiaPhuong).Single().MaSoLuuTamVang;
            phieu.MaCongAn = GlobalState.Ins().maCongAn;
            phieu.MaNhanKhau = this.maNhanKhau;
            phieu.NgayKhaiBao = DateTime.Now;
            phieu.NgayBatDau = this.startDate;
            phieu.NgayKetThuc = this.endDate;
            phieu.LyDo = this.reason;

            DataProvider.Ins.DB.PHIEUKHAIBAOTAMVANGs.Add(phieu);
            try
            {
                DataProvider.Ins.DB.SaveChanges();
            }
            catch(Exception e)
            {
                return false;
            }
            return true;
        }
        public void Cancel()
        {

        }

        #endregion
    }
}
