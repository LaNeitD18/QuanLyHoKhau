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
    class QuanLyTamTru_VM : BaseViewModel
    {
        public ICommand BtnAddTamTru_Command { get; set; }

        public QuanLyTamTru_VM()
        {
            BtnAddTamTru_Command = new RelayCommand((p) => {
                NhapGiayTamTruWindow nhapGiayTamTruWindow = new NhapGiayTamTruWindow();
                nhapGiayTamTruWindow.ShowDialog();
            });
        }

        ObservableCollection<GIAYTAMTRU> _listPhieuTamTru;
        public ObservableCollection<GIAYTAMTRU> ListPhieuTamTru
        {
            get
            {
                if(_listPhieuTamTru == null)
                {
                    _listPhieuTamTru = new ObservableCollection<GIAYTAMTRU>(DataProvider.Ins.DB.GIAYTAMTRUs);
                }
                return _listPhieuTamTru;
            }
            set
            {
                _listPhieuTamTru = value;
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

        public void FilterListPhieuTamTru()
        {
            if (_searchText == "" || _searchText == null)
            {
                _listPhieuTamTru = new ObservableCollection<GIAYTAMTRU>(DataProvider.Ins.DB.GIAYTAMTRUs.ToList());
            }
            else
            {
                List<GIAYTAMTRU> listPhieuTamTruFiltered = DataProvider.Ins.DB.GIAYTAMTRUs.Where(x => x.MaGiayTamTru.Contains(_searchText)).ToList();
                _listPhieuTamTru = new ObservableCollection<GIAYTAMTRU>(listPhieuTamTruFiltered);
            }
            OnPropertyChanged("ListPhieuTamTru");
        }
    }
}
