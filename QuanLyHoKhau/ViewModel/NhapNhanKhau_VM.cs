﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        private bool _isCmndReadOnly = true;
        public bool IsCmndReadOnly
        {
            get => _isCmndReadOnly;
            set { _isCmndReadOnly = value; OnPropertyChanged(); }
        }

        private bool _isMaShkEnable = true;
        public bool IsMaShkEnable
        {
            get => _isMaShkEnable;
            set { _isMaShkEnable = value; OnPropertyChanged(); }
        }

        public NhapNhanKhau_VM() : this(null, null)
        {
        }

        public NhapNhanKhau_VM(NGUOI nguoi, NHANKHAU nhanKhau)
        {
            isAddingMode = (nguoi == null) || (nhanKhau == null);
            IsCmndReadOnly = !isAddingMode;
            IsMaShkEnable = isAddingMode;

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
                System.Windows.MessageBoxResult dlgRes;

                if (isAddingMode)
                    dlgRes = System.Windows.MessageBox.Show($"Vui lòng xác nhận việc thêm nhân khẩu {CMND}", "Xác nhận", System.Windows.MessageBoxButton.YesNo);
                else
                    dlgRes = System.Windows.MessageBox.Show($"Vui lòng xác nhận việc sửa nhân khẩu {CMND}", "Xác nhận", System.Windows.MessageBoxButton.YesNo);

                if (dlgRes == System.Windows.MessageBoxResult.No)
                    return;

                UpsertResult();
                
                if(isAddingMode)
                    System.Windows.MessageBox.Show($"Đã thêm nhân khẩu {CMND} thành công.\nVui lòng chờ duyệt thay đổi để cập nhật.", "Thông báo");
                else
                    System.Windows.MessageBox.Show($"Đã sửa nhân khẩu {CMND} thành công.\nVui lòng chờ duyệt thay đổi để cập nhật.", "Thông báo");

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
            System.Windows.MessageBoxResult msgRes = System.Windows.MessageBox.Show($"Bạn có chắc muốn huỷ thông tin đang nhập không?", "Xác nhận", System.Windows.MessageBoxButton.YesNo);

            if (msgRes == System.Windows.MessageBoxResult.Yes)
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

                if(CMND.Any(c => c < '0' || c > '9'))
                {
                    errors = $"Số CMND chỉ được phép chứa chữ số";
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

            if(targetNK != null && !targetNK.IsDeleted)
            {
                AddPendingEditNhanKhau();
            }
            else
            {
                AddPendingAddNhanKhau();
            }

            // Update ChuHo of SHK:
            // UpdateChuHoOfShk();
            Utils.RemoveInvalidChuHoInSHKs();

            DataProvider.Ins.DB.SaveChanges();
            OnDatabaseUpdated?.Invoke(this, null);
        }



        private void AddPendingEditNhanKhau()
        {
            NHANKHAU targetNK = DataProvider.Ins.DB.NHANKHAUs.Find(CMND);
            NHANKHAU tempNK = new NHANKHAU(ResultNhanKhau) 
            { 
                CMND = Utils.GenerateNewId(DataProvider.Ins.DB.NHANKHAUs, $"{CMND}_", 3),
                BanChinhThuc = false,
                IsDeleted = false,
            };

            // if the info has not been changed, then it's no need to create Pending entry
            if(
                targetNK.QuanHeVoiChuHo == tempNK.QuanHeVoiChuHo &&
                targetNK.ChoOHienNay == tempNK.ChoOHienNay
            )
            {
                return;
            }

            // CuteTN Note: this is hot fix :)
            // because if we don't add a temp NGUOI, the db will raise Foreign key constrain error
            NGUOI tempN = new NGUOI(ResultNguoi)
            {
                CMND = tempNK.CMND,
                IsDeleted = true,
            };
            DataProvider.Ins.DB.NGUOIs.Add(tempN);

            DataProvider.Ins.DB.NHANKHAUs.Add(tempNK);

            PHIEUDUYETNHANKHAU pdnk = new PHIEUDUYETNHANKHAU()
            {
                MaPD_NK = Utils.GenerateNewId(DataProvider.Ins.DB.PHIEUDUYETNHANKHAUs, "PDNK_", 8),
                NgayTao = DateTime.Now,
                IsDeleted = false,

                MaNK = targetNK.CMND,
                MaNK_PendingInfo = tempNK.CMND,
                
                ActionType = "Edit",
                DaDuyet = false,
            };

            DataProvider.Ins.DB.PHIEUDUYETNHANKHAUs.Add(pdnk);
        }

        private void AddPendingAddNhanKhau()
        {
            NHANKHAU targetNK = DataProvider.Ins.DB.NHANKHAUs.Find(CMND);
            ResultNhanKhau.BanChinhThuc = false;

            if(targetNK == null)
            {
                DataProvider.Ins.DB.NHANKHAUs.Add(ResultNhanKhau);
            }
            else if(targetNK.IsDeleted)
            {
                targetNK.CopyInfo(ResultNhanKhau);
                targetNK.IsDeleted = false;
            }

            PHIEUDUYETNHANKHAU pdnk = new PHIEUDUYETNHANKHAU()
            {
                MaPD_NK = Utils.GenerateNewId(DataProvider.Ins.DB.PHIEUDUYETNHANKHAUs, "PDNK_", 8),
                NgayTao = DateTime.Now,
                IsDeleted = false,

                MaNK = CMND,
                MaNK_PendingInfo = CMND,

                ActionType = "Add",
                DaDuyet = false,
            };

            DataProvider.Ins.DB.PHIEUDUYETNHANKHAUs.Add(pdnk);
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
