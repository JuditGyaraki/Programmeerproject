using BikesForRentWPF.Models;
using System.Text;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public BikesDbContext db = new BikesDbContext();

        public MainWindow()
        {
            InitializeComponent();
            //frame.Navigate(new Login());
        }

        private void Data_btn_click(object sender, EventArgs e)
        {
            
            frame.Navigate(new Database());
        }

        private void Login_btn_click(object sender, EventArgs e)
        {
            frame.Navigate(new Login());
        }
    }

     
    }