using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Input;
using QuanLyHoKhau.Model;
using QuanLyHoKhau.Utilities;

namespace QuanLyHoKhau.ViewModel
{
    class NhapHoKhau_VM : BaseViewModel
    {
        // CuteTN Note: the goal is to make this class immutable from the outside :)

        const int SOHOKHAU_ID_LENGTH = 8;
        const string SOHOKHAU_ID_PREFIX = "SHK";

        #region Init
        public NhapHoKhau_VM() : this(null)
        {
        }

        public NhapHoKhau_VM(SOHOKHAU soHoKhau)
        {
            Reset();
            ResultSoHoKhau = new SOHOKHAU(soHoKhau);
        }
        #endregion

        #region fields
        private SOHOKHAU _resultSoHoKhau = null;
        public SOHOKHAU ResultSoHoKhau
        {
            get 
            {
                if(_resultSoHoKhau == null)
                { 
                    _resultSoHoKhau = new SOHOKHAU(null);
                    OnPropertyChanged();
                }
                return _resultSoHoKhau;
            }
            set { _resultSoHoKhau = value; OnPropertyChanged(); }
        }

        public string MaCongAn
        {
            get => ResultSoHoKhau.MaCongAn;
            set { ResultSoHoKhau.MaCongAn = value; OnPropertyChanged(); }
        }

        public string MaSoHoKhau
        {
            get => ResultSoHoKhau.MaSHK;
            set { ResultSoHoKhau.MaSHK = value; OnPropertyChanged(); }
        }

        public NHANKHAU SelectedChuHo
        {
            get => ResultSoHoKhau.NHANKHAU;
            set
            {
                ResultSoHoKhau.NHANKHAU = value;

                if (value != null)
                    ResultSoHoKhau.CMNDChuHo = value.CMND;
                else
                    ResultSoHoKhau.CMNDChuHo = null;

                OnPropertyChanged();
            }
        }

        public SOLUUNHANKHAU SelectedSoLuuNhanKhau
        {
            get => ResultSoHoKhau.SOLUUNHANKHAU;
            set 
            { 
                ResultSoHoKhau.SOLUUNHANKHAU = value; 
                
                if(value != null)
                    ResultSoHoKhau.MaSoLuuNhanKhau = value.MaSoLuuNhanKhau;
                else
                    ResultSoHoKhau.MaSoLuuNhanKhau = null;

                OnPropertyChanged(); 
            }
        }

        public string SoCmndChuHo
        {
            get => ResultSoHoKhau.CMNDChuHo;
            set { ResultSoHoKhau.CMNDChuHo = value; OnPropertyChanged(); }
        }

        public string DiaChi
        {
            get => ResultSoHoKhau.DiaChi;
            set { ResultSoHoKhau.DiaChi = value; OnPropertyChanged(); }
        }
        #endregion

        #region ListSOLUUNHANKHAU
        private BindingList<SOLUUNHANKHAU> LoadSLNK()
        {
            BindingList<SOLUUNHANKHAU> result = new BindingList<SOLUUNHANKHAU>(DataProvider.Ins.DB.SOLUUNHANKHAUs.ToList());
            return result;
        }

