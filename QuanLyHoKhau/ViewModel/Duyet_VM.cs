using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyHoKhau.Model;

namespace QuanLyHoKhau.ViewModel
{
    public class Duyet_VM: BaseViewModel
    {
        #region class display support

        public class NhanKhauChoDuyetDisplay
        {
            private PHIEUDUYETNHANKHAU _phieuDuyet;
            public PHIEUDUYETNHANKHAU PhieuDuyet
            {
                get { return _phieuDuyet; }
                set { _phieuDuyet = value; }
            }

            private NHANKHAU _nhanKhau;
            public NHANKHAU NhanKhau
            {
                get{ return _nhanKhau; }
                set { _nhanKhau = value; }
            }

            private NHANKHAU _nhanKhauPending;
            public NHANKHAU NhanKhauPending
            {
                get { return _nhanKhauPending; }
                set { _nhanKhauPending = value; }
            }

            private NGUOI _nguoiInfo;
            public NGUOI NguoiInfo
            {
                get { return _nguoiInfo; }
                set { _nguoiInfo = value; }
            }
        }

        public class SoHoKhauChoDuyetDisplay
        {
            private PHIEUDUYETSOHOKHAU _phieuDuyet;
            public PHIEUDUYETSOHOKHAU PhieuDuyet
            {
                get { return _phieuDuyet; }
                set { _phieuDuyet = value; }
            }

            private SOHOKHAU _soHoKhau;
            public SOHOKHAU SoHoKhau
            {
                get { return _soHoKhau; }
                set { _soHoKhau = value; }
            }

            private SOHOKHAU _soHoKhauPending;
            public SOHOKHAU SoHoKhauPending
            {
                get { return _soHoKhauPending; }
                set { _soHoKhauPending = value; }
            }
        }

        public class ChuyenKhauChoDuyetDisplay
        {
            private PHIEUDUYETCHUYENKHAU _phieuDuyet;
            public PHIEUDUYETCHUYENKHAU PhieuDuyet
            {
                get { return _phieuDuyet; }
                set { _phieuDuyet = value; }
            }
        }
        #endregion

        #region ComboBox LoaiGiayTo 
        // first parameter is id
        // second parameter is displayed string
        private List<Tuple<int, string>> _listLoaiGiayTo;
        public List<Tuple<int, string>> ListLoaiGiayTo
        {
            get
            {
                if(_listLoaiGiayTo == null)
                {
                    _listLoaiGiayTo = new List<Tuple<int, string>>();
                    _listLoaiGiayTo.Add(new Tuple<int, string>(0, "Nhân khẩu"));
                    _listLoaiGiayTo.Add(new Tuple<int, string>(1, "Hộ khẩu"));
                    _listLoaiGiayTo.Add(new Tuple<int, string>(2, "Chuyển khẩu"));
                }
                return _listLoaiGiayTo;
            }
            set { _listLoaiGiayTo = value; }
        }

        private Tuple<int, string> _selectedLoaiGiayTo;
        public Tuple<int, string> SelectedLoaiGiayTo
        {
            get
            {
                if(_selectedLoaiGiayTo == null)
                {
                    _selectedLoaiGiayTo = ListLoaiGiayTo[0];
                }
                return _selectedLoaiGiayTo;
            }
            set { _selectedLoaiGiayTo = value; }
        }
        #endregion

        #region NhanKhau

