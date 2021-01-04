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
    /// Interaction logic for ChinhSuaGiayTamVang.xaml
    /// </summary>
    public partial class ChinhSuaGiayTamVang : Window
    {
        public ChinhSuaGiayTamVang()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ChinhSuaTamVang_VM vm = DataContext as ChinhSuaTamVang_VM;
            string result = vm.CheckValidInfo();
            if (result != null)
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
