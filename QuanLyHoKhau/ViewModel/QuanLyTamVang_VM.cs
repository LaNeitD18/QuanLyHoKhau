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
                _listPhieuTamVang = new ObservableCollection<PHIEUKHAIBAOTAMVANG>(DataProvider.Ins.DB.PHIEUKHAIBAOTAMVANGs.ToList());
            }
            else
            {
                List<PHIEUKHAIBAOTAMVANG> listPhieuTamVangFiltered = DataProvider.Ins.DB.PHIEUKHAIBAOTAMVANGs.Where(x => x.MaPhieuKhaiBao.Contains(_searchText)).ToList();
                _listPhieuTamVang = new ObservableCollection<PHIEUKHAIBAOTAMVANG>(listPhieuTamVangFiltered);
            }
            OnPropertyChanged("ListPhieuTamVang");
        }
    }
}