        private ObservableCollection<NhanKhauChoDuyetDisplay> _listNhanKhauChoDuyet;
        public ObservableCollection<NhanKhauChoDuyetDisplay> ListNhanKhauChoDuyet
        {
            get
            {
                if(_listNhanKhauChoDuyet == null)
                {
                    _listNhanKhauChoDuyet = new ObservableCollection<NhanKhauChoDuyetDisplay>();

                    var listPhieuDuyetNhanKhau = (from p in DataProvider.Ins.DB.PHIEUDUYETNHANKHAUs
                                                  where p.DaDuyet == false
                                                  select p).ToList();

                    foreach(var phieu in listPhieuDuyetNhanKhau)
                    {
                        NhanKhauChoDuyetDisplay nhanKhau = new NhanKhauChoDuyetDisplay();
                        nhanKhau.NhanKhau = DataProvider.Ins.DB.NHANKHAUs.Find(phieu.MaNK);
                        nhanKhau.NhanKhauPending = DataProvider.Ins.DB.NHANKHAUs.Find(phieu.MaNK_PendingInfo);
                        nhanKhau.PhieuDuyet = phieu;
                        nhanKhau.NguoiInfo = DataProvider.Ins.DB.NGUOIs.Find(nhanKhau.NhanKhau.CMND);

                        _listNhanKhauChoDuyet.Add(nhanKhau);
                    }
                }

                return _listNhanKhauChoDuyet;
            }
            set
            {
                _listNhanKhauChoDuyet = value;
                OnPropertyChanged("ListNhanKhauChoDuyet");
            }
        }

        #region Duyet Nhan Khau
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Null if success, otherwise, return error message</returns>
        public string DuyetNhanKhau(object input)
        {
            NhanKhauChoDuyetDisplay nhanKhauChoDuyet = input as NhanKhauChoDuyetDisplay;

            if (nhanKhauChoDuyet == null) return "DataType for input not valid"; // check data type

            string errorMsg = null;
            switch(nhanKhauChoDuyet.PhieuDuyet.ActionType)
            {
                case DuyetActionTypes.Add:
                    errorMsg = AddNhanKhau(nhanKhauChoDuyet);
                    break;
                case DuyetActionTypes.Edit:
                    errorMsg = EditNhanKhau(nhanKhauChoDuyet);
                    break;
                case DuyetActionTypes.Remove:
                    errorMsg = RemoveNhanKhau(nhanKhauChoDuyet);
                    break;
            }

            return errorMsg;
        }

        public string TuChoiDuyetNhanKhau(object input)
        {
            NhanKhauChoDuyetDisplay nhanKhauChoDuyet = input as NhanKhauChoDuyetDisplay;

            if (nhanKhauChoDuyet == null) return "DataType for input not valid"; // check data type

            string errorMsg = null;

            nhanKhauChoDuyet.PhieuDuyet.DaDuyet = true;

            try
            {
                DataProvider.Ins.DB.SaveChanges();
            }
            catch (Exception e)
            {
                errorMsg = e.Message;
                return errorMsg;
            }

            ListNhanKhauChoDuyet.Remove(nhanKhauChoDuyet);
            return errorMsg;
        }

        private string AddNhanKhau(NhanKhauChoDuyetDisplay nhanKhau)
        {
            DataProvider.Ins.DB.NHANKHAUs.Add(nhanKhau.NhanKhauPending);

            try
            {
                DataProvider.Ins.DB.SaveChanges();
            }
            catch (Exception e)
            {
                var msg = e.Message;
                return msg;
            }

            ListNhanKhauChoDuyet.Remove(nhanKhau);
            return null;
        }

        private string EditNhanKhau(NhanKhauChoDuyetDisplay nhanKhau)
        {
            var oldCmnd = nhanKhau.NhanKhau.CMND;

            nhanKhau.NhanKhau.CopyInfo(nhanKhau.NhanKhauPending);

            nhanKhau.NhanKhau.CMND = oldCmnd; // we copy info but the primary key we dont change
            nhanKhau.NhanKhau.BanChinhThuc = true;

            nhanKhau.PhieuDuyet.DaDuyet = true;

            try
            {
                DataProvider.Ins.DB.SaveChanges();
            }
            catch(Exception e)
            {
                var msg = e.Message;
                return msg;
            }

            ListNhanKhauChoDuyet.Remove(nhanKhau);
            return null;
        }

