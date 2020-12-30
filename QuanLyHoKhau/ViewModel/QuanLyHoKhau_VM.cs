using QuanLyHoKhau.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyHoKhau.ViewModel
{
    class QuanLyHoKhau_VM : BaseViewModel
    {
        public ICommand BtnAddSoHoKhau_Command { get; set; }

        public QuanLyHoKhau_VM()
        {
            BtnAddSoHoKhau_Command = new RelayCommand((p) => {
                NhapHoKhauWindow nhapHoKhauWindow = new NhapHoKhauWindow();
                nhapHoKhauWindow.DataContext = new NhapHoKhau_VM();
                nhapHoKhauWindow.ShowDialog();
            });
        }
    }
}
