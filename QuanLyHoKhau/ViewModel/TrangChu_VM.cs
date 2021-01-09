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
        }
    }
}
