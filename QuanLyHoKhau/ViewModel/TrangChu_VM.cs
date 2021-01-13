using QuanLyHoKhau.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace QuanLyHoKhau.ViewModel
{
    class TrangChu_VM : BaseViewModel
    {
        #region Variables
        public DispatcherTimer _timer;
        private string _CurrentDate;
        public string CurrentDate
        {
            get { return _CurrentDate; }
            set
            {
                if (_CurrentDate == value)
                    return;
                _CurrentDate = value; OnPropertyChanged();
            }
        }

        private string _CurrentTime;
        public string CurrentTime
        {
            get { return _CurrentTime; }
            set
            {
                if (_CurrentTime == value)
                    return;
                _CurrentTime = value; OnPropertyChanged();
            }
        }

        private double _SoHoKhauProgress;
        public double SoHoKhauProgress
        {
            get { return _SoHoKhauProgress; }
            set { _SoHoKhauProgress = value; OnPropertyChanged(); }
        }

        private string _NewSoHoKhausInfo;
        public string NewSoHoKhausInfo
        {
            get { return _NewSoHoKhausInfo; }
            set { _NewSoHoKhausInfo = value; OnPropertyChanged(); }
        }

        private double _GiayTamVangProgress;
        public double GiayTamVangProgress
        {
            get { return _GiayTamVangProgress; }
            set { _GiayTamVangProgress = value; OnPropertyChanged(); }
        }

        private string _NewTamVangsInfo;
        public string NewTamVangsInfo
        {
            get { return _NewTamVangsInfo; }
            set { _NewTamVangsInfo = value; OnPropertyChanged(); }
        }

        private double _GiayTamTruProgress;
        public double GiayTamTruProgress
        {
            get { return _GiayTamTruProgress; }
            set { _GiayTamTruProgress = value; OnPropertyChanged(); }
        }

        private string _NewTamTrusInfo;
        public string NewTamTrusInfo
        {
            get { return _NewTamTrusInfo; }
            set { _NewTamTrusInfo = value; OnPropertyChanged(); }
        }

        private double _GiayChuyenKhauProgress;
        public double GiayChuyenKhauProgress
        {
            get { return _GiayChuyenKhauProgress; }
            set { _GiayChuyenKhauProgress = value; OnPropertyChanged(); }
        }

        private string _NewChuyenKhausInfo;
        public string NewChuyenKhausInfo
        {
            get { return _NewChuyenKhausInfo; }
            set { _NewChuyenKhausInfo = value; OnPropertyChanged(); }
        }

        private double _SoHoKhauChoDuyetProgress;
        public double SoHoKhauChoDuyetProgress
        {
            get { return _SoHoKhauChoDuyetProgress; }
            set { _SoHoKhauChoDuyetProgress = value; OnPropertyChanged(); }
        }

        private string _SoHoKhauChoDuyetsInfo;
        public string SoHoKhauChoDuyetsInfo
        {
            get { return _SoHoKhauChoDuyetsInfo; }
            set { _SoHoKhauChoDuyetsInfo = value; OnPropertyChanged(); }
        }

        private double _NhanKhauChoDuyetProgress;
        public double NhanKhauChoDuyetProgress
        {
            get { return _NhanKhauChoDuyetProgress; }
            set { _NhanKhauChoDuyetProgress = value; OnPropertyChanged(); }
        }

        private string _NhanKhauChoDuyetsInfo;
        public string NhanKhauChoDuyetsInfo
        {
            get { return _NhanKhauChoDuyetsInfo; }
            set { _NhanKhauChoDuyetsInfo = value; OnPropertyChanged(); }
        }

        private double _GiayChuyenKhauChoDuyetProgress;
        public double GiayChuyenKhauChoDuyetProgress
        {
            get { return _GiayChuyenKhauChoDuyetProgress; }
            set { _GiayChuyenKhauChoDuyetProgress = value; OnPropertyChanged(); }
        }

        private string _GiayChuyenKhauChoDuyetsInfo;
        public string GiayChuyenKhauChoDuyetsInfo
        {
            get { return _GiayChuyenKhauChoDuyetsInfo; }
            set { _GiayChuyenKhauChoDuyetsInfo = value; OnPropertyChanged(); }
        }
        #endregion

        #region Functions
        private string DayOfWeekInVietnam()
        {
            DayOfWeek currentDay = DateTime.Now.DayOfWeek;
            if(currentDay == DayOfWeek.Monday) {
                return "Thứ hai,";
            }
            else if(currentDay == DayOfWeek.Tuesday) {
                return "Thứ ba,";
            }
            else if(currentDay == DayOfWeek.Wednesday) {
                return "Thứ tư,";
            }
            else if(currentDay == DayOfWeek.Thursday) {
                return "Thứ năm,";
            }
            else if(currentDay == DayOfWeek.Friday) {
                return "Thứ sáu,";
            }
            else if(currentDay == DayOfWeek.Saturday) {
                return "Thứ bảy,";
            }
            else if(currentDay == DayOfWeek.Sunday) {
                return "Chủ nhật,";
            }
            return "";
        }

        private void CountNewItems()
        {
            int thisMonth = DateTime.Now.Month;
            int thisYear = DateTime.Now.Year;

            // calculate number of new sohokhau
            double countNewSoHoKhaus = DataProvider.Ins.DB.SOHOKHAUs.Where(x => x.NgayTaoSo.Month == thisMonth && x.NgayTaoSo.Year == thisYear && x.BanChinhThuc == true && x.IsDeleted == false).Count();
            SoHoKhauProgress = countNewSoHoKhaus / 100 * 100;
            NewSoHoKhausInfo = countNewSoHoKhaus.ToString() + "/100";

            // calculate number of new giaytamvang
            double countNewTamVangs = DataProvider.Ins.DB.PHIEUKHAIBAOTAMVANGs.Where(x => x.NgayKhaiBao.Month == thisMonth && x.NgayKhaiBao.Year == thisYear).Count();
            GiayTamVangProgress = countNewTamVangs / 100 * 100;
            NewTamVangsInfo = countNewTamVangs.ToString() + "/100";

            // calculate number of new giaytamtru
            double countNewTamTrus = DataProvider.Ins.DB.GIAYTAMTRUs.Where(x => x.NgayKhaiBao.Value.Month == thisMonth && x.NgayKhaiBao.Value.Year == thisYear).Count();
            GiayTamTruProgress = countNewTamTrus / 100 * 100;
            NewTamTrusInfo = countNewTamTrus.ToString() + "/100";

            // calculate number of new giaychuyenkhau
            double countNewChuyenKhaus = DataProvider.Ins.DB.PHIEUDUYETCHUYENKHAUs.Where(x => x.NgayTao.Value.Month == thisMonth && x.NgayTao.Value.Year == thisYear).Count();
            GiayChuyenKhauProgress = countNewChuyenKhaus / 100 * 100;
            NewChuyenKhausInfo = countNewChuyenKhaus.ToString() + "/100";
        }

        private void CountWaitApprovingItems()
        {
            int thisMonth = DateTime.Now.Month;
            int thisYear = DateTime.Now.Year;

            // calculate number of choduyet sohokhau
            double countCDSoHoKhaus = DataProvider.Ins.DB.PHIEUDUYETSOHOKHAUs.Where(x => x.NgayTao.Value.Month == thisMonth && x.NgayTao.Value.Year == thisYear && x.DaDuyet == false && x.IsDeleted == false).Count();
            double allCDSoHoKhauInMonth = DataProvider.Ins.DB.PHIEUDUYETSOHOKHAUs.Where(x => x.NgayTao.Value.Month == thisMonth && x.NgayTao.Value.Year == thisYear && x.IsDeleted == false).Count(); 
            if(allCDSoHoKhauInMonth == 0) {
                SoHoKhauChoDuyetProgress = 0;
            }
            else {
                SoHoKhauChoDuyetProgress = countCDSoHoKhaus / allCDSoHoKhauInMonth * 100;
            }
            SoHoKhauChoDuyetsInfo = countCDSoHoKhaus.ToString() + "/" + allCDSoHoKhauInMonth.ToString();

            // calculate number of choduyet nhankhau
            double countCDNhanKhaus = DataProvider.Ins.DB.PHIEUDUYETNHANKHAUs.Where(x => x.NgayTao.Value.Month == thisMonth && x.NgayTao.Value.Year == thisYear && x.DaDuyet == false && x.IsDeleted == false).Count();
            double allCDNhanKhauInMonth = DataProvider.Ins.DB.PHIEUDUYETNHANKHAUs.Where(x => x.NgayTao.Value.Month == thisMonth && x.NgayTao.Value.Year == thisYear && x.IsDeleted == false).Count();
            if(allCDNhanKhauInMonth == 0) {
                NhanKhauChoDuyetProgress = 0;
            }
            else {
                NhanKhauChoDuyetProgress = countCDNhanKhaus / allCDNhanKhauInMonth * 100;
            }
            NhanKhauChoDuyetsInfo = countCDNhanKhaus.ToString() + "/" + allCDNhanKhauInMonth.ToString();

            // calculate number of choduyet giaychuyenkhau
            double countCDChuyenKhaus = DataProvider.Ins.DB.PHIEUDUYETCHUYENKHAUs.Where(x => x.NgayTao.Value.Month == thisMonth && x.NgayTao.Value.Year == thisYear && x.DaDuyet == false && x.IsDeleted == false).Count();
            double allCDChuyenKhauInMonth = DataProvider.Ins.DB.PHIEUDUYETCHUYENKHAUs.Where(x => x.NgayTao.Value.Month == thisMonth && x.NgayTao.Value.Year == thisYear && x.IsDeleted == false).Count();
            if(allCDChuyenKhauInMonth == 0) {
                GiayChuyenKhauChoDuyetProgress = 0;
            }
            else {
                GiayChuyenKhauChoDuyetProgress = countCDChuyenKhaus / allCDChuyenKhauInMonth * 100;
            }
            GiayChuyenKhauChoDuyetsInfo = countCDChuyenKhaus.ToString() + "/" + allCDChuyenKhauInMonth.ToString();
        }

        private void LoadData()
        {
            CountNewItems();
            CountWaitApprovingItems();
        }
        private void GetTimeNow()
        {
            CurrentTime = DateTime.Now.ToString("HH:mm:ss");
            // Copy from https://stackoverflow.com/questions/31160201/time-ticking-in-c-sharp-wpf-mvvm
            _timer = new DispatcherTimer(DispatcherPriority.Render);
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += (sender, args) =>
            {
                //Khởi tạo tài khoản cho toàn bộ chương trình
                //InitTaiKhoan();
                CurrentTime = DateTime.Now.ToString("HH:mm:ss");
            };
            _timer.Start();

            CurrentDate = DayOfWeekInVietnam() + " " + DateTime.Now.ToString("dd/MM/yyyy");
        }
        #endregion

        public TrangChu_VM()
        {
            GetTimeNow();
            LoadData();
        }
    }
}
