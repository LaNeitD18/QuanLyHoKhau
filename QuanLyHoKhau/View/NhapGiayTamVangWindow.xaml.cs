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
using System.Windows.Shapes;
using QuanLyHoKhau.ViewModel;

namespace QuanLyHoKhau.View
{
    /// <summary>
    /// Interaction logic for NhapGiayTamVangWindow.xaml
    /// </summary>
    public partial class NhapGiayTamVangWindow : Window
    {
        public NhapGiayTamVangWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát?", "Question", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NhapGiayTamVang_VM vm = DataContext as NhapGiayTamVang_VM;
            string result = vm.CheckValidInfo();
            if(result != null)
            {
                MessageBox.Show(result);
            }
            else
            {
                if (vm.Accept() == true)
                {
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra trong quá trình lưu thông tin!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
