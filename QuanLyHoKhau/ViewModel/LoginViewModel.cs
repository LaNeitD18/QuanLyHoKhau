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

namespace QuanLyHoKhau.ViewModel
{
    class LoginViewModel : BaseViewModel
    {
        #region Variables
        private string _UserName;

        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; OnPropertyChanged(); }
        }

        private string _Password;

        public string Password
        {
            get { return _Password; }
            set { _Password = value; OnPropertyChanged(); }
        }

        #endregion

        #region ICommand
        public ICommand PasswordChangedCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        #endregion


        public LoginViewModel()
        {
            PasswordChangedCommand = new RelayCommand((p) => {
                PasswordBox passwordBox = p as PasswordBox;
                Password = passwordBox.Password;
            });

            LoginCommand = new RelayCommand((p) => {
                if (UserName == null || Password == null) {
                    MessageBox.Show("Chưa nhập thông tin tài khoản.");
                    return;
                }

                ObservableCollection<CONGAN> Account = new ObservableCollection<CONGAN>(DataProvider.Ins.DB.CONGANs);

                foreach (var item in Account)
                {
                    if (item.MaCongAn == UserName && item.MatKhau == Password)
                    {
                        LoginWindow loginWindow = p as LoginWindow;
                        //Gan static TaiKhoanSuDung
                        //TaiKhoanSuDung = item;
                        //MessageBox.Show("Đăng nhập thành công");
                        loginWindow.Close();
                        return;
                    }

                }
                MessageBox.Show("Tài khoản không hợp lệ!");
            });

            ExitCommand = new RelayCommand((p) => {
                LoginWindow loginWindow = p as LoginWindow;
                loginWindow.Close();
                System.Environment.Exit(1);
            });
        }
    }
}
