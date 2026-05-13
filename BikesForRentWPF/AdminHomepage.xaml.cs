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

namespace BikesForRentWPF
{
    /// <summary>
    /// Interaction logic for AdminHomepage.xaml
    /// </summary>
    public partial class AdminHomepage : Page
    {
        public AdminHomepage()
        {
            InitializeComponent();
        }

        private void btn_Uitloggen_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btn_Reservatie_Aanmaken_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.frame.Navigate(new Page_Agenda()); //voor een clean page
        }


    }
}
