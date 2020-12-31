﻿using QuanLyHoKhau.Model;
using QuanLyHoKhau.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyHoKhau.ViewModel
{
    class QuanLyHoKhau_VM : BaseViewModel
    {
        #region List So ho khau (full)
        ObservableCollection<SOHOKHAU> LoadSoHoKhaus()
        {
            return new ObservableCollection<SOHOKHAU>(DataProvider.Ins.DB.SOHOKHAUs.ToList());
        }

        ObservableCollection<SOHOKHAU> _listSoHoKhau = null;
        public ObservableCollection<SOHOKHAU> ListSoHoKhau
        {
            get
            {
                if (_listSoHoKhau == null)
                    _listSoHoKhau = LoadSoHoKhaus();
                return _listSoHoKhau;
            }
            set
            {
                _listSoHoKhau = value ?? LoadSoHoKhaus();
                ListFilteredSoHoKhau = SelectFilterSoHoKhaus();
                OnPropertyChanged();
            }

        }
        #endregion

        #region List So ho Khau (filtered)
        private string _searchedText = null;
        public string SearchedText
        {
            get
            {
                if (_searchedText == null)
                    _searchedText = "";
                return _searchedText;
            }

            set
            {
                _searchedText = value;
                ListFilteredSoHoKhau = SelectFilterSoHoKhaus();
                OnPropertyChanged();
            }
        }

        bool IsMatchedSoHoKhau(SOHOKHAU shk)
        {
            if(!string.IsNullOrEmpty(shk.CMNDChuHo))
                if (Regex.IsMatch(shk.CMNDChuHo.ToUpper(), SearchedText.ToUpper()))
                    return true;

            if (Regex.IsMatch(shk.MaSHK.ToUpper(), SearchedText.ToUpper()))
                return true;

            return false;
        }

        ObservableCollection<SOHOKHAU> SelectFilterSoHoKhaus()
        {
            return new ObservableCollection<SOHOKHAU>(ListSoHoKhau.Where(IsMatchedSoHoKhau).ToList());
        }

        ObservableCollection<SOHOKHAU> _listFilteredSoHoKhau = null;
        public ObservableCollection<SOHOKHAU> ListFilteredSoHoKhau
        {
            get
            {
                if (_listFilteredSoHoKhau == null)
                    _listFilteredSoHoKhau = SelectFilterSoHoKhaus();
                return _listFilteredSoHoKhau;
            }
            set
            {
                _listFilteredSoHoKhau = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Button Add SoHoKhau
        public ICommand BtnAddSoHoKhau_Command { get; set; }

        public QuanLyHoKhau_VM()
        {
            BtnAddSoHoKhau_Command = new RelayCommand((p) => {
                NhapHoKhauWindow nhapHoKhauWindow = new NhapHoKhauWindow();
                nhapHoKhauWindow.DataContext = new NhapHoKhau_VM();
                (nhapHoKhauWindow.DataContext as NhapHoKhau_VM).OnDatabaseUpdated = new EventHandler(HandleOnDbUpdated);
                nhapHoKhauWindow.ShowDialog();
            });
        }
        #endregion

        #region Update from database
        void HandleOnDbUpdated(Object sender, EventArgs args)
        {
            Refresh();
        }

        void Refresh()
        {
            ListSoHoKhau = LoadSoHoKhaus();
        }
        #endregion
    }
}
