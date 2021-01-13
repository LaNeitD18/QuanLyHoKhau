using LiveCharts;
using LiveCharts.Wpf;
using QuanLyHoKhau.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyHoKhau.ViewModel
{
    class ThongKeNhanKhau_VM : BaseViewModel
    {
        enum RangeOfAge : int
        {
            All = 0,
            LowerThanEighteen = 1,
            EighteenToFifty = 2,
            FiftyToEighty = 3,
            HigherThanEighty = 4
        };

        #region Variables
        private ObservableCollection<string> _ListAge;
        public ObservableCollection<string> ListAge
        {
            get { return _ListAge; }
            set { _ListAge = value; OnPropertyChanged(); }
        }

        private string _SelectedAge;
        public string SelectedAge
        {
            get { return _SelectedAge; }
            set { _SelectedAge = value; OnPropertyChanged(); }
        }

        private int _SelectedIndexAge;
        public int SelectedIndexAge
        {
            get { return _SelectedIndexAge; }
            set { _SelectedIndexAge = value; OnPropertyChanged(); }
        }

        private ObservableCollection<NHANKHAU> _ListFilteredNhanKhau;
        public ObservableCollection<NHANKHAU> ListFilteredNhanKhau
        {
            get { return _ListFilteredNhanKhau; }
            set { _ListFilteredNhanKhau = value; OnPropertyChanged(); }
        }

        private NHANKHAU _SelectedNhanKhau;
        public NHANKHAU SelectedNhanKhau
        {
            get { return _SelectedNhanKhau; }
            set { _SelectedNhanKhau = value; OnPropertyChanged(); }
        }

        private int _NKsInRange;
        public int NKsInRange
        {
            get { return _NKsInRange; }
            set { _NKsInRange = value; OnPropertyChanged(); }
        }

        private int _MinAge;
        public int MinAge
        {
            get { return _MinAge; }
            set { _MinAge = value; OnPropertyChanged(); }
        }

        private int _MaxAge;
        public int MaxAge
        {
            get { return _MaxAge; }
            set { _MaxAge = value; OnPropertyChanged(); }
        }

        private SeriesCollection _PieSeries;
        public SeriesCollection PieSeries
        {
            get { return _PieSeries; }
            set { _PieSeries = value; OnPropertyChanged(); }
        }

        //public Brush DangerBrushFill { get; set; } = new SolidColorBrush(Colors.Green);
        #endregion

        #region Functions
        private void LoadPieChartInput()
        {
            int thisYear = DateTime.Now.Year;

            LoadNhanKhauInRangeOfAge(0, 17);
            int lowerThanEighteen = ListFilteredNhanKhau.Count();

            LoadNhanKhauInRangeOfAge(18, 50);
            int eighteenToFifty = ListFilteredNhanKhau.Count();

            LoadNhanKhauInRangeOfAge(51, 80);
            int fiftyToEighty = ListFilteredNhanKhau.Count();

            LoadNhanKhauInRangeOfAge(81, 300);
            int higherThanEighty = ListFilteredNhanKhau.Count();

            PieSeries = new SeriesCollection {
                new PieSeries {
                    Title = "<18 tuổi",
                    Values = new ChartValues<int>{ lowerThanEighteen },
                    // set color
                    //Fill = DangerBrushFill
                },
                new PieSeries {
                    Title = "18-50 tuổi",
                    Values = new ChartValues<int>{ eighteenToFifty },
                    //Fill = DangerBrushFill
                },
                new PieSeries {
                    Title = "51-80 tuổi",
                    Values = new ChartValues<int>{ fiftyToEighty }
                },
                new PieSeries {
                    Title = ">80 tuổi",
                    Values = new ChartValues<int>{ higherThanEighty }
                }
            };
        }

        private int GetAgeOfNhanKhau(DateTime dateOfBirth)
        {
            var today = DateTime.Today;

            var a = (today.Year * 100 + today.Month) * 100 + today.Day;
            var b = (dateOfBirth.Year * 100 + dateOfBirth.Month) * 100 + dateOfBirth.Day;

            return (a - b) / 10000;
        }

        private void LoadNhanKhauInRangeOfAge(int minAge, int maxAge)
        {
            SetRangeOfAge(); // sửa lại hàm ko tham số sau
            int thisYear = DateTime.Now.Year;
            IQueryable<NHANKHAU> listFilteredNhanKhau;

            if (minAge == 0 && maxAge == 0) {
                listFilteredNhanKhau = DataProvider.Ins.DB.NHANKHAUs.Where(x => x.BanChinhThuc == true);
            } else {
                //listFilteredNhanKhau = DataProvider.Ins.DB.NHANKHAUs.Where(x => x.BanChinhThuc == true && GetAgeOfNhanKhau(x.NGUOI.NgaySinh) >= minAge && GetAgeOfNhanKhau(x.NGUOI.NgaySinh) <= maxAge);
                listFilteredNhanKhau = DataProvider.Ins.DB.NHANKHAUs.Where(x => x.BanChinhThuc == true && thisYear - x.NGUOI.NgaySinh.Year >= minAge && thisYear - x.NGUOI.NgaySinh.Year <= maxAge);
            }

            ListFilteredNhanKhau = new ObservableCollection<NHANKHAU>(listFilteredNhanKhau);
            NKsInRange = ListFilteredNhanKhau.Count();
        }

        private void SetRangeOfAge()
        {
            MinAge = 0;
            MaxAge = 0;

            if (SelectedIndexAge == (int)RangeOfAge.LowerThanEighteen)
            {
                MaxAge = 17;
            }
            else if (SelectedIndexAge == (int)RangeOfAge.EighteenToFifty)
            {
                MinAge = 18;
                MaxAge = 50;
            }
            else if (SelectedIndexAge == (int)RangeOfAge.FiftyToEighty)
            {
                MinAge = 51;
                MaxAge = 80;
            }
            else if (SelectedIndexAge == (int)RangeOfAge.HigherThanEighty)
            {
                MinAge = 81;
                MaxAge = 300;
            }
        }

        private void LoadData()
        {
            ListAge = new ObservableCollection<string>() { "Mọi lứa tuổi", "Dưới 18 tuổi", "Từ 18 đến 50 tuổi", "Từ 50 đến 80 tuổi", "Trên 80 tuổi" };
            SelectedIndexAge = 0;

            SetRangeOfAge();
            LoadNhanKhauInRangeOfAge(MinAge, MaxAge);

            // pie chart
            LoadPieChartInput();
        }

        #endregion

        #region ICommand
        public ICommand LoadNKInRangeOfAge_Command { get; set; }
        #endregion

        public ThongKeNhanKhau_VM()
        {
            LoadData();

            LoadNKInRangeOfAge_Command = new RelayCommand((p) => {
                SetRangeOfAge();
                LoadNhanKhauInRangeOfAge(MinAge, MaxAge);
            });
        }
    }
}
