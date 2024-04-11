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

namespace J21LMC_GUI_20222023.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Pilot_Click(object sender, RoutedEventArgs e)
        {
            PilotPage myPage = new PilotPage();            
            frame.Content = myPage;
        }

        private void Team_Click(object sender, RoutedEventArgs e)
        {
            TeamPage myPage = new TeamPage();
            frame.Content = myPage;
        }

        private void Race_Click(object sender, RoutedEventArgs e)
        {
            RacePage myPage = new RacePage();
            frame.Content = myPage;
        }

        private void NonCrud_Click(object sender, RoutedEventArgs e)
        {
            NonCrudPage myPage = new NonCrudPage();
            frame.Content = myPage;
        }
    }
}
