using LiveCharts;
using LiveCharts.Wpf;
using QuanLyHoKhau.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace QuanLyHoKhau.ViewModel
{
    class BaoCao_VM : BaseViewModel
    {
        enum LoaiGiayTo : int
        {
            SoHoKhau = 0,
            GiayTamVang = 1,
            GiayTamTru = 2,
            GiayChuyenKhau = 3
        };

        #region Variables
        private ObservableCollection<int> _ListMonth;
        public ObservableCollection<int> ListMonth
        {
            get { return _ListMonth; }
            set { _ListMonth = value; OnPropertyChanged(); }
        }

        private int _SelectedMonth;
        public int SelectedMonth
        {
            get { return _SelectedMonth; }
            set { _SelectedMonth = value; OnPropertyChanged(); }
        }

        private ObservableCollection<int> _ListYear;
        public ObservableCollection<int> ListYear
        {
            get { return _ListYear; }
            set { _ListYear = value; OnPropertyChanged(); }
        }

        private int _SelectedYear;
        public int SelectedYear
        {
            get { return _SelectedYear; }
            set { _SelectedYear = value; OnPropertyChanged(); }
        }

        private ObservableCollection<string> _ListLoaiGiayTo;
        public ObservableCollection<string> ListLoaiGiayTo
        {
            get { return _ListLoaiGiayTo; }
            set { _ListLoaiGiayTo = value; OnPropertyChanged(); }
        }

        private string _SelectedLoaiGiayTo;
        public string SelectedLoaiGiayTo
        {
            get { return _SelectedLoaiGiayTo; }
            set { _SelectedLoaiGiayTo = value; OnPropertyChanged(); }
        }

        private int _SelectedIndexLoaiGiayTo;
        public int SelectedIndexLoaiGiayTo
        {
            get { return _SelectedIndexLoaiGiayTo; }
            set { _SelectedIndexLoaiGiayTo = value; OnPropertyChanged(); }
        }

        private int _NewGiayChuyenKhausInMonth;
        public int NewGiayChuyenKhausInMonth
        {
            get { return _NewGiayChuyenKhausInMonth; }
            set { _NewGiayChuyenKhausInMonth = value; OnPropertyChanged(); }
        }

        private int _NewSoHoKhausInMonth;
        public int NewSoHoKhausInMonth
        {
            get { return _NewSoHoKhausInMonth; }
            set { _NewSoHoKhausInMonth = value; OnPropertyChanged(); }
        }

        private int _NewGiayTamTrusInMonth;
        public int NewGiayTamTrusInMonth
        {
            get { return _NewGiayTamTrusInMonth; }
            set { _NewGiayTamTrusInMonth = value; OnPropertyChanged(); }
        }

        private int _NewGiayTamVangsInMonth;
        public int NewGiayTamVangsInMonth
        {
            get { return _NewGiayTamVangsInMonth; }
            set { _NewGiayTamVangsInMonth = value; OnPropertyChanged(); }
        }

        private SeriesCollection _PieSeries;
        public SeriesCollection PieSeries
        {
            get { return _PieSeries; }
            set { _PieSeries = value; OnPropertyChanged(); }
        }

        private SeriesCollection _ColumnSeries;
        public SeriesCollection ColumnSeries
        {
            get { return _ColumnSeries; }
            set { _ColumnSeries = value; OnPropertyChanged(); }
        }

        private Func<ChartPoint, string> _PointLabel;
        public Func<ChartPoint, string> PointLabel
        {
            get { return _PointLabel; }
            set { _PointLabel = value; OnPropertyChanged(); }
        }

        private ObservableCollection<int> _ItemsCountedEachMonth;
        public ObservableCollection<int> ItemsCountedEachMonth
        {
            get { 
                //if(ItemsCountedEachMonth == null) {
                //    ItemsCountedEachMonth = new ObservableCollection<int>();
                //}
                return _ItemsCountedEachMonth; }
            set { _ItemsCountedEachMonth = value; OnPropertyChanged(); }
        }

        //public Brush DangerBrushFill { get; set; } = new SolidColorBrush(Colors.Green);
        #endregion

        #region Function
        // load full ko load theo thang, nam
        private void LoadPieChartInput()
        {
            NewSoHoKhausInMonth = DataProvider.Ins.DB.SOHOKHAUs.Where(x => x.NgayTaoSo.Month == SelectedMonth && x.NgayTaoSo.Year == DateTime.Now.Year).Count();
            NewGiayTamVangsInMonth = DataProvider.Ins.DB.PHIEUKHAIBAOTAMVANGs.Where(x => x.NgayKhaiBao.Month == SelectedMonth && x.NgayKhaiBao.Year == DateTime.Now.Year).Count();
            // xem va sua lai db, ko cho NgayKhaiBao nullable
            NewGiayTamTrusInMonth = DataProvider.Ins.DB.GIAYTAMTRUs.Where(x => x.NgayKhaiBao.Value.Month == SelectedMonth && x.NgayKhaiBao.Value.Year == DateTime.Now.Year).Count();
            NewGiayChuyenKhausInMonth = DataProvider.Ins.DB.PHIEUTHAYDOI_HK_NK.Where(x => x.NgayKhaiBao.Month == SelectedMonth && x.NgayKhaiBao.Year == DateTime.Now.Year).Count();

            PieSeries = new SeriesCollection {
                new PieSeries {
                    Title = "Sổ hộ khẩu",
                    Values = new ChartValues<int>{ NewSoHoKhausInMonth },
                    // set color
                    //Fill = DangerBrushFill
                },
                new PieSeries {
                    Title = "Giấy tạm vắng",
                    Values = new ChartValues<int>{ NewGiayTamVangsInMonth },
                    //Fill = DangerBrushFill
                },
                new PieSeries {
                    Title = "Giấy tạm trú",
                    Values = new ChartValues<int>{ NewGiayTamTrusInMonth }
                },
                new PieSeries {
                    Title = "Giấy chuyển khẩu",
                    Values = new ChartValues<int>{ NewGiayChuyenKhausInMonth }
                }
            };
        }

        private void LoadBarChartInput()
        {
            PointLabel = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
            
            ItemsCountedEachMonth = new ObservableCollection<int>();
            for(int i=1; i<=12; i++) {
                int countItemsInMonth = 0;
                if (SelectedIndexLoaiGiayTo == (int)LoaiGiayTo.SoHoKhau) {
                    countItemsInMonth = DataProvider.Ins.DB.SOHOKHAUs.Where(x => x.NgayTaoSo.Year == SelectedYear && x.NgayTaoSo.Month == i).Count();
                }
                else if(SelectedIndexLoaiGiayTo == (int)LoaiGiayTo.GiayTamVang) {
                    countItemsInMonth = DataProvider.Ins.DB.PHIEUKHAIBAOTAMVANGs.Where(x => x.NgayKhaiBao.Year == SelectedYear && x.NgayKhaiBao.Month == i).Count();
                }
                else if(SelectedIndexLoaiGiayTo == (int)LoaiGiayTo.GiayTamTru) {
                    countItemsInMonth = DataProvider.Ins.DB.GIAYTAMTRUs.Where(x => x.NgayKhaiBao.Value.Year == SelectedYear && x.NgayKhaiBao.Value.Month == i).Count();
                }
                else if(SelectedIndexLoaiGiayTo == (int)LoaiGiayTo.GiayChuyenKhau) {
                    countItemsInMonth = DataProvider.Ins.DB.PHIEUTHAYDOI_HK_NK.Where(x => x.NgayKhaiBao.Year == SelectedYear && x.NgayKhaiBao.Month == i).Count();
                }
                ItemsCountedEachMonth.Add(countItemsInMonth);
            }

            ColumnSeries = new SeriesCollection {
                new ColumnSeries {
                    Title = "Số lượng " + SelectedLoaiGiayTo + " tạo mới trong năm " + SelectedYear.ToString(),
                    Values = new ChartValues<int>(ItemsCountedEachMonth),
                    //Values = new ChartValues<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 },        // test data
                    LabelPoint = PointLabel
                }
            };
        }

        private void LoadData()
        {
            // combobox select month & year
            ListMonth = new ObservableCollection<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            SelectedMonth = DateTime.Now.Month;

            ListYear = new ObservableCollection<int>();
            for (int i = DateTime.Now.Year + 2; i >= 2000; i--)
            {
                ListYear.Add(i);
            }
            SelectedYear = DateTime.Now.Year;

            ListLoaiGiayTo = new ObservableCollection<string>();
            ListLoaiGiayTo.Add("Sổ hộ khẩu");
            ListLoaiGiayTo.Add("Giấy tạm vắng");
            ListLoaiGiayTo.Add("Giấy tạm trú");
            ListLoaiGiayTo.Add("Giấy chuyển khẩu");

            // pie chart
            LoadPieChartInput();

            // bar chart
            LoadBarChartInput();
        }
        #endregion

        #region ICommand
        public ICommand DisplayPieChart_Command { get; set; }
        public ICommand DisplayBarChart_Command { get; set; }
        #endregion

        public BaoCao_VM()
        {
            LoadData();

            DisplayPieChart_Command = new RelayCommand((p) => {
                LoadPieChartInput();
            });

            DisplayBarChart_Command = new RelayCommand((p) => {
                LoadBarChartInput();
            });
        }
    }
}
