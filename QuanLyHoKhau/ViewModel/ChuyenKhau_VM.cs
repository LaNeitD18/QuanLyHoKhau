using QuanLyHoKhau.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using QuanLyHoKhau.Utilities;

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

            // crash
            //BindingList<NHANKHAU> result = new BindingList<NHANKHAU>
            //    (
            //        shk.NHANKHAUs.Where(nk => nk.BanChinhThuc && !nk.IsDeleted).ToList()
            //    );

            BindingList<NHANKHAU> result = new BindingList<NHANKHAU>
            (
                DataProvider.Ins.DB.NHANKHAUs.Where(nk =>
                    (nk.BanChinhThuc) && (!nk.IsDeleted) && (nk.MASHK == shk.MaSHK)
                ).ToList()
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

        #region ListSelectedNHANKHAUinFromSHK
        private ObservableCollection<NHANKHAU> _listSelectedNHANKHAUinFromSHK = new ObservableCollection<NHANKHAU>();
        public ObservableCollection<NHANKHAU> ListSelectedNHANKHAUinFromSHK
        {
            get => _listSelectedNHANKHAUinFromSHK;
            set
            {
                _listSelectedNHANKHAUinFromSHK = value;
                ListNHANKHAUinFromSHKDataGrid_OnSelectionChanged();
                OnPropertyChanged();
            }
        }

        private ICommand _listNHANKHAUinFromSHKDataGrid_SelectionChangedCmd = null;
        public ICommand ListNHANKHAUinFromSHKDataGrid_SelectionChangedCmd
        {
            get
            {
                if (_listNHANKHAUinFromSHKDataGrid_SelectionChangedCmd == null)
                    _listNHANKHAUinFromSHKDataGrid_SelectionChangedCmd = new RelayCommand(obj => ListNHANKHAUinFromSHKDataGrid_OnSelectionChanged(obj));
                return _listNHANKHAUinFromSHKDataGrid_SelectionChangedCmd;
            }
            set
            {
                _listNHANKHAUinFromSHKDataGrid_SelectionChangedCmd = value;
                OnPropertyChanged();
            }
        }

        private void GetDataGridSelectedItems(System.Windows.Controls.DataGrid datagrid)
        {
            ListSelectedNHANKHAUinFromSHK.Clear();

            foreach (var item in datagrid.SelectedItems)
            {
                NHANKHAU nk = (NHANKHAU)item;
                if (nk != null && !ListSelectedNHANKHAUinFromSHK.Contains(nk))
                    ListSelectedNHANKHAUinFromSHK.Add(nk);
            }
        }

        private void ListNHANKHAUinFromSHKDataGrid_OnSelectionChanged(Object obj = null)
        {
            System.Windows.Controls.DataGrid dataGrid = obj as System.Windows.Controls.DataGrid;
            if (dataGrid == null)
                return;

            GetDataGridSelectedItems(dataGrid);
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
            string error;

            if (Validate(out error))
            {
                FilterAllNhanKhauToChuyenKhau().ForEach(AddPendingChuyenKhau);
                DataProvider.Ins.DB.SaveChanges();
                (obj as System.Windows.Window)?.Close();
            }
            else
            {
                System.Windows.MessageBox.Show(error, "Lỗi", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
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

        #region ChuyenKhau Button
        ICommand _cmdChuyenKhau = null;
        public ICommand CmdChuyenKhau
        {
            get => _cmdChuyenKhau != null ? _cmdConfirm : new RelayCommand(HandleChuyenKhauButton);
            set { _cmdChuyenKhau = value; OnPropertyChanged(); }
        }

        void HandleChuyenKhauButton(Object obj)
        {
            string error;

            if (ValidateForChuyenKhau(out error))
            {
                List<NHANKHAU> temp = new List<NHANKHAU>(ListSelectedNHANKHAUinFromSHK);
                foreach (var nk in temp)
                {
                    ChuyenKhauForPreviewing(nk);
                }
            }
            else
            {
                System.Windows.MessageBox.Show(error, "Lỗi", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }
        #endregion

        #region Reset Button
        ICommand _cmdReset = null;
        public ICommand CmdReset
        {
            get => _cmdReset != null ? _cmdConfirm : new RelayCommand(HandleResetButton);
            set { _cmdReset = value; OnPropertyChanged(); }
        }

        void HandleResetButton(Object obj)
        {
            ResetChuyenKhau();
        }
        #endregion

        #region Logics
        private bool ValidateForChuyenKhau(out string errors)
        {
            errors = "";

            if(SelectedFromSoHoKhau == null)
            {
                errors = "Vui lòng chọn hộ khẩu hiện tại";
                return false;
            }

            if(SelectedToSoHoKhau == null)
            {
                errors = "Vui lòng chọn hộ khẩu chuyển đến";
                return false;
            }

            if(SelectedFromSoHoKhau == SelectedToSoHoKhau)
            {
                errors = "Sổ hộ khẩu chuyển đến không được trùng với sổ hộ khẩu hiên tại";
                return false;
            }

            if(ListSelectedNHANKHAUinFromSHK.Count == 0)
            {
                errors = "Vui lòng chọn nhân khẩu cần chuyển từ sổ hộ khẩu hiện tại";
                return false;
            }

            return true;
        }

        private void ResetChuyenKhau()
        {
            ListNHANKHAUinFromSHK = LoadNhanKhauInSHK(SelectedFromSoHoKhau);
            ListNHANKHAUinToSHK = LoadNhanKhauInSHK(SelectedToSoHoKhau);
        }

        private void ChuyenKhauForPreviewing(NHANKHAU nk)
        {
            ListNHANKHAUinFromSHK.Remove(nk);
            ListNHANKHAUinToSHK.Add(nk);
        }

        private List<NHANKHAU> FilterAllNhanKhauToChuyenKhau()
        {
            var result = new List<NHANKHAU>();

            foreach(var nk in ListNHANKHAUinToSHK)
            {
                if(nk.MASHK != SelectedToSoHoKhau.MaSHK)
                {
                    result.Add(nk);
                }
            }

            return result;
        }

        private void AddPendingChuyenKhau(NHANKHAU nk)
        {
            if(nk == null)
                return;

            PHIEUDUYETCHUYENKHAU pdck = new PHIEUDUYETCHUYENKHAU()
            {
                MaPhieuThayDoi = Utils.GenerateNewId(DataProvider.Ins.DB.PHIEUDUYETCHUYENKHAUs, "PDCK_", 8),
                NgayTao = DateTime.Now,
                IsDeleted = false,
                DaDuyet = false,

                MaSoLuuChuyenKhau = SelectedSoLuuChuyenKhau.MaSoLuuChuyenKhau,
                CMND = nk.CMND,
                MaSHKChuyenDen = SelectedToSoHoKhau.MaSHK,
            };

            DataProvider.Ins.DB.PHIEUDUYETCHUYENKHAUs.Add(pdck);
        }

        private bool Validate(out string errors)
        {
            errors = "";

            if(SelectedSoLuuChuyenKhau == null)
            {
                errors = "Vui lòng chọn sổ lưu nhân khẩu";
                return false;
            }

            return true;
        }

        #endregion
    }
}
