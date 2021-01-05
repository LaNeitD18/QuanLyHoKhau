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
    /// Interaction logic for NhapHoKhauWindow.xaml
    /// </summary>
    public partial class NhapHoKhauWindow : Window
    {
        public NhapHoKhauWindow()
        {
            InitializeComponent();
        }

        private void btnEditNhanKhau_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Object shkItem = (e.Source as Button).DataContext;
                (DataContext as NhapHoKhau_VM)?.EditNhanKhau(shkItem);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
