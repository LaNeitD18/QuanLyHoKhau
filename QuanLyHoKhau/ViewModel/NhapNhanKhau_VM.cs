using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using QuanLyHoKhau.Model;
using QuanLyHoKhau.Utilities;

namespace QuanLyHoKhau.ViewModel
{
    class NhapNhanKhau_VM : BaseViewModel
    {
        #region Init
        bool isAddingMode = true;

        private bool _isCmndReadOnly = false;
        public bool IsCmndReadOnly
        {
            get => _isCmndReadOnly;
            set { _isCmndReadOnly = value; OnPropertyChanged(); }
        }

        public NhapNhanKhau_VM() : this(null, null)
        {
        }

        public NhapNhanKhau_VM(NGUOI nguoi, NHANKHAU nhanKhau)
        {
            isAddingMode = (nguoi == null) || (nhanKhau == null);
            IsCmndReadOnly = !isAddingMode;

            Reset();
            LoadInfo(nguoi, nhanKhau);
        }

        private void LoadInfo(NGUOI nguoi, NHANKHAU nhanKhau)
        {
            ResultNguoi = new NGUOI(nguoi);
            ResultNhanKhau = new NHANKHAU(nhanKhau);

            if(nguoi != null)
            {
                CMND = nguoi.CMND;
                HoTen = nguoi.Ten;
                NgaySinh = nguoi.NgaySinh;
                GioiTinh = nguoi.GioiTinh;
                NoiSinh = nguoi.NoiSinh;
                QueQuan = nguoi.QueQuan;
                DanToc = nguoi.DanToc;
                TonGiao = nguoi.TonGiao;
                NgheNghiep = nguoi.NgheNghiep;
            }

            if(nhanKhau != null)
            {
                SelectedSoHoKhau = nhanKhau.SOHOKHAU;
                QuanHeVoiChuHo = nhanKhau.QuanHeVoiChuHo;
                IsChuHo = QuanHeVoiChuHo == "Chủ hộ";
                ChoOHienNay = nhanKhau.ChoOHienNay;
            }
        }
        #endregion

        #region fields
        private NHANKHAU _resultNhanKhau = null;
        public NHANKHAU ResultNhanKhau
        {
            get
            {
                if (_resultNhanKhau == null)
                {
                    _resultNhanKhau = new NHANKHAU(null);
                    OnPropertyChanged();
                }
                return _resultNhanKhau;
            }
            set { _resultNhanKhau = value; OnPropertyChanged(); }
        }

        private NGUOI _resultNguoi = null;
        public NGUOI ResultNguoi
        {
            get
            {
                if (_resultNguoi == null)
                {
                    _resultNguoi = new NGUOI(null);
                    OnPropertyChanged();
                }
                return _resultNguoi;
            }
            set { _resultNguoi = value; OnPropertyChanged(); }
        }

        public string CMND
        {
            get => ResultNguoi.CMND;
            set { ResultNguoi.CMND = value; ResultNhanKhau.CMND = value; OnPropertyChanged(); }
        }

        public string HoTen
        {
            get => ResultNguoi.Ten;
            set { ResultNguoi.Ten = value; OnPropertyChanged(); }
        }

        public DateTime NgaySinh
        {
            get => ResultNguoi.NgaySinh;
            set { ResultNguoi.NgaySinh = value; OnPropertyChanged(); }
        }
        public string GioiTinh
        {
            get => ResultNguoi.GioiTinh;
            set { ResultNguoi.GioiTinh = value; OnPropertyChanged(); }
        }

        public string NoiSinh
        {
            get => ResultNguoi.NoiSinh;
            set { ResultNguoi.NoiSinh = value; OnPropertyChanged(); }
        }
        public string QueQuan
        {
            get => ResultNguoi.QueQuan;
            set { ResultNguoi.QueQuan = value; OnPropertyChanged(); }
        }

        public string DanToc
        {
            get => ResultNguoi.DanToc;
            set { ResultNguoi.DanToc = value; OnPropertyChanged(); }
        }

        public string TonGiao
        {
            get => ResultNguoi.TonGiao;
            set { ResultNguoi.TonGiao = value; OnPropertyChanged(); }
        }

