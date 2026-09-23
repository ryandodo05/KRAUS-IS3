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

namespace KRAUS_IS3
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSeance2_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Seance2());
        }

        private void btnSeance3_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Seance3());
        }

        private void btnSeance4_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Seance4());
        }

        private void btnSeance5_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Seance5());
        }

        private void btnSeance6_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Seance6());
        }

        private void btnSeance7_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Seance7());
        }

        private void btnSeance8_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Seance8());
        }
    }
}
