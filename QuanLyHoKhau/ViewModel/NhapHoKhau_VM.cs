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
using QuanLyHoKhau.View;

namespace QuanLyHoKhau.ViewModel
{
    class NhapHoKhau_VM : BaseViewModel
    {
        // CuteTN Note: the goal is to make this class immutable from the outside :)

        const int SOHOKHAU_ID_LENGTH = 9;
        const string SOHOKHAU_ID_PREFIX = "SHK_";

        #region Init
        bool isAddingMode = true;

        public NhapHoKhau_VM() : this(null)
        {

        }

        public NhapHoKhau_VM(SOHOKHAU soHoKhau)
        {
            isAddingMode = soHoKhau == null;

            Reset();
            LoadInfo(soHoKhau);
        }

        public void LoadInfo(SOHOKHAU soHoKhau)
        {
            ResultSoHoKhau = new SOHOKHAU(soHoKhau);

            if(soHoKhau != null)
            {
                MaSoHoKhau = soHoKhau.MaSHK;
                MaCongAn = soHoKhau.MaCongAn;
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
                Debug.WriteLine(ResultSoHoKhau.MaSHK);
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

            BindingList<NHANKHAU> result = new BindingList<NHANKHAU>
                (
                    DataProvider.Ins.DB.NHANKHAUs.Where(nk =>
                        (nk.BanChinhThuc) && (!nk.IsDeleted) && (nk.MASHK == MaSoHoKhau)
                    ).ToList()
                );
            //BindingList<NHANKHAU> result = new BindingList<NHANKHAU>
            //    (
            //        shk.NHANKHAUs.Where(nk => nk.BanChinhThuc && !nk.IsDeleted).ToList()
            //    );

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
                System.Windows.MessageBoxResult dlgRes;

                if (isAddingMode)
                    dlgRes = System.Windows.MessageBox.Show($"Vui lòng xác nhận việc thêm hộ khẩu mới", "Xác nhận", System.Windows.MessageBoxButton.YesNo);
                else
                    dlgRes = System.Windows.MessageBox.Show($"Vui lòng xác nhận việc sửa hộ khẩu {MaSoHoKhau}", "Xác nhận", System.Windows.MessageBoxButton.YesNo);

                if (dlgRes == System.Windows.MessageBoxResult.No)
                    return;

                UpsertResult();

                if (isAddingMode)
                    System.Windows.MessageBox.Show($"Đã thêm hộ khẩu {MaSoHoKhau} thành công.\nVui lòng chờ duyệt thay đổi để cập nhật.", "Thông báo");
                else
                    System.Windows.MessageBox.Show($"Đã sửa hộ khẩu {MaSoHoKhau} thành công.\nVui lòng chờ duyệt thay đổi để cập nhật.", "Thông báo");

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
                var filteredNhanKhau = DataProvider.Ins.DB.NHANKHAUs.Where(nhankhau => (nhankhau.CMND == nguoiDo.CMND) && (nhankhau.BanChinhThuc && !nhankhau.IsDeleted));

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

            if(targetSHK == null || targetSHK.IsDeleted)
            {
                AddPendingAddSoHoKhau();
            }
            else
            { 
                AddPendingEditSoHoKhau();
            }

            DataProvider.Ins.DB.SaveChanges();
            OnDatabaseUpdated?.Invoke(this, null);
        }

        private void AddPendingEditSoHoKhau()
        {
            SOHOKHAU targetSHK = DataProvider.Ins.DB.SOHOKHAUs.Find(MaSoHoKhau);
            SOHOKHAU tempSHK = new SOHOKHAU(ResultSoHoKhau)
            {
                MaSHK = Utils.GenerateNewId(DataProvider.Ins.DB.SOHOKHAUs, $"{MaSoHoKhau}_", 3),
                BanChinhThuc = false,
                IsDeleted = false,
            };

            // if the info has not been changed, then it's no need to create Pending entry
            if (
                targetSHK.MaSoLuuNhanKhau == tempSHK.MaSoLuuNhanKhau &&
                targetSHK.DiaChi == tempSHK.DiaChi
            )
            {
                return;
            }

            DataProvider.Ins.DB.SOHOKHAUs.Add(tempSHK);

            PHIEUDUYETSOHOKHAU pdshk = new PHIEUDUYETSOHOKHAU()
            {
                MaPD_SHK = Utils.GenerateNewId(DataProvider.Ins.DB.PHIEUDUYETSOHOKHAUs, "PDSHK_", 8),
                NgayTao = DateTime.Now,
                IsDeleted = false,

                MaSHK = targetSHK.MaSHK,
                MaSHK_PendingInfo = tempSHK.MaSHK,

                ActionType = "Edit",
                DaDuyet = false,
            };

            DataProvider.Ins.DB.PHIEUDUYETSOHOKHAUs.Add(pdshk);
        }

        private void AddPendingAddSoHoKhau()
        {
            SOHOKHAU targetSHK = DataProvider.Ins.DB.SOHOKHAUs.Find(MaSoHoKhau);
            ResultSoHoKhau.BanChinhThuc = false;

            if (targetSHK == null)
            {
                MaSoHoKhau = Utils.GenerateNewId(DataProvider.Ins.DB.SOHOKHAUs, SOHOKHAU_ID_PREFIX, SOHOKHAU_ID_LENGTH);
                DataProvider.Ins.DB.SOHOKHAUs.Add(ResultSoHoKhau);
            }
            else if (targetSHK.IsDeleted)
            {
                targetSHK.CopyInfo(ResultSoHoKhau);
                targetSHK.IsDeleted = false;
            }

            PHIEUDUYETSOHOKHAU pdshk = new PHIEUDUYETSOHOKHAU()
            {
                MaPD_SHK = Utils.GenerateNewId(DataProvider.Ins.DB.PHIEUDUYETSOHOKHAUs, "PDSHK_", 8),
                NgayTao = DateTime.Now,
                IsDeleted = false,

                MaSHK = MaSoHoKhau,
                MaSHK_PendingInfo = MaSoHoKhau,

                ActionType = "Add",
                DaDuyet = false,
            };

            DataProvider.Ins.DB.PHIEUDUYETSOHOKHAUs.Add(pdshk);
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
            UpdateCmndOnNhanKhauUpdate();
            OnDatabaseUpdated?.Invoke(this, null);
        }

        void UpdateCmndOnNhanKhauUpdate()
        {
            // Update CMNDChuHo after editting NHANKHAU
            SOHOKHAU shkInDb = DataProvider.Ins.DB.SOHOKHAUs.Find(MaSoHoKhau);
            if (shkInDb != null)
            {
                SoCmndChuHo = shkInDb.CMNDChuHo;
            }
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