        private string RemoveNhanKhau(NhanKhauChoDuyetDisplay nhanKhau)
        {
            nhanKhau.NhanKhauPending.IsDeleted = true;

            try
            {
                DataProvider.Ins.DB.SaveChanges();
            }
            catch (Exception e)
            {
                var msg = e.Message;
                return msg;
            }

            ListNhanKhauChoDuyet.Remove(nhanKhau);
            return null;
        }

        #endregion
        #endregion

        #region SoHoKhau
        private ObservableCollection<SoHoKhauChoDuyetDisplay> _listSoHoKhauChoDuyet;
        public ObservableCollection<SoHoKhauChoDuyetDisplay> ListSoHoKhauChoDuyet
        {
            get
            {
                if (_listSoHoKhauChoDuyet == null)
                {
                    _listSoHoKhauChoDuyet = new ObservableCollection<SoHoKhauChoDuyetDisplay>();

                    var listPhieuDuyetSoHoKhau = (from p in DataProvider.Ins.DB.PHIEUDUYETSOHOKHAUs
                                                  where p.DaDuyet == false
                                                  select p).ToList();

                    foreach (var phieu in listPhieuDuyetSoHoKhau)
                    {
                        SoHoKhauChoDuyetDisplay soHoKhau = new SoHoKhauChoDuyetDisplay();
                        soHoKhau.PhieuDuyet = phieu;
                        soHoKhau.SoHoKhau = DataProvider.Ins.DB.SOHOKHAUs.Find(phieu.MaSHK);
                        soHoKhau.SoHoKhauPending = DataProvider.Ins.DB.SOHOKHAUs.Find(phieu.MaSHK_PendingInfo);

                        _listSoHoKhauChoDuyet.Add(soHoKhau);
                    }
                }

                return _listSoHoKhauChoDuyet;
            }
            set
            {
                _listSoHoKhauChoDuyet = value;
                OnPropertyChanged("ListSoHoKhauChoDuyet");
            }
        }

        #region Duyet SHK
        public string DuyetSoHoKhau(object input)
        {
            SoHoKhauChoDuyetDisplay soHoKhauChoDuyet = input as SoHoKhauChoDuyetDisplay;

            if (soHoKhauChoDuyet == null) return "DataType for input not valid"; // check data type

            string errorMsg = null;
            switch (soHoKhauChoDuyet.PhieuDuyet.ActionType)
            {
                case DuyetActionTypes.Add:
                    errorMsg = AddSoHoKhau(soHoKhauChoDuyet);
                    break;
                case DuyetActionTypes.Edit:
                    errorMsg = EditSoHoKhau(soHoKhauChoDuyet);
                    break;
                case DuyetActionTypes.Remove:
                    errorMsg = RemoveSoHoKhau(soHoKhauChoDuyet);
                    break;
            }

            return errorMsg;
        }

        public string TuChoiDuyetSoHoKhau(object input)
        {
            SoHoKhauChoDuyetDisplay soHoKhauChoDuyet = input as SoHoKhauChoDuyetDisplay;

            if (soHoKhauChoDuyet == null) return "DataType for input not valid"; // check data type

            string errorMsg = null;

            soHoKhauChoDuyet.PhieuDuyet.DaDuyet = true;

            try
            {
                DataProvider.Ins.DB.SaveChanges();
            }
            catch (Exception e)
            {
                errorMsg = e.Message;
                return errorMsg;
            }

            ListSoHoKhauChoDuyet.Remove(soHoKhauChoDuyet);
            return errorMsg;
        }

        private string AddSoHoKhau(SoHoKhauChoDuyetDisplay soHoKhau)
        {
            DataProvider.Ins.DB.SOHOKHAUs.Add(soHoKhau.SoHoKhau);

            try
            {
                DataProvider.Ins.DB.SaveChanges();
            }
            catch (Exception e)
            {
                var msg = e.Message;
                return msg;
            }

            ListSoHoKhauChoDuyet.Remove(soHoKhau);
            return null;
        }

