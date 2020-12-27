using QuanLyHoKhau.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyHoKhau.ViewModel
{
    class NhapGiayTamTru_VM
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

        private string _maPhieuTamVang;
        public string maPhieuTamVang
        {
            get
            {
                return _maPhieuTamVang;
            }
            set
            {
                _maPhieuTamVang = value;
            }
        }

        private string _noiDKTamTru;
        public string noiDKTamTru
        {
            get
            {
                return _noiDKTamTru;
            }
            set
            {
                _noiDKTamTru = value;
            }
        }

        private ICommand _acceptCommand;
        public ICommand acceptCommand
        {
            get
            {
                if (_acceptCommand == null)
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
        bool checkValidInfo()
        {
            if (this.endDate < this.startDate) return false;

            if (DataProvider.Ins.DB.NHANKHAUs.Find(this.maNhanKhau) == null) return false;

            if((this.endDate - this.startDate).Days > GlobalState.Ins().maxFreeDaysStay)
            {
                var listPHIEUTAMVANG = DataProvider.Ins.DB.PHIEUKHAIBAOTAMVANGs.Where(x => x.MaNhanKhau == this.maNhanKhau).ToList();
                listPHIEUTAMVANG = listPHIEUTAMVANG.Where(x =>
                {
                    return x.NgayKetThuc > DateTime.Now;
                }).ToList();

                if(listPHIEUTAMVANG.Count == 0)
                {
                    return false;
                }
            }

            return true;
        }

        public void Accept()
        {
            if (checkValidInfo() == false)
                return;

            string DiaPhuong = DataProvider.Ins.DB.CONGANs.Find(GlobalState.Ins().maCongAn).MaDiaPhuong;

            GIAYTAMTRU phieu = new GIAYTAMTRU();

            phieu.MaGiayTamTru = this.maNhanKhau + this.startDate.Day.ToString() + "/" + this.startDate.Month.ToString() + "/" + this.startDate.Year.ToString();
            phieu.MaSoLuuTamTru = DataProvider.Ins.DB.SOLUUTAMVANGs.Where(x => x.DiaPhuong == DiaPhuong).Single().MaSoLuuTamVang;
            phieu.MaCongAn = GlobalState.Ins().maCongAn;
            phieu.MaNhanKhau = this.maNhanKhau;
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

            DataProvider.Ins.DB.GIAYTAMTRUs.Add(phieu);
            DataProvider.Ins.DB.SaveChanges();
        }
        public void Cancel()
        {

        }

        #endregion
    }
}
