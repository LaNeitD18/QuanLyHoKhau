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
    /// Interaction logic for NhapGiayTamVangPage.xaml
    /// </summary>
    public partial class QuanLyTamVangPage : Page
    {
        public QuanLyTamVangPage()
        {
            InitializeComponent();
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                QuanLyTamVang_VM vm = DataContext as QuanLyTamVang_VM;
                vm.FilterListPhieuTamVang();
                e.Handled = true;
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            QuanLyTamVang_VM vm = DataContext as QuanLyTamVang_VM;

            ChinhSuaGiayTamVang chinhSuaWindow = new ChinhSuaGiayTamVang();
            ChinhSuaTamVang_VM chinhSuaVM = chinhSuaWindow.DataContext as ChinhSuaTamVang_VM;

            chinhSuaVM.InitMaPhieu((sender as Button).DataContext);

            chinhSuaWindow.ShowDialog();

            vm.FilterListPhieuTamVang();

            e.Handled = true;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            QuanLyTamVang_VM vm = DataContext as QuanLyTamVang_VM;

            if(MessageBox.Show("Bạn có chắc muốn xóa Phiếu Tạm Vắng này không?", "Question?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                vm.DeletePhieuTamVang((sender as Button).DataContext);
                vm.FilterListPhieuTamVang();
            }

            e.Handled = true;
        }
    }
}