        private string EditSoHoKhau(SoHoKhauChoDuyetDisplay soHoKhau)
        {
            var oldMaSHK = soHoKhau.SoHoKhau.MaSHK;

            soHoKhau.SoHoKhau.CopyInfo(soHoKhau.SoHoKhauPending);

            soHoKhau.SoHoKhau.MaSHK = oldMaSHK; // we copy info but the primary key we dont change
            soHoKhau.SoHoKhau.BanChinhThuc = true;

            soHoKhau.PhieuDuyet.DaDuyet = true;

            try
            {
                DataProvider.Ins.DB.SaveChanges();
            }
            catch (Exception e)
            {
                var msg = e.Message;
                return msg;
            }

            ListSoHoKhauChoDuyet.Remove(soHoKhau);
            return null;
        }

        private string RemoveSoHoKhau(SoHoKhauChoDuyetDisplay soHoKhau)
        {
            soHoKhau.SoHoKhau.IsDeleted = true;

            try
            {
                DataProvider.Ins.DB.SaveChanges();
            }
            catch (Exception e)
            {
                var msg = e.Message;
                return msg;
            }

            ListSoHoKhauChoDuyet.Remove(soHoKhau);
            return null;
        }

        #endregion
        #endregion

        #region ChuyenKhau
        private ObservableCollection<ChuyenKhauChoDuyetDisplay> _listChuyenKhauChoDuyet;
        public ObservableCollection<ChuyenKhauChoDuyetDisplay> ListChuyenKhauChoDuyet
        {
            get
            {
                if (_listChuyenKhauChoDuyet == null)
                {
                    _listChuyenKhauChoDuyet = new ObservableCollection<ChuyenKhauChoDuyetDisplay>();

                    var listPhieuDuyetChuyenKhau = (from p in DataProvider.Ins.DB.PHIEUDUYETCHUYENKHAUs
                                                  where p.DaDuyet == false
                                                  select p).ToList();

                    foreach (var phieu in listPhieuDuyetChuyenKhau)
                    {
                        ChuyenKhauChoDuyetDisplay chuyenKhau = new ChuyenKhauChoDuyetDisplay();
                        chuyenKhau.PhieuDuyet = phieu;

                        _listChuyenKhauChoDuyet.Add(chuyenKhau);
                    }
                }

                return _listChuyenKhauChoDuyet;
            }
            set
            {
                _listChuyenKhauChoDuyet = value;
                OnPropertyChanged("ListChuyenKhauChoDuyet");
            }
        }

        #region Duyet Chuyen Khau
        public string DuyetChuyenKhau(object input)
        {
            ChuyenKhauChoDuyetDisplay chuyenKhauChoDuyet = input as ChuyenKhauChoDuyetDisplay;

            if (chuyenKhauChoDuyet == null) return "DataType for input not valid"; // check data type

            string errorMsg = null;

            var nhanKhau = DataProvider.Ins.DB.NHANKHAUs.Find(chuyenKhauChoDuyet.PhieuDuyet.CMND);

            nhanKhau.MASHK = chuyenKhauChoDuyet.PhieuDuyet.MaSHKChuyenDen;
            chuyenKhauChoDuyet.PhieuDuyet.DaDuyet = true;
            try
            {
                DataProvider.Ins.DB.SaveChanges();
            }
            catch (Exception e)
            {
                errorMsg = e.Message;
                return errorMsg;
            }

            ListChuyenKhauChoDuyet.Remove(chuyenKhauChoDuyet);
            return null;
        }

