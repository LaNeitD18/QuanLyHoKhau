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
using QuanLyHoKhau.ViewModel;

namespace QuanLyHoKhau.View
{
    /// <summary>
    /// Interaction logic for NhapGiayTamTruPage.xaml
    /// </summary>
    public partial class QuanLyTamTruPage : Page
    {
        public QuanLyTamTruPage()
        {
            InitializeComponent();
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                QuanLyTamTru_VM vm = DataContext as QuanLyTamTru_VM;
                vm.FilterListPhieuTamTru();
                e.Handled = true;
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            QuanLyTamTru_VM vm = DataContext as QuanLyTamTru_VM;

            if (MessageBox.Show("Bạn có chắc muốn xóa Phiếu Tạm Trú này không?", "Question?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                string error = vm.DeletePhieuTamTru((sender as Button).DataContext);
                if (error != null)
                {
                    MessageBox.Show(error);
                }
                vm.FilterListPhieuTamTru();
            }

            e.Handled = true;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            QuanLyTamTru_VM vm = DataContext as QuanLyTamTru_VM;

            ChinhSuaGiayTamTru chinhSuaWindow = new ChinhSuaGiayTamTru();
            ChinhSuaTamTru_VM chinhSuaVM = chinhSuaWindow.DataContext as ChinhSuaTamTru_VM;

            chinhSuaVM.InitMaPhieu((sender as Button).DataContext);

            chinhSuaWindow.ShowDialog();

            vm.FilterListPhieuTamTru();

            e.Handled = true;
        }
    }
}