        public string NgheNghiep
        {
            get => ResultNguoi.NgheNghiep;
            set { ResultNguoi.NgheNghiep = value; OnPropertyChanged(); }
        }

        private bool _isChuHo = false;
        public bool IsChuHo
        {
            get => _isChuHo;
            set 
            {
                if(value == _isChuHo)
                    return;

                if(value)
                    QuanHeVoiChuHo = "Chủ hộ";
                else
                    QuanHeVoiChuHo = "";

                _isChuHo = value;
                IsNotChuHo = !value;

                OnPropertyChanged(); 
            }
        }

        // cant set this property :)
        public bool IsNotChuHo
        {
            get => !_isChuHo;
            set { /* cant set this */ OnPropertyChanged(); }
        }

        private SOHOKHAU _selectedSoHoKhau = null;
        public SOHOKHAU SelectedSoHoKhau
        {
            get => _selectedSoHoKhau;
            set 
            { 
                _selectedSoHoKhau = value; 
                
                if(value != null)
                    ResultNhanKhau.MASHK = value.MaSHK; 
                else
                    ResultNhanKhau.MASHK = null;

                OnPropertyChanged(); 
            }
        }
        
        public string QuanHeVoiChuHo
        {
            get => ResultNhanKhau.QuanHeVoiChuHo;
            set { ResultNhanKhau.QuanHeVoiChuHo = value; OnPropertyChanged(); }
        }

        public string ChoOHienNay
        {
            get => ResultNhanKhau.ChoOHienNay;
            set { ResultNhanKhau.ChoOHienNay = value; OnPropertyChanged(); }
        }
        #endregion

        #region ListGioiTinh
        private BindingList<string> _listGioitinh = null;
        public BindingList<string> ListGioitinh
        {
            get
            {
                if(_listGioitinh == null)
                {
                    _listGioitinh = new BindingList<string>();
                    _listGioitinh.Add("Nam");
                    _listGioitinh.Add("Nữ");
                }

                return _listGioitinh;
            }
        }
        #endregion

