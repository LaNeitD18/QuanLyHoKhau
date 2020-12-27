using QuanLyHoKhau.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyHoKhau.ViewModel
{
    class QuanLyTamTru_VM : BaseViewModel
    {
        public ICommand BtnAddTamTru_Command { get; set; }

        public QuanLyTamTru_VM()
        {
            BtnAddTamTru_Command = new RelayCommand((p) => {
                NhapGiayTamTruWindow nhapGiayTamTruWindow = new NhapGiayTamTruWindow();
                nhapGiayTamTruWindow.ShowDialog();
            });
        }
    }
}
