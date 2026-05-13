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
using BikesForRentWPF.Models;
using System.Collections.ObjectModel;

namespace BikesForRentWPF
{
    /// <summary>
    /// Interaction logic for Page_Winkelkar.xaml
    /// </summary>
    

    public partial class Page_Winkelkar : Page
    {
        private static BikesDbContext db = new BikesDbContext();
        private int _days;
        public Page_Winkelkar(ObservableCollection<Bike> bikes, int days)
        {
            InitializeComponent();

            var vm = new Winkelkar_ViewModel();
            vm.SelectedBikes = bikes;
            DataContext = vm;
        }

        private void btn_Annuleren_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.frame.Navigate(new Login()); //voor een clean page PAGENAME AANPASSEN
        }

        private void btn_Reserveren_Click(object sender, RoutedEventArgs e)
        {
            var vm = (Winkelkar_ViewModel)DataContext;
            //MessageBox.Show(vm.SelectedBikes.Count.ToString()); // tijdelijk

            var TeBetalen = vm.SelectedBikes.Sum(b => (b.Priceperday ?? 0) * b.Days);

            var hotelUserId = db.Hotelusers
                .Where(hu => hu.UserId == Session.UserId)
                .Select(hu => hu.Id)
                .FirstOrDefault();

            foreach (var bike in vm.SelectedBikes)
            {
                Reservation reservation = new Reservation()
                {
                    HoteluserId = hotelUserId,
                    BikeId = bike.Id,
                    Totalprice = TeBetalen,
                    Startdate = DateTime.Now.ToString("yyyy-MM-dd"),
                    Enddate = DateTime.Now.AddDays(bike.Days).ToString("yyyy-MM-dd"),
                    Status = "Booked",
                    Days = bike.Days                
                };
                db.Reservations.Add(reservation);
                MessageBox.Show(
                    $"HotelUserId: {hotelUserId}\n" +
                    $"BikeId: {bike.Id}\n" +
                    $"UserId: {Session.UserId}"
                );
            }
            db.SaveChanges();
            MessageBox.Show("Reservatie opgeslagen!");

        }


    }
}
