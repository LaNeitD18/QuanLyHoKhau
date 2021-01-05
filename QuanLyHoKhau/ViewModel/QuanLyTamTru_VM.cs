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
                FilterListPhieuTamTru();
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
                _listPhieuTamTru = new ObservableCollection<GIAYTAMTRU>(GetDefaultListPhieuTamTru());
            }
            else
            {
                string[] listOption = _searchText.Split(' ');

                List<GIAYTAMTRU> listPhieuTamVangFiltered = GetDefaultListPhieuTamTru();

                for (int i = 0; i < listOption.Count();)
                {
                    switch (listOption[i])
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
                                catch (Exception e)
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

                _listPhieuTamTru = new ObservableCollection<GIAYTAMTRU>(listPhieuTamVangFiltered);
            }
            OnPropertyChanged("ListPhieuTamTru");
        }

        List<GIAYTAMTRU> SearchByMaPhieu(string id, List<GIAYTAMTRU> list)
        {
            List<GIAYTAMTRU> returnList = new List<GIAYTAMTRU>();

            returnList.AddRange(list.Where(x => x.MaGiayTamTru == id));

            return returnList;
        }

        List<GIAYTAMTRU> SearchByMaNhanKhau(string id, List<GIAYTAMTRU> list)
        {
            List<GIAYTAMTRU> returnList = new List<GIAYTAMTRU>();

            returnList.AddRange(list.Where(x => x.CMND == id));

            return returnList;
        }

        List<GIAYTAMTRU> SearchByNgayKhaiBao(DateTime id, List<GIAYTAMTRU> list)
        {
            List<GIAYTAMTRU> returnList = new List<GIAYTAMTRU>();

            returnList.AddRange(list.Where(x => (x.NgayKhaiBao).Value.Date == id.Date));

            return returnList;
        }

        List<GIAYTAMTRU> GetDefaultListPhieuTamTru()
        {
            return DataProvider.Ins.DB.GIAYTAMTRUs.ToList();
        }

        /// <summary>
        /// Remove PhieuTamTru with PhieuTamTru
        /// If remove success, function return null. Otherwise, it return string with error description
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string DeletePhieuTamTru(object PhieuTamTru)
        {
            var target = PhieuTamTru as GIAYTAMTRU;

            target = DataProvider.Ins.DB.GIAYTAMTRUs.Find(target.MaGiayTamTru);

            if (target == null)
                return "Không tìm thấy phiếu tạm trú tương ứng với id: " + target.MaGiayTamTru.ToString();

            if (target != null)
                DataProvider.Ins.DB.GIAYTAMTRUs.Remove(target);

            DataProvider.Ins.DB.SaveChanges();

            return null;
        }
    }
}
