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
        public string  CheckValidInfo()
        {
            if (this.endDate < this.startDate) return "Ngày bắt đầu phải bé hơn ngày kết thúc";

            if (DataProvider.Ins.DB.NHANKHAUs.Find(this.maNhanKhau) == null) return "Không tìm thấy mã nhân khẩu";

            if((this.endDate - this.startDate).Days > GlobalState.Ins().maxFreeDaysStay)
            {
                if(this.maPhieuTamVang == "" || this.maPhieuTamVang == null)
                {
                    return "Tạm trú trên " + GlobalState.Ins().maxFreeDaysStay.ToString() + " ngày cần phải có phiếu tạm vắng";
                }

                var listPHIEUTAMVANG = DataProvider.Ins.DB.PHIEUKHAIBAOTAMVANGs.Where(x => x.MaPhieuKhaiBao == this.maPhieuTamVang).ToList();

                if (listPHIEUTAMVANG.Count == 0)
                {
                    return "Không tìm thấy thông tin phiếu tạm vắng";
                }

                if(listPHIEUTAMVANG[0].NgayKetThuc > this.endDate)
                {
                    return "Không thể ở lâu hơn thời hạn của phiếu tạm vắng";
                }
            }

            return null;
        }

        public bool Accept()
        {
            if (CheckValidInfo() != null)
                return false;

            string DiaPhuong = DataProvider.Ins.DB.CONGANs.Find(GlobalState.Ins().maCongAn).MaDiaPhuong;

            string pre_id = this.maNhanKhau + this.startDate.Day.ToString().PadLeft(2, '0') + this.startDate.Month.ToString().PadLeft(2, '0') + this.startDate.Year.ToString();
            string pos_id = DataProvider.Ins.DB.GIAYTAMTRUs.Where(x => x.MaGiayTamTru.Contains(pre_id)).ToList().Count.ToString().PadLeft(2, '0');

            GIAYTAMTRU phieu = new GIAYTAMTRU();

            phieu.MaGiayTamTru = pre_id + pos_id;
            phieu.MaSoLuuTamTru = DataProvider.Ins.DB.SOLUUTAMVANGs.Where(x => x.DiaPhuong == DiaPhuong).Single().MaSoLuuTamVang;
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

            DataProvider.Ins.DB.GIAYTAMTRUs.Add(phieu);
            try
            {
                DataProvider.Ins.DB.SaveChanges();
            }
            catch(Exception e)
            {
                return false;
            }

            ResetAllField();
            return true;
        }
        public void Cancel()
        {

        }

        public void ResetAllField()
        {
            this.maNhanKhau = null;
            this.maPhieuTamVang = null;
            this.noiDKTamTru = null;
            this.startDate = this.endDate = DateTime.Now;

        }

        #endregion
    }
}
