using QuanLyHoKhau.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyHoKhau.ViewModel
{
    class QuanLyNhanKhau_VM : BaseViewModel
    {
        public ICommand BtnAddNhanKhau_Command { get; set; }

        public QuanLyNhanKhau_VM()
        {
            BtnAddNhanKhau_Command = new RelayCommand((p) => {
                NhapNhanKhauWindow nhapNhanKhauWindow = new NhapNhanKhauWindow();
                nhapNhanKhauWindow.ShowDialog();
            });
        }
    }
}
