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

        // 3
        private bool _IsQLNKEnabled;
        public bool IsQLNKEnabled
        {
            get { return _IsQLNKEnabled; }
            set { _IsQLNKEnabled = value; OnPropertyChanged(); }
        }

        private Visibility _VisibilityOfQLNK;
        public Visibility VisibilityOfQLNK
        {
            get { return _VisibilityOfQLNK; }
            set { _VisibilityOfQLNK = value; OnPropertyChanged(); }
        }

        // 4
        private bool _IsQLHKEnabled;
        public bool IsQLHKEnabled
        {
            get { return _IsQLHKEnabled; }
            set { _IsQLHKEnabled = value; OnPropertyChanged(); }
        }

        private Visibility _VisibilityOfQLHK;
        public Visibility VisibilityOfQLHK
        {
            get { return _VisibilityOfQLHK; }
            set { _VisibilityOfQLHK = value; OnPropertyChanged(); }
        }

        // 5
        private bool _IsTKNKEnabled;
        public bool IsTKNKEnabled
        {
            get { return _IsTKNKEnabled; }
            set { _IsTKNKEnabled = value; OnPropertyChanged(); }
        }

        private Visibility _VisibilityOfTKNK;
        public Visibility VisibilityOfTKNK
        {
            get { return _VisibilityOfTKNK; }
            set { _VisibilityOfTKNK = value; OnPropertyChanged(); }
        }

        // 6
        private bool _IsQLTVEnabled;
        public bool IsQLTVEnabled
        {
            get { return _IsQLTVEnabled; }
            set { _IsQLTVEnabled = value; OnPropertyChanged(); }
        }

        private Visibility _VisibilityOfQLTV;
        public Visibility VisibilityOfQLTV
        {
            get { return _VisibilityOfQLTV; }
            set { _VisibilityOfQLTV = value; OnPropertyChanged(); }
        }

        // 7
        private bool _IsQLTTEnabled;
        public bool IsQLTTEnabled
        {
            get { return _IsQLTTEnabled; }
            set { _IsQLTTEnabled = value; OnPropertyChanged(); }
        }

        private Visibility _VisibilityOfQLTT;
        public Visibility VisibilityOfQLTT
        {
            get { return _VisibilityOfQLTT; }
            set { _VisibilityOfQLTT = value; OnPropertyChanged(); }
        }

        // 8
        private bool _IsBaoCaoEnabled;
        public bool IsBaoCaoEnabled
        {
            get { return _IsBaoCaoEnabled; }
            set { _IsBaoCaoEnabled = value; OnPropertyChanged(); }
        }

        private Visibility _VisibilityOfBaoCao;
        public Visibility VisibilityOfBaoCao
        {
            get { return _VisibilityOfBaoCao; }
            set { _VisibilityOfBaoCao = value; OnPropertyChanged(); }
        }

        // 9
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
        public ICommand LogOut_Command { get; set; }
        #endregion

        #region Functions
        private void DisableButtons()
        {
            // disable and hidden all buttons
            IsDashboardEnabled = IsQLHGDEnabled = IsQLNKEnabled = IsQLHKEnabled = IsTKNKEnabled = IsQLTVEnabled = IsQLTTEnabled = IsBaoCaoEnabled = IsDuyetEnabled = false;
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
                case 3:
                    IsQLHKEnabled = true;
                    VisibilityOfQLHK = Visibility.Visible;
                    break;
                case 4:
                    IsQLNKEnabled = true;
                    VisibilityOfQLNK = Visibility.Visible;
                    break;
                case 5:
                    IsTKNKEnabled = true;
                    VisibilityOfTKNK = Visibility.Visible;
                    break;
                case 6:
                    IsQLTVEnabled = true;
                    VisibilityOfQLTV = Visibility.Visible;
                    break;
                case 7:
                    IsQLTTEnabled = true;
                    VisibilityOfQLTT = Visibility.Visible;
                    break;
                case 8:
                    IsBaoCaoEnabled = true;
                    VisibilityOfBaoCao = Visibility.Visible;
                    break;
                case 9:
                    IsDuyetEnabled = true;
                    VisibilityOfDuyet = Visibility.Visible;
                    break;
            }
        }

        private void InitButtonsForUsing(string userID)
        {
            var congAn = DataProvider.Ins.DB.CONGANs.Where(x => x.MaCongAn == userID).SingleOrDefault();
            CONGAN currentUser = congAn as CONGAN;

            DisableButtons();
            var listPermission = DataProvider.Ins.DB.CHITIET_PHANQUYEN.ToList();
            ObservableCollection<CHITIET_PHANQUYEN> ListPermission = new ObservableCollection<CHITIET_PHANQUYEN>(listPermission);
            foreach (var item in ListPermission)
            {
                if (item.MaLoaiCongAn == currentUser.MaLoaiCongAn)
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
                    if (GlobalState.Ins().maCongAn != null)
                    {
                        InitButtonsForUsing(GlobalState.Ins().maCongAn);

                        _timer.Stop();
                    }
                };
                _timer.Start();

                FrameContent = new TrangChu();
                //FrameContent.DataContext = new TrangChu_VM();

                // CuteTN
                AdjustDbOnLoad();
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

            LogOut_Command = new RelayCommand((p) => {
                var dlgRes = System.Windows.MessageBox.Show("Bạn có muốn đăng xuất không?", "Thông báo", MessageBoxButton.YesNo);
                if(dlgRes == MessageBoxResult.Yes) {
                    System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                    Application.Current.Shutdown();
                }
            });
        }

        #region CuteTN: Adjust DB on load
        private void AdjustDbOnLoad()
        {
            foreach(var nk in DataProvider.Ins.DB.NHANKHAUs)
            {
                if(nk != null && nk.BanChinhThuc && !nk.IsDeleted)
                    Utilities.Utils.UpdateChuHoOfShk(nk);
            }

            DataProvider.Ins.DB.SaveChanges();
        }
        #endregion
    }
}