        public string TuChoiDuyetChuyenKhau(object input)
        {
            ChuyenKhauChoDuyetDisplay chuyenKhauChoDuyet = input as ChuyenKhauChoDuyetDisplay;

            if (chuyenKhauChoDuyet == null) return "DataType for input not valid"; // check data type

            string errorMsg = null;

            chuyenKhauChoDuyet.PhieuDuyet.DaDuyet = true;

            try
            {
                DataProvider.Ins.DB.SaveChanges();
            }
            catch (Exception e)
            {
                errorMsg = e.Message;
                return errorMsg;
            }

            ListChuyenKhauChoDuyet.Remove(chuyenKhauChoDuyet);
            return errorMsg;
        }
        #endregion
        #endregion

        #region Duyet all
        
        public string DuyetAll()
        {
            string errorString = null;
            switch(SelectedLoaiGiayTo.Item1)
            {
                case 0:
                    errorString = DuyetAllNhanKhau();
                    break;
                case 1:
                    errorString = DuyetAllSoHoKhau();
                    break;
                case 2:
                    errorString = DuyetAllChuyenKhau();
                    break;
            }

            return errorString;
        }

        private string DuyetAllNhanKhau()
        {
            string errorString = null;
            var ListDuyet = ListNhanKhauChoDuyet.ToList();
            foreach (var nhanKhau in ListDuyet)
            {
                errorString = DuyetNhanKhau(nhanKhau);

                if(errorString != null)
                {
                    return errorString;
                }
            }
            return null;
        }

        private string DuyetAllSoHoKhau()
        {
            string errorString = null;
            var ListDuyet = ListSoHoKhauChoDuyet.ToList();
            foreach (var soHoKhau in ListDuyet)
            {
                errorString = DuyetSoHoKhau(soHoKhau);

                if (errorString != null)
                {
                    return errorString;
                }
            }
            return null;
        }

        private string DuyetAllChuyenKhau()
        {
            string errorString = null;
            var ListDuyet = ListChuyenKhauChoDuyet.ToList();
            foreach (var nhanKhau in ListDuyet)
            {
                errorString = DuyetNhanKhau(nhanKhau);

                if (errorString != null)
                {
                    return errorString;
                }
            }
            return null;
        }
        #endregion

        #region Tu choi all
        public string TuChoiAll()
        {
            string errorString = null;
            switch (SelectedLoaiGiayTo.Item1)
            {
                case 0:
                    errorString = TuChoiAllNhanKhau();
                    break;
                case 1:
                    errorString = TuChoiAllSoHoKhau();
                    break;
                case 2:
                    errorString = TuChoiAllChuyenKhau();
                    break;
            }

            return errorString;
        }

        private string TuChoiAllNhanKhau()
        {
            string errorString = null;
            var ListDuyet = ListNhanKhauChoDuyet.ToList();
            foreach (var nhanKhau in ListDuyet)
            {
                errorString = TuChoiDuyetNhanKhau(nhanKhau);

                if (errorString != null)
                {
                    return errorString;
                }
            }
            return null;
        }

        private string TuChoiAllSoHoKhau()
        {
            string errorString = null;
            var ListDuyet = ListSoHoKhauChoDuyet.ToList();
            foreach (var soHoKhau in ListDuyet)
            {
                errorString = TuChoiDuyetSoHoKhau(soHoKhau);

                if (errorString != null)
                {
                    return errorString;
                }
            }
            return null;
        }

        private string TuChoiAllChuyenKhau()
        {
            string errorString = null;
            var ListDuyet = ListChuyenKhauChoDuyet.ToList();
            foreach (var nhanKhau in ListDuyet)
            {
                errorString = TuChoiDuyetChuyenKhau(nhanKhau);

                if (errorString != null)
                {
                    return errorString;
                }
            }
            return null;
        }
        #endregion

        public void refreshData()
        {
            _listNhanKhauChoDuyet = null;
            _listSoHoKhauChoDuyet = null;
            _listChuyenKhauChoDuyet = null;

            OnPropertyChanged("ListNhanKhauChoDuyet");
            OnPropertyChanged("ListSoHoKhauChoDuyet");
            OnPropertyChanged("ListChuyenKhauChoDuyet");
        }
    }
}
