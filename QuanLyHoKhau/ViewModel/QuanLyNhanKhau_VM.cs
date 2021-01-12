using QuanLyHoKhau.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using QuanLyHoKhau.Model;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Windows;
using QuanLyHoKhau.Utilities;

namespace QuanLyHoKhau.ViewModel
{
    class QuanLyNhanKhau_VM : BaseViewModel
    {
        #region List Nhan khau (full)
        ObservableCollection<NHANKHAU> LoadNhanKhaus()
        {
            return new ObservableCollection<NHANKHAU>(DataProvider.Ins.DB.NHANKHAUs.Where(nk => nk.BanChinhThuc && !nk.IsDeleted).ToList());
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
                ListFilteredNhanKhau = SelectFilterNhanKhaus();
                OnPropertyChanged();
            }

        }
        #endregion

        #region List Nhan Khau (filtered)
        private string _searchedText = null;
        public string SearchedText
        {
            get
            {
                if(_searchedText == null)
                    _searchedText = "";
                return _searchedText;
            }

            set
            {
                _searchedText = value;
                ListFilteredNhanKhau = SelectFilterNhanKhaus();
                OnPropertyChanged();
            }
        }

        bool IsMatchedNhanKhau(NHANKHAU nk)
        {
            if(Regex.IsMatch(nk.CMND.ToUpper(), SearchedText.ToUpper()))
                return true;

            if(Regex.IsMatch(nk.NGUOI.Ten.ToUpper(), SearchedText.ToUpper()))
                return true;

            return false;
        }

        ObservableCollection<NHANKHAU> SelectFilterNhanKhaus()
        {
            return new ObservableCollection<NHANKHAU>(ListNhanKhau.Where(IsMatchedNhanKhau).ToList());
        }

        ObservableCollection<NHANKHAU> _listFilteredNhanKhau = null;
        public ObservableCollection<NHANKHAU> ListFilteredNhanKhau
        {
            get
            {
                if(_listFilteredNhanKhau == null)
                    _listFilteredNhanKhau = SelectFilterNhanKhaus();
                return _listFilteredNhanKhau;
            }
            set
            {
                _listFilteredNhanKhau = value;
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
                (nhapNhanKhauWindow.DataContext as NhapNhanKhau_VM).OnDatabaseUpdated = new EventHandler(HandleOnDbUpdated);
                nhapNhanKhauWindow.ShowDialog();
            });
        }
        #endregion

        #region Button Delete Nhan Khau
        public void DeleteNhanKhau(Object item)
        {
            NHANKHAU nk = item as NHANKHAU;
            if (nk == null)
                return;

            MessageBoxResult msgRes = MessageBox.Show($"Bạn có chắc muốn xoá thông tin nhân khẩu {nk.CMND} không?", "Xoá", MessageBoxButton.YesNo);
            if (msgRes == MessageBoxResult.Yes)
            {
                //nk.IsDeleted = true;
                
                //if(nk.NGUOI != null)
                //    nk.NGUOI.IsDeleted = true;

                //Utils.RemoveInvalidChuHoInSHKs();

                AddPendingDeleteNhanKhau(nk);

                DataProvider.Ins.DB.SaveChanges();
                Refresh();
            }
        }

        private void AddPendingDeleteNhanKhau(NHANKHAU nk)
        {
            if(nk == null)
                return;

            PHIEUDUYETNHANKHAU pdnk = new PHIEUDUYETNHANKHAU()
            {
                MaPD_NK = Utils.GenerateNewId(DataProvider.Ins.DB.PHIEUDUYETNHANKHAUs, "PDNK_", 8),
                NgayTao = DateTime.Now,
                IsDeleted = false,

                MaNK = nk.CMND,
                MaNK_PendingInfo = nk.CMND,

                ActionType = DuyetActionTypes.Remove,
                DaDuyet = false,
            };

            DataProvider.Ins.DB.PHIEUDUYETNHANKHAUs.Add(pdnk);
        }
        #endregion

        #region Button Edit Nhan Khau
        public void EditNhanKhau(Object item)
        {
            NHANKHAU nk = item as NHANKHAU;
            if (nk == null)
                return;

            NhapNhanKhauWindow nhapNhanKhauWindow = new NhapNhanKhauWindow();
            nhapNhanKhauWindow.DataContext = new NhapNhanKhau_VM(nk.NGUOI, nk);
            (nhapNhanKhauWindow.DataContext as NhapNhanKhau_VM).OnDatabaseUpdated = new EventHandler(HandleOnDbUpdated);
            nhapNhanKhauWindow.ShowDialog();

            DataProvider.Ins.DB.SaveChanges();
            Refresh();
        }
        #endregion

        #region Update from database
        void HandleOnDbUpdated(Object sender, EventArgs args)
        {
            Refresh();
        }

        void Refresh()
        {
            ListNhanKhau = LoadNhanKhaus();
        }
        #endregion
    }
}
