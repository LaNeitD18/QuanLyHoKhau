﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Input;
using QuanLyHoKhau.Model;
using QuanLyHoKhau.Utilities;
using QuanLyHoKhau.View;

namespace QuanLyHoKhau.ViewModel
{
    class NhapHoKhau_VM : BaseViewModel
    {
        // CuteTN Note: the goal is to make this class immutable from the outside :)

        const int SOHOKHAU_ID_LENGTH = 5;
        const string SOHOKHAU_ID_PREFIX = "SHK";

        #region Init
        bool isAddingMode = true;

        public NhapHoKhau_VM() : this(null)
        {

        }

        public NhapHoKhau_VM(SOHOKHAU soHoKhau)
        {
            isAddingMode = soHoKhau == null;

            ResultSoHoKhau = new SOHOKHAU(soHoKhau);
            LoadInfo(soHoKhau);
        }

        public void LoadInfo(SOHOKHAU soHoKhau)
        {
            Reset();

            if(soHoKhau != null)
            {
                MaSoHoKhau = soHoKhau.MaSHK;
                SelectedSoLuuNhanKhau = soHoKhau.SOLUUNHANKHAU;
                DiaChi = soHoKhau.DiaChi;
            }
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
            set 
            { 
                ResultSoHoKhau.MaSHK = value; 
                ListNHANKHAUinSHK = LoadNhanKhauInSHK();
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

        #region ListNHANKHAUinSHK
        private BindingList<NHANKHAU> LoadNhanKhauInSHK()
        {
            SOHOKHAU shk = DataProvider.Ins.DB.SOHOKHAUs.Find(MaSoHoKhau);

            if(shk == null || shk.IsDeleted)
                return new BindingList<NHANKHAU>();

            //BindingList<NHANKHAU> result = new BindingList<NHANKHAU>
            //    (
            //        DataProvider.Ins.DB.NHANKHAUs.Where(nk =>
            //            (!nk.IsDeleted) && (nk.MASHK == MaSoHoKhau)
            //        ).ToList()
            //    );
            BindingList<NHANKHAU> result = new BindingList<NHANKHAU>
                (
                    shk.NHANKHAUs.Where(nk => !nk.IsDeleted).ToList()
                );

            return result;
        }

        private BindingList<NHANKHAU> _listNHANKHAUinSHK = null;
        public BindingList<NHANKHAU> ListNHANKHAUinSHK
        {
            get
            {
                if (_listNHANKHAUinSHK == null)
                    _listNHANKHAUinSHK = LoadNhanKhauInSHK();
                return _listNHANKHAUinSHK;
            }
            set
            {
                _listNHANKHAUinSHK = value;
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
            if(!string.IsNullOrEmpty(MaSoHoKhau))
            {
                var cntFiltered = DataProvider.Ins.DB.SOHOKHAUs.Where(shk => ((!shk.IsDeleted) && (MaSoHoKhau == shk.MaSHK))).Count();

                if(cntFiltered != 0 && isAddingMode)
                { 
                    errors = $"Mã sổ hộ khẩu {MaSoHoKhau} đã tồn tại trong hệ thống";
                    return false;
                }
            }

            if(!string.IsNullOrEmpty(MaCongAn))
                if(DataProvider.Ins.DB.CONGANs.Find(MaCongAn) == null)
                {
                    errors = $"Mã công an {MaCongAn} không tồn tại";
                    return false;
                }

            if(!string.IsNullOrEmpty(SoCmndChuHo))
            {
                var filteredNguoi = DataProvider.Ins.DB.NGUOIs.Where(nguoi => (nguoi.CMND == SoCmndChuHo) && (!nguoi.IsDeleted));

                // check if there is a person has this cmnd
                if (filteredNguoi.Count() == 0)
                {
                    errors = $"Công dân với số CMND {SoCmndChuHo} không tồn tại";
                    return false;
                }

                // check if this person is actually a NHANKHAU
                var nguoiDo = filteredNguoi.First();
                var filteredNhanKhau = DataProvider.Ins.DB.NHANKHAUs.Where(nhankhau => (nhankhau.CMND == nguoiDo.CMND) && (!nhankhau.IsDeleted));

                if(filteredNhanKhau.Count() == 0)
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
        private void UpsertResult()
        {
            AdjustResult();

            SOHOKHAU targetSHK = DataProvider.Ins.DB.SOHOKHAUs.Find(MaSoHoKhau);

            if(targetSHK == null)
            { 
                MaSoHoKhau = Utils.GenerateNewId(DataProvider.Ins.DB.SOHOKHAUs, SOHOKHAU_ID_PREFIX, SOHOKHAU_ID_LENGTH);
                DataProvider.Ins.DB.SOHOKHAUs.Add(ResultSoHoKhau);
            }
            else
            {
                targetSHK.CopyInfo(ResultSoHoKhau);
            }

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
            DiaChi = "";
            Refresh();
        }

        public void Refresh()
        {
            ListSOLUUNHANKHAU = LoadSLNK();
            ListNHANKHAUinSHK = LoadNhanKhauInSHK();
        }

        void HandleOnDbUpdated(Object sender, EventArgs args)
        {
            Refresh();

            // Update CMNDChuHo after editting NHANKHAU
            SOHOKHAU shk = DataProvider.Ins.DB.SOHOKHAUs.Find(MaSoHoKhau);
            if(shk != null)
                SoCmndChuHo = shk.CMNDChuHo;

            OnDatabaseUpdated?.Invoke(this, null);
        }
        #endregion

        #region Events
        public EventHandler OnDatabaseUpdated = null;
        #endregion

        #region Edit NHANKHAU
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
    }
}
