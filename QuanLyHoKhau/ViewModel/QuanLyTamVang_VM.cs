using QuanLyHoKhau.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyHoKhau.ViewModel
{
    class QuanLyTamVang_VM : BaseViewModel
    {
        public ICommand BtnAddTamVang_Command { get; set; }

        public QuanLyTamVang_VM()
        {
            BtnAddTamVang_Command = new RelayCommand((p) => {
                NhapGiayTamVangWindow nhapGiayTamVangWindow = new NhapGiayTamVangWindow();
                nhapGiayTamVangWindow.ShowDialog();
            });
        }
    }
}
