using MaterialDesignThemes.Wpf;
using QuanLyHoKhau.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static QuanLyHoKhau.ViewModel.Duyet_VM;

namespace QuanLyHoKhau.View
{
    /// <summary>
    /// Interaction logic for DuyetPage.xaml
    /// </summary>
    public partial class DuyetPage : Page
    {
        List<Card> ListCards;

        public DuyetPage()
        {
            InitializeComponent();

            if (ListCards == null)
                InitListCards();
        }

        void InitListCards()
        {
            ListCards = new List<Card>();
            ListCards.Add(CdSHK);
            ListCards.Add(CdNhanKhau);
            ListCards.Add(CdChuyenKhau);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListCards == null)
                return;

            foreach(var card in ListCards)
            {
                card.Visibility = Visibility.Hidden;
            }

            var selectedLoaiGiayTo = (e.AddedItems[0] as Tuple<int, string>);

            switch(selectedLoaiGiayTo.Item1)
            {
                case 0:
                    CdNhanKhau.Visibility = Visibility.Visible;
                    break;
                case 1:
                    CdSHK.Visibility = Visibility.Visible;
                    break;
                case 2:
                    CdChuyenKhau.Visibility = Visibility.Visible;
                    break;

            }
        }

        private void btnApproveNhanKhau_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Bạn có chắc muốn duyệt phiếu nhân khẩu này không?", "Question?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                object selectedNhanKhau = (sender as Button).DataContext;

                var vm = DataContext as Duyet_VM;

                string errorMsg = vm.DuyetNhanKhau(selectedNhanKhau);

                if(errorMsg == null)
                {
                    MessageBox.Show("Duyệt thành công");
                }
                else
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + errorMsg);
                }

            }
        }

        private void btnRejectNhanKhau_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn từ chối phiếu nhân khẩu này không?", "Question?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                object selectedNhanKhau = (sender as Button).DataContext;

                var vm = DataContext as Duyet_VM;

                string errorMsg = vm.TuChoiDuyetNhanKhau(selectedNhanKhau);

                if (errorMsg == null)
                {
                    MessageBox.Show("Từ chối thành công");
                }
                else
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + errorMsg);
                }

            }
        }

        private void btnApproveSHK_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn duyệt sổ hộ khẩu này không?", "Question?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                object selectedSoHoKhau = (sender as Button).DataContext;

                var vm = DataContext as Duyet_VM;

                string errorMsg = vm.DuyetSoHoKhau(selectedSoHoKhau);

                if (errorMsg == null)
                {
                    MessageBox.Show("Duyệt thành công");
                }
                else
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + errorMsg);
                }

            }
        }

        private void btnRejectSHK_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn từ chối sổ hộ khẩu này không?", "Question?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                object selectedSoHoKhau = (sender as Button).DataContext;

                var vm = DataContext as Duyet_VM;

                string errorMsg = vm.TuChoiDuyetSoHoKhau(selectedSoHoKhau);

                if (errorMsg == null)
                {
                    MessageBox.Show("Từ chối thành công");
                }
                else
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + errorMsg);
                }

            }
        }

        private void btnApproveChuyenKhau_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn duyệt phiếu chuyển khẩu này không?", "Question?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                object selectedChuyenKhau = (sender as Button).DataContext;

                var vm = DataContext as Duyet_VM;

                string errorMsg = vm.DuyetChuyenKhau(selectedChuyenKhau);

                if (errorMsg == null)
                {
                    MessageBox.Show("Duyệt thành công");
                }
                else
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + errorMsg);
                }

            }
        }

        private void btnRejectChuyenKhau_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn từ chối phiếu chuyển khẩu này không?", "Question?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                object selectedChuyenKhau = (sender as Button).DataContext;

                var vm = DataContext as Duyet_VM;

                string errorMsg = vm.TuChoiDuyetChuyenKhau(selectedChuyenKhau);

                if (errorMsg == null)
                {
                    MessageBox.Show("Từ chối thành công");
                }
                else
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + errorMsg);
                }

            }
        }

        private void BtnDuyetAll_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn duyệt hết không?", "Question?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var vm = DataContext as Duyet_VM;

                string errorMsg = vm.DuyetAll();

                if (errorMsg == null)
                {
                    MessageBox.Show("Duyệt thành công");
                }
                else
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + errorMsg);
                }

            }
        }

        private void btnTuChoiDuyetAll_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn từ chối hết không không?", "Question?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var vm = DataContext as Duyet_VM;

                string errorMsg = vm.TuChoiAll();

                if (errorMsg == null)
                {
                    MessageBox.Show("Từ chối thành công");
                }
                else
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + errorMsg);
                }

            }
        }
    }
}
