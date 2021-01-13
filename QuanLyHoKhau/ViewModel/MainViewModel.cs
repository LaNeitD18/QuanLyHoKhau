using QuanLyHoKhau.Model;
using QuanLyHoKhau.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace QuanLyHoKhau.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        #region Variables
        private Page _FrameContent;
        public Page FrameContent
        {
            get { return _FrameContent; }
            set { _FrameContent = value; OnPropertyChanged(); }
        }

        static public DispatcherTimer _timer;

        public bool isLoaded = false;

        // 1
        private bool _IsDashboardEnabled;
        public bool IsDashboardEnabled
        {
            get { return _IsDashboardEnabled; }
            set { _IsDashboardEnabled = value; OnPropertyChanged(); }
        }

        private Visibility _VisibilityOfDashboard;
        public Visibility VisibilityOfDashboard
        {
            get { return _VisibilityOfDashboard; }
            set { _VisibilityOfDashboard = value; OnPropertyChanged(); }
        }

        // 2
        private bool _IsQLHGDEnabled;
        public bool IsQLHGDEnabled
        {
            get { return _IsQLHGDEnabled; }
            set { _IsQLHGDEnabled = value; OnPropertyChanged(); }
        }

        private Visibility _VisibilityOfQLHGD;
        public Visibility VisibilityOfQLHGD
        {
            get { return _VisibilityOfQLHGD; }
            set { _VisibilityOfQLHGD = value; OnPropertyChanged(); }
        }

        // 8
        private bool _IsDuyetEnabled;
        public bool IsDuyetEnabled
        {
            get { return _IsDuyetEnabled; }
            set { _IsDuyetEnabled = value; OnPropertyChanged(); }
        }

        private Visibility _VisibilityOfDuyet;
        public Visibility VisibilityOfDuyet
        {
            get { return _VisibilityOfDuyet; }
            set { _VisibilityOfDuyet = value; OnPropertyChanged(); }
        }
        #endregion

        #region ICommand
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand TrangChu_SelectedCommand { get; set; }
        public ICommand QLNhanKhau_Page_SelectedCommand { get; set; }
        public ICommand QLHoKhau_Page_SelectedCommand { get; set; }
        public ICommand QLGiayTamVang_Page_SelectedCommand { get; set; }
        public ICommand QLGiayTamTru_Page_SelectedCommand { get; set; }
        public ICommand Duyet_Page_SelectedCommand { get; set; }
        public ICommand BaoCao_Page_SelectedCommand { get; set; }
        public ICommand ThongKeNK_Page_SelectedCommand { get; set; }
        #endregion

        #region Functions
        private void DisableButtons()
        {
            // disable and hidden all buttons
            IsDuyetEnabled = false;
            VisibilityOfDuyet = Visibility.Hidden;
        }

        private void InitButton(int featureID)
        {
            switch (featureID)
            {
                case 1:
                    IsDashboardEnabled = true;
                    VisibilityOfDashboard = Visibility.Visible;
                    break;
                case 2:
                    IsQLHGDEnabled = true;
                    VisibilityOfQLHGD = Visibility.Visible;
                    break;
                //case 3:
                //    IsRoomEnabled = true;
                //    IsRoomVisible = Visibility.Visible;
                //    break;
                //case 4:
                //    IsTimetableInputEnabled = true;
                //    IsTimetableInputVisible = Visibility.Visible;
                //    break;
                //case 5:
                //    IsEventInputEnabled = true;
                //    IsEventInputVisible = Visibility.Visible;
                //    break;
                //case 6:
                //    IsExamInputEnabled = true;
                //    IsExamInputVisible = Visibility.Visible;
                //    break;
                //case 7:
                //    IsRoomManagementEnabled = true;
                //    IsRoomManagementVisible = Visibility.Visible;
                //    break;
                case 8:
                    IsDuyetEnabled = true;
                    VisibilityOfDuyet = Visibility.Visible;
                    break;
            }
        }

        private void InitButtonsForUsing(string userID)
        {
            DisableButtons();
            ObservableCollection<CHITIET_PHANQUYEN> listPermission = new ObservableCollection<CHITIET_PHANQUYEN>(DataProvider.Ins.DB.CHITIET_PHANQUYEN);
            foreach (var item in listPermission)
            {
                if (item.MaLoaiCongAn == userID)
                {
                    int id = Convert.ToInt32(item.MaQuyen);
                    InitButton(id);
                }
            }
        }
        #endregion

        public MainViewModel()
        {
            LoadedWindowCommand = new RelayCommand((p) => {
                if (p == null)
                    MessageBox.Show(p.GetType().Name);
                Window mainWindow = p as Window;
                mainWindow.Hide(); // main view hide in login window
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();
                isLoaded = true;

                //if (loginWindow.DataContext == null) return;
                //var loginVM = loginWindow.DataContext as LoginViewModel;
                //if (loginVM.isLogin)
                //{
                mainWindow.Show();
                //    LoadRemainsData(); // show remain table
                //}
                //else
                //{

                //}

                _timer = new DispatcherTimer(DispatcherPriority.Render);
                _timer.Interval = TimeSpan.FromSeconds(1);
                _timer.Tick += (sender, args) =>
                {
                    if (GlobalState.Ins() != null)
                    {
                        InitButtonsForUsing(GlobalState.Ins().maCongAn);

                        _timer.Stop();
                    }
                };
                _timer.Start();

                FrameContent = new TrangChu();
                //FrameContent.DataContext = new TrangChu_VM();
            });

            TrangChu_SelectedCommand = new RelayCommand((p) => {
                //Title = "Trang chủ";
                FrameContent = new TrangChu();
                FrameContent.DataContext = new TrangChu_VM();
            });

            QLNhanKhau_Page_SelectedCommand = new RelayCommand((p) => {
                //Title = "Trang chủ";
                FrameContent = new QuanLyNhanKhauPage();
                FrameContent.DataContext = new QuanLyNhanKhau_VM();
            });

            QLHoKhau_Page_SelectedCommand = new RelayCommand((p) => {
                //Title = "Trang chủ";
                FrameContent = new QuanLyHoKhauPage();
                FrameContent.DataContext = new QuanLyHoKhau_VM();
            });

            QLGiayTamVang_Page_SelectedCommand = new RelayCommand((p) => {
                //Title = "Trang chủ";
                FrameContent = new QuanLyTamVangPage();
                FrameContent.DataContext = new QuanLyTamVang_VM();
            });

            QLGiayTamTru_Page_SelectedCommand = new RelayCommand((p) => {
                //Title = "Trang chủ";
                FrameContent = new QuanLyTamTruPage();
                FrameContent.DataContext = new QuanLyTamTru_VM();
            });

            Duyet_Page_SelectedCommand = new RelayCommand((p) => {
                //Title = "Trang chủ";
                FrameContent = new DuyetPage();
                FrameContent.DataContext = new Duyet_VM();
            });

            BaoCao_Page_SelectedCommand = new RelayCommand((p) => {
                //Title = "Trang chủ";
                FrameContent = new BaoCaoPage();
                FrameContent.DataContext = new BaoCao_VM();
            });

            ThongKeNK_Page_SelectedCommand = new RelayCommand((p) => {
                //Title = "Trang chủ";
                FrameContent = new ThongKeNhanKhauPage();
                FrameContent.DataContext = new ThongKeNhanKhau_VM();
            });
        }
    }
}
