using QuanLyHoKhau.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyHoKhau.ViewModel
{
    class ChuyenKhau_VM : BaseViewModel
    {
        #region Fields
        private SOLUUCHUYENKHAU _selectedSoLuuChuyenKhau = null;
        public SOLUUCHUYENKHAU SelectedSoLuuChuyenKhau
        {
            get => _selectedSoLuuChuyenKhau;
            set { _selectedSoLuuChuyenKhau = value; OnPropertyChanged(); }
        }

        private SOHOKHAU _selectedFromSoHoKhau = null;
        public SOHOKHAU SelectedFromSoHoKhau
        {
            get => _selectedFromSoHoKhau;
            set
            {
                _selectedFromSoHoKhau = value;
                ListNHANKHAUinFromSHK = LoadNhanKhauInSHK(value);
                OnPropertyChanged();
            }
        }

        private SOHOKHAU _selectedToSoHoKhau = null;
        public SOHOKHAU SelectedToSoHoKhau
        {
            get => _selectedToSoHoKhau;
            set
            {
                _selectedToSoHoKhau = value;
                ListNHANKHAUinToSHK = LoadNhanKhauInSHK(value);
                OnPropertyChanged();
            }
        }

        #endregion

        #region ListSOLUUCHUYENKHAU
        private BindingList<SOLUUCHUYENKHAU> LoadSLCKs()
        {
            BindingList<SOLUUCHUYENKHAU> result = new BindingList<SOLUUCHUYENKHAU>(DataProvider.Ins.DB.SOLUUCHUYENKHAUs.ToList());
            return result;
        }

        private BindingList<SOLUUCHUYENKHAU> _listSOLUUCHUYENKHAU = null;
        public BindingList<SOLUUCHUYENKHAU> ListSOLUUCHUYENKHAU
        {
            get
            {
                if (_listSOLUUCHUYENKHAU == null)
                    _listSOLUUCHUYENKHAU = LoadSLCKs();
                return _listSOLUUCHUYENKHAU;
            }
            set
            {
                _listSOLUUCHUYENKHAU = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region ListSOHOKHAU
        private BindingList<SOHOKHAU> LoadSoHoKhau()
        {
            BindingList<SOHOKHAU> result = new BindingList<SOHOKHAU>(DataProvider.Ins.DB.SOHOKHAUs.Where(shk => shk.BanChinhThuc && !shk.IsDeleted).ToList());
            return result;
        }

        private BindingList<SOHOKHAU> _listSOHOKHAU = null;
        public BindingList<SOHOKHAU> ListSOHOKHAU
        {
            get
            {
                if (_listSOHOKHAU == null)
                    _listSOHOKHAU = LoadSoHoKhau();
                return _listSOHOKHAU;
            }
            set
            {
                _listSOHOKHAU = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region ListNHANKHAUinSHK
        private BindingList<NHANKHAU> LoadNhanKhauInSHK(SOHOKHAU shk)
        {
            if (shk == null || shk.IsDeleted)
                return new BindingList<NHANKHAU>();

            BindingList<NHANKHAU> result = new BindingList<NHANKHAU>
                (
                    shk.NHANKHAUs.Where(nk => nk.BanChinhThuc && !nk.IsDeleted).ToList()
                );

            return result;
        }

        private BindingList<NHANKHAU> _listNHANKHAUinFromSHK = null;
        public BindingList<NHANKHAU> ListNHANKHAUinFromSHK
        {
            get
            {
                if (_listNHANKHAUinFromSHK == null)
                    _listNHANKHAUinFromSHK = LoadNhanKhauInSHK(SelectedFromSoHoKhau);
                return _listNHANKHAUinFromSHK;
            }
            set
            {
                _listNHANKHAUinFromSHK = value;
                OnPropertyChanged();
            }
        }

        private BindingList<NHANKHAU> _listNHANKHAUinToSHK = null;
        public BindingList<NHANKHAU> ListNHANKHAUinToSHK
        {
            get
            {
                if (_listNHANKHAUinToSHK == null)
                    _listNHANKHAUinToSHK = LoadNhanKhauInSHK(SelectedToSoHoKhau);
                return _listNHANKHAUinToSHK;
            }
            set
            {
                _listNHANKHAUinToSHK = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Confirm Button
        ICommand _cmdConfirm = null;
        public ICommand CmdConfirm
        {
            get => _cmdConfirm != null ? _cmdConfirm : new RelayCommand(HandleConfirmButton);
            set { _cmdConfirm = value; OnPropertyChanged(); }
        }

        void HandleConfirmButton(Object obj)
        {
            System.Windows.MessageBox.Show("xac nhan cai ne!");
            (obj as System.Windows.Window)?.Close();
            //string error;

            //if (ValidateResult(out error))
            //{
            //    UpsertResult();
            //    (obj as System.Windows.Window)?.Close();
            //}
            //else
            //{
            //    System.Windows.MessageBox.Show(error, "Lỗi", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            //}
        }
        #endregion

        #region Cancel button
        ICommand _cmdCancel = null;
        public ICommand CmdCancel
        {
            get => _cmdCancel != null ? _cmdConfirm : new RelayCommand(HandleCancelButton);
            set { _cmdCancel = value; OnPropertyChanged(); }
        }

        void HandleCancelButton(Object obj)
        {
            (obj as System.Windows.Window)?.Close();
        }
        #endregion
    }
}
