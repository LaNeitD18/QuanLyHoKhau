using QuanLyHoKhau.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using QuanLyHoKhau.Model;

namespace QuanLyHoKhau.ViewModel
{
    class QuanLyTamVang_VM : BaseViewModel
    {
        public ICommand BtnAddTamVang_Command { get; set; }

        public QuanLyTamVang_VM()
        {
            BtnAddTamVang_Command = new RelayCommand((p) => {
                NhapGiayTamVangWindow nhapGiayTamVangWindow = new NhapGiayTamVangWindow();
                nhapGiayTamVangWindow.ShowDialog();
                this.FilterListPhieuTamVang();
            });
        }

        ObservableCollection<PHIEUKHAIBAOTAMVANG> _listPhieuTamVang;
        public ObservableCollection<PHIEUKHAIBAOTAMVANG> ListPhieuTamVang
        {
            get
            {
                if(_listPhieuTamVang == null)
                {
                    _listPhieuTamVang = new ObservableCollection<PHIEUKHAIBAOTAMVANG>(DataProvider.Ins.DB.PHIEUKHAIBAOTAMVANGs.ToList());
                }
                return _listPhieuTamVang;
            }
            set
            {
                _listPhieuTamVang = value;
            }
        }

        PHIEUKHAIBAOTAMVANG _selectedPhieuTamVang;
        public PHIEUKHAIBAOTAMVANG SelectedPhieuTamVang
        {
            get
            {
                return _selectedPhieuTamVang;
            }

            set
            {
                _selectedPhieuTamVang = value;
            }
        }

        string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set 
            { 
                _searchText = value; 
            }
        }

        public void FilterListPhieuTamVang()
        {
            if(_searchText == "" || _searchText == null)
            {
                _listPhieuTamVang = new ObservableCollection<PHIEUKHAIBAOTAMVANG>(GetDefaultListPhieuTamVang());
            }
            else
            {
                string[] listOption = _searchText.Split(' ');

                List<PHIEUKHAIBAOTAMVANG> listPhieuTamVangFiltered = GetDefaultListPhieuTamVang();

                for (int i = 0; i < listOption.Count();)
                {
                    switch(listOption[i])
                    {
                        case "-id":
                            {
                                listPhieuTamVangFiltered = SearchByMaPhieu(listOption[i + 1], listPhieuTamVangFiltered);
                                i += 2;
                                break;
                            }

                        case "-u":
                            {
                                listPhieuTamVangFiltered = SearchByMaNhanKhau(listOption[i + 1], listPhieuTamVangFiltered);
                                i += 2;
                                break;
                            }
                        case "-d":
                            {
                                DateTime ngayKhaiBao;

                                try
                                {
                                    ngayKhaiBao = DateTime.Parse(listOption[i + 1]);
                                }
                                catch(Exception e)
                                {
                                    i += 2;
                                    break;
                                }

                                listPhieuTamVangFiltered = SearchByNgayKhaiBao(ngayKhaiBao, listPhieuTamVangFiltered);

                                i += 2;
                                break;
                            }
                        default:
                            return;
                    }
                }

                _listPhieuTamVang = new ObservableCollection<PHIEUKHAIBAOTAMVANG>(listPhieuTamVangFiltered);
            }
            OnPropertyChanged("ListPhieuTamVang");
        }

        List<PHIEUKHAIBAOTAMVANG> SearchByMaPhieu(string id, List<PHIEUKHAIBAOTAMVANG> list)
        {
            List<PHIEUKHAIBAOTAMVANG> returnList = new List<PHIEUKHAIBAOTAMVANG>();

            returnList.AddRange(list.Where(x => x.MaPhieuKhaiBao == id));

            return returnList;
        }

        List<PHIEUKHAIBAOTAMVANG> SearchByMaNhanKhau(string id, List<PHIEUKHAIBAOTAMVANG> list)
        {
            List<PHIEUKHAIBAOTAMVANG> returnList = new List<PHIEUKHAIBAOTAMVANG>();

            returnList.AddRange(list.Where(x => x.CMND == id));

            return returnList;
        }

        List<PHIEUKHAIBAOTAMVANG> SearchByNgayKhaiBao(DateTime id, List<PHIEUKHAIBAOTAMVANG> list)
        {
            List<PHIEUKHAIBAOTAMVANG> returnList = new List<PHIEUKHAIBAOTAMVANG>();

            returnList.AddRange(list.Where(x => x.NgayKhaiBao.Date == id.Date));

            return returnList;
        }

        List<PHIEUKHAIBAOTAMVANG> GetDefaultListPhieuTamVang()
        {
            return DataProvider.Ins.DB.PHIEUKHAIBAOTAMVANGs.ToList();
        }

        /// <summary>
        /// Remove PhieuTamVang with PhieuTamVang
        /// If remove success, function return null. Otherwise, it return string with error description
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string DeletePhieuTamVang(object PhieuTamVang)
        {
            var target = PhieuTamVang as PHIEUKHAIBAOTAMVANG;

            target = DataProvider.Ins.DB.PHIEUKHAIBAOTAMVANGs.Find(target.MaPhieuKhaiBao);

            if(DataProvider.Ins.DB.GIAYTAMTRUs.Where(x => x.MaGiayTamVang == target.MaPhieuKhaiBao).ToList().Count > 0)
            {
                return "Giấy tạm vắng này đã được sử dụng để đăng ký tạm trú, không thể xóa.";
            }

            if (target != null)
                DataProvider.Ins.DB.PHIEUKHAIBAOTAMVANGs.Remove(target);

            DataProvider.Ins.DB.SaveChanges();

            return null;
        }
    }
}