        #region ListSOHOKHAU
        private BindingList<SOHOKHAU> LoadSoHoKhau()
        {
            BindingList<SOHOKHAU> result = new BindingList<SOHOKHAU>(DataProvider.Ins.DB.SOHOKHAUs.Where(shk => !shk.IsDeleted).ToList());
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

            if (ValidateResult(out error))
            {
                UpsertResult();
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

        #region Test
        private void TestPrintResult()
        {
            Debug.WriteLine(
                  $"[= Test nhap nguoi ============]\n"
                + $"    CMND: {ResultNguoi.CMND}\n"
                + $"    Ten: {ResultNguoi.Ten}\n"
                + $"    Ngay sinh: {ResultNguoi.NgaySinh}\n"
                + $"    Gioi tinh: {ResultNguoi.GioiTinh}\n"
                + $"    Nghe nghiep: {ResultNguoi.NgheNghiep}\n"
                + $"    Noi sinh: {ResultNguoi.NoiSinh}\n"
                + $"    Que quan: {ResultNguoi.QueQuan}\n"
                + $"    Dan toc: {ResultNguoi.DanToc}\n"
                + $"    Ton giao: {ResultNguoi.TonGiao}\n"
                + $"[==============================]\n"
            );

            Debug.WriteLine(
                  $"[= Test nhap nhan khau ========]\n"
                + $"    CMND: {ResultNhanKhau.CMND}\n"
                + $"    Ma SHK: {ResultNhanKhau.MASHK}\n"
                + $"    Quan he voi chu ho: {ResultNhanKhau.QuanHeVoiChuHo}\n"
                + $"    Cho o hien nay: {ResultNhanKhau.ChoOHienNay}\n"
                + $"[==============================]\n"
            );
        }
        #endregion

        #region Adjust and Validation
        private void AdjustResult()
        {
            // CuteTN Note: but.. why???
            // ResultNguoi.NHANKHAU = ResultNhanKhau;
            ResultNhanKhau.NGUOI = ResultNguoi;
        }

        private bool ValidateResult(out string errors)
        {
            if (string.IsNullOrEmpty(CMND))
            {
                errors = $"Vui lòng nhập số CMND";
                return false;
            }
            else
            {
                var cntFiltered = DataProvider.Ins.DB.NGUOIs.Where(nguoi => ((!nguoi.IsDeleted) && (CMND == nguoi.CMND))).Count();

                if (cntFiltered != 0 && isAddingMode)
                {
                    errors = $"Công dân với CMND {CMND} đã tồn tại trong hệ thống";
                    return false;
                }
            }

            if(string.IsNullOrEmpty(HoTen))
            {
                errors = $"Vui lòng nhập họ và tên của công dân";
                return false;
            }

            if (string.IsNullOrEmpty(GioiTinh))
            {
                errors = $"Vui lòng chọn giới tính của công dân";
                return false;
            }

            if(DateTime.Today.Year - NgaySinh.Year < 15)
            {
                errors = $"Công dân phải từ đủ 15 tuổi trở lên để được cấp giấy CMND";
                return false;
            }

            if(SelectedSoHoKhau == null)
            {
                errors = $"Vui lòng chọn mã sổ hộ khẩu";
                return false;
            }

            if(IsChuHo)
            {
                // if there's already CHUHO and they have not been deleted
                if(! (string.IsNullOrEmpty(SelectedSoHoKhau.CMNDChuHo) || SelectedSoHoKhau.NHANKHAU.IsDeleted))
                    if(SelectedSoHoKhau.CMNDChuHo != CMND)
                    {
                        errors = $"Sổ hộ khẩu {SelectedSoHoKhau.MaSHK} đã tồn tại chủ hộ. với CMND {SelectedSoHoKhau.CMNDChuHo}";
                        return false;
                    }
            }

            if(string.IsNullOrEmpty(QuanHeVoiChuHo))
            {
                errors = $"Vui lòng nhập quan hệ với chủ hộ";
                return false;
            }

            if(string.IsNullOrEmpty(ChoOHienNay))
            {
                errors = $"Vui lòng nhập chỗ ở hiện nay";
                return false;
            }

            errors = "";
            return true;
        }
        #endregion


        #region database
        private void UpsertResult()
        {
            AdjustResult();

            NGUOI targetNguoi = DataProvider.Ins.DB.NGUOIs.Find(CMND);
            NHANKHAU targetNK = DataProvider.Ins.DB.NHANKHAUs.Find(CMND);

            if (targetNguoi == null)
            {
                DataProvider.Ins.DB.NGUOIs.Add(ResultNguoi);
            }
            else
            {
                targetNguoi.CopyInfo(ResultNguoi);
                targetNguoi.IsDeleted = false;
            }

            if (targetNK == null)
            {
                DataProvider.Ins.DB.NHANKHAUs.Add(ResultNhanKhau);
            }
            else
            {
                targetNK.CopyInfo(ResultNhanKhau);
                targetNK.IsDeleted = false;
            }

            // Update ChuHo of SHK:
            UpdateChuHoOfShk();
            Utils.AdjustChuHoInSoHoKhaus();

            DataProvider.Ins.DB.SaveChanges();
            OnDatabaseUpdated?.Invoke(this, null);
        }

        private void UpdateChuHoOfShk()
        {
            if(IsChuHo)
                SelectedSoHoKhau.CMNDChuHo = CMND;
            else
            {
                if(SelectedSoHoKhau.CMNDChuHo == CMND)
                    SelectedSoHoKhau.CMNDChuHo = null;
            }
        }
        #endregion

        #region Utils
        private void Reset()
        {
            ResultNguoi = null;
            ResultNhanKhau = null;  
            CMND = "";
            HoTen = "";
            GioiTinh = "";
            NoiSinh = "";
            QueQuan = "";
            DanToc = "";
            TonGiao = "";
            NgheNghiep = "";

            SelectedSoHoKhau = null;
            QuanHeVoiChuHo = "";
            ChoOHienNay = "";

            Refresh();
        }

        public void Refresh()
        {
            ListSOHOKHAU = LoadSoHoKhau();
        }

        private bool CheckCMNDExist(string cmnd)
        {
            return (DataProvider.Ins.DB.NGUOIs.Find(cmnd) != null);
        }
        #endregion

        #region Events
        public EventHandler OnDatabaseUpdated = null;
        #endregion
    }
}
