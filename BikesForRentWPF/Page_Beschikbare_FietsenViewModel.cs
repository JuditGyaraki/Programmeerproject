using BikesForRentWPF.Models; // Het idee is dat je UI niet rechtstreeks met je data of logica praat. De View bindt zich aan properties en commands van het ViewModel. Het ViewModel vertaalt je applicatielogica naar iets dat de WPF-UI makkelijk kan binden en tonen.
using Microsoft.Data.Sqlite; // Meerdere Views kunnen hetzelfde ViewModel gebruiken
using System;                 
using System.Collections.Generic;
using System.Collections.ObjectModel;      
using System.ComponentModel;               //Commands (ICommand) → acties voor knoppen
using System.Linq;                         //Validatie, UI-state → bv. IsLoading, IsEnabled
using System.Text;                         // overzichtelijker, makkelijker uitbreidbaar, makkelijker te debuggen, makkelijker herbruikbaar, meer  structuur
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;


namespace BikesForRentWPF
{
    public class Page_Beschikbare_FietsenViewModel : INotifyPropertyChanged ////WPF kan automatisch de UI updaten wanneer properties veranderen
    {
        private int _days;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public ObservableCollection<Bike> AvailableBikes { get; set; } //lijsten die live updaten

        //public string SelectedFilter { get; set; } = "Alles";

        private string _selectedFilter = "Alles";

        public string SelectedFilter
        {
            get => _selectedFilter;
            set
            {
                _selectedFilter = value;
                OnPropertyChanged();

                // 👇 belangrijk: filter opnieuw toepassen
                AvailableBikesView?.Refresh();
            }
        }

        private ICollectionView _availableBikesView;
        public ICollectionView AvailableBikesView
        {
            get => _availableBikesView;
            set
            {
                _availableBikesView = value;
                OnPropertyChanged();
            }
        }

        public Page_Beschikbare_FietsenViewModel(int days)
        {
            _days = days;

            AvailableBikes = new ObservableCollection<Bike>();
            LoadAvailableBikes();

            AvailableBikesView = CollectionViewSource.GetDefaultView(AvailableBikes);


            AvailableBikesView.Filter = b =>
            {
                var bike = (Bike)b;



                return SelectedFilter switch
                {
                    "Dame" => bike.Sex == "Dames",
                    "Heren" => bike.Sex == "Man",
                    "Kinderen" => bike.Sex == "Kind",
                    _ => true
                };
            };
        }

        private void LoadAvailableBikes()
        {
            string connectionString = "Data Source=BikesForRentDatabase.db";

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT Id, Name, ImagePath, Sex, Priceperday FROM Bikes WHERE Number > 0";

                using (var command = new SqliteCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        AvailableBikes.Add(new Bike
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            ImagePath = new Uri($"pack://siteoforigin:,,,/{reader.GetString(2)}"),
                            Sex = reader.IsDBNull(3) ? "" : reader.GetString(3),
                            Priceperday = reader.GetDouble(4),
                            Days = _days
                        });
                    }
                }
            }
        }
        public IEnumerable<Bike> SelectedBikes =>
           AvailableBikes.Where(b => b.IsSelected);

        private DateTime? _startDate;
        public DateTime? StartDate
        {
            get => _startDate;
            set
            {
                if (value < DateTime.Today)
                {
                    MessageBox.Show("Startdatum kan niet in het verleden liggen.");
                    return;
                }

                _startDate = value;

                if (EndDate == null || EndDate < _startDate)
                {
                    EndDate = _startDate;
                }

                UpdateDaysForBikes();

                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalPrice));
                OnPropertyChanged(nameof(TotalDays));
            }
        }

       
        private DateTime? _endDate;
        public DateTime? EndDate
        {
            get => _endDate;
            set
            {
                if (value < StartDate)
                {
                    MessageBox.Show("Einddatum kan niet vóór startdatum liggen.");
                    return;
                }

                _endDate = value;

                UpdateDaysForBikes();
                OnPropertyChanged(nameof(EndDate));
                OnPropertyChanged(nameof(TotalPrice));//?
                OnPropertyChanged(nameof(TotalDays));
            }
        }

        public int TotalDays
        {
            get
            {
                if (StartDate == null || EndDate == null)
                    return 0;

                return Math.Max(0, (EndDate.Value - StartDate.Value).Days);
            }
        }

        public double TotalPrice
        {
            get
            {
                if (StartDate == null || EndDate == null)
                    return 0;

                int days = Math.Max(0, (EndDate.Value - StartDate.Value).Days);

                return AvailableBikes
                    .Where(b => b.IsSelected)
                    .Sum(b => (b.Priceperday ?? 0) * days);
            }
        }
        private void btn_Verder_Click(object sender, RoutedEventArgs e)
        {
            int totalDays = TotalDays; // of je berekening hier
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.frame.Navigate(new Page_Beschikbare_Fietsen(totalDays)); //voor een clean page PAGENAME AANPASSEN
        }

        private void UpdateDaysForBikes()
        {
            if (StartDate == null || EndDate == null)
                return;

            int days = Math.Max(0, (EndDate.Value - StartDate.Value).Days);

            foreach (var bike in AvailableBikes)
            {
                bike.Days = days;
            }
            
        }


    }
}
