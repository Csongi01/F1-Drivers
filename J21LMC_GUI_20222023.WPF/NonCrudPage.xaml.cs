using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
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

namespace J21LMC_GUI_20222023.WPF
{
    /// <summary>
    /// Interaction logic for NonCrudPage.xaml
    /// </summary>
    public partial class NonCrudPage : Page
    {
        NonCrudViewModel viewModel = new NonCrudViewModel();
        public NonCrudPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
            lb1.ItemsSource = viewModel.PTF_out;            

        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            lb1.ItemsSource = viewModel.TOP2_out;
        }

        private void Best(object sender, RoutedEventArgs e)
        {
            lb1.ItemsSource = viewModel.Best_out;
            ;
        }

        private void Mogyorod(object sender, RoutedEventArgs e)
        {
            lb1.ItemsSource = viewModel.Mogyorod_out;
        }

        private void AVG(object sender, RoutedEventArgs e)
        {
            lb1.ItemsSource = viewModel.AVG_out;
        }
    }
}
