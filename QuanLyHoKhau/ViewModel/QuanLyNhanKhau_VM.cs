using QuanLyHoKhau.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using QuanLyHoKhau.Model;

namespace QuanLyHoKhau.ViewModel
{
    class QuanLyNhanKhau_VM : BaseViewModel
    {
        #region List Nhan khau
        ObservableCollection<NHANKHAU> LoadNhanKhaus()
        {
            return new ObservableCollection<NHANKHAU>(DataProvider.Ins.DB.NHANKHAUs.ToList());
        }

        ObservableCollection<NHANKHAU> _listNhanKhau = null;
        public ObservableCollection<NHANKHAU> ListNhanKhau
        {
            get
            {
                if(_listNhanKhau == null)
                    _listNhanKhau = LoadNhanKhaus();
                return _listNhanKhau;
            }
            set
            {
                _listNhanKhau = value ?? LoadNhanKhaus();
                OnPropertyChanged();
            }

        }
        #endregion

        #region Button add NhanKhau
        public ICommand BtnAddNhanKhau_Command { get; set; }

        public QuanLyNhanKhau_VM()
        {
            BtnAddNhanKhau_Command = new RelayCommand((p) => {
                NhapNhanKhauWindow nhapNhanKhauWindow = new NhapNhanKhauWindow();
                nhapNhanKhauWindow.DataContext = new NhapNhanKhau_VM();
                (nhapNhanKhauWindow.DataContext as NhapNhanKhau_VM).OnDatabaseUpdated = new EventHandler(UpdateFromDb);
                nhapNhanKhauWindow.ShowDialog();
            });
        }
        #endregion

        #region Update from database
        void UpdateFromDb(Object sender, EventArgs args)
        {
            ListNhanKhau = LoadNhanKhaus();
        }
        #endregion
    }
}
