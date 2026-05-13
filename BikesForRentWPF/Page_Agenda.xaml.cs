using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for Page_Agenda.xaml
    /// </summary>
    public partial class Page_Agenda : Page, INotifyPropertyChanged
    {
        public Page_Agenda()
        {
            InitializeComponent();
            DataContext = this; // BELANGRIJK
            StartDate = DateTime.Today;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

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

                // Zorg dat EndDate mee opschuift
                if (EndDate == null || EndDate < _startDate)
                {
                    EndDate = _startDate;
                }

                OnPropertyChanged(nameof(StartDate));
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
                OnPropertyChanged(nameof(EndDate));
                OnPropertyChanged(nameof(TotalDays));
            }
        }

        public int TotalDays
        {
            get
            {
                if (StartDate.HasValue && EndDate.HasValue)
                {
                    return (EndDate.Value - StartDate.Value).Days;
                }

                return 0;
            }
        }
        private void btn_Verder_Click(object sender, RoutedEventArgs e)
        {
            int totalDays = TotalDays;

            var page = new Page_Beschikbare_Fietsen(totalDays);
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.frame.Navigate(page); //voor een clean page PAGENAME AANPASSEN
        }

    }
}