        private BindingList<SOLUUNHANKHAU> _listSOLUUNHANKHAU = null;
        public BindingList<SOLUUNHANKHAU> ListSOLUUNHANKHAU
        {
            get
            {
                if (_listSOLUUNHANKHAU == null)
                    _listSOLUUNHANKHAU = LoadSLNK();
                return _listSOLUUNHANKHAU;
            }
            set
            {
                _listSOLUUNHANKHAU = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region ListNHANKHAU
        private BindingList<NHANKHAU> LoadNhanKhau()
        {
            BindingList<NHANKHAU> result = new BindingList<NHANKHAU>(DataProvider.Ins.DB.NHANKHAUs.ToList());
            return result;
        }

        private BindingList<NHANKHAU> _listNHANKHAU = null;
        public BindingList<NHANKHAU> ListNHANKHAU
        {
            get
            {
                if (_listNHANKHAU == null)
                    _listNHANKHAU = LoadNhanKhau();
                return _listNHANKHAU;
            }
            set
            {
                _listNHANKHAU = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Confirm Button
        ICommand _cmdConfirm = null;
        public ICommand CmdConfirm
        { 
            get => _cmdConfirm != null? _cmdConfirm : new RelayCommand(HandleConfirmButton);
            set { _cmdConfirm = value; OnPropertyChanged(); }
        }

        void HandleConfirmButton(Object obj)
        {
            string error;

            if(ValidateResult(out error))
            { 
                if(string.IsNullOrEmpty(MaSoHoKhau))
                    AddNewSoHoKhauToDB();

                Reset();
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
            Reset();
        }
        #endregion

        #region Test
        private void TestPrintResult()
        {
            Debug.WriteLine(
                $"[= Test so ho khau ===========]\n"
                + $"    Ma so ho khau: {ResultSoHoKhau.MaSHK}\n"
                + $"    CMND chu ho: {ResultSoHoKhau.CMNDChuHo}\n"
                + $"    Ma cong an: {ResultSoHoKhau.MaCongAn}\n"
                + $"    Ma so luu nhan khau: {ResultSoHoKhau.MaSoLuuNhanKhau}\n"
                + $"    Dia chi: {ResultSoHoKhau.DiaChi}\n"
                + $"    Ngay tao so: {ResultSoHoKhau.NgayTaoSo}\n"
                + $"    Da xoa: {ResultSoHoKhau.IsDeleted}\n"
                + $"[==============================]\n"
            );
        }
        #endregion

        #region Adjust and Validation
        private void AdjustResult()
        {
            if(MaCongAn == "")
                MaCongAn = null;
            if(SoCmndChuHo == "")
                SoCmndChuHo = null;
        }

        private bool ValidateResult(out string errors)
        {
            if(!string.IsNullOrEmpty(MaCongAn))
                if(DataProvider.Ins.DB.CONGANs.Find(MaCongAn) == null)
                {
                    errors = $"Mã công an {MaCongAn} không tồn tại";
                    return false;
                }

            if(!string.IsNullOrEmpty(SoCmndChuHo))
            {
                var filteredNguoi = DataProvider.Ins.DB.NGUOIs.Where(nguoi => (nguoi.CMND == SoCmndChuHo));

                // check if there is a person has this cmnd
                if (filteredNguoi.Count() == 0)
                {
                    errors = $"Công dân với số CMND {SoCmndChuHo} không tồn tại";
                    return false;
                }

                // check if this person is actually a NHANKHAU
                var nguoiDo = filteredNguoi.First();
                var filteredNhanKhau = DataProvider.Ins.DB.NHANKHAUs.Where(nhankhau => (nhankhau.CMND == nguoiDo.CMND));

                if(filteredNguoi.Count() == 0)
                {
                    errors = $"Công dân với số CMND {SoCmndChuHo} không phải là nhân khẩu";
                    return false;
                }
            }

            errors = "";
            return true;
        }
        #endregion

        #region database
        private void AddNewSoHoKhauToDB()
        {
            AdjustResult();
            MaSoHoKhau = Utils.GenerateNewId(DataProvider.Ins.DB.SOHOKHAUs, SOHOKHAU_ID_PREFIX, SOHOKHAU_ID_LENGTH);
            TestPrintResult();
            DataProvider.Ins.DB.SOHOKHAUs.Add(ResultSoHoKhau);
            DataProvider.Ins.DB.SaveChanges();
            OnDatabaseUpdated?.Invoke(this, null);
        }
        #endregion

        #region Utils
        private void Reset()
        {
            ResultSoHoKhau = null;
            MaSoHoKhau = null;
            SelectedSoLuuNhanKhau = null;
            SelectedChuHo = null;
            DiaChi = "";
            Refresh();
        }

        public void Refresh()
        {
            ListSOLUUNHANKHAU = LoadSLNK();
            ListNHANKHAU = LoadNhanKhau();
        }
        #endregion

        #region Events
        public EventHandler OnDatabaseUpdated = null;
        #endregion
    }
}
