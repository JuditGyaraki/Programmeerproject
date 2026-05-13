using BikesForRentWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for Page_Beschikbare_Fietsen.xaml
    /// </summary>
    public partial class Page_Beschikbare_Fietsen : Page
    {
        private int _days;
        public Page_Beschikbare_Fietsen(int days)
        {
            InitializeComponent();
            //DataContext = App.SharedVM;
            DataContext = new Page_Beschikbare_FietsenViewModel(days);
            _days = days;
        }

        //Scrolling probleem oplossen
        private void ListView_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            e.Handled = true;

            var scrollViewer = FindParent<ScrollViewer>((DependencyObject)sender);
            if (scrollViewer != null)
            {
                scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - e.Delta);
            }
        }

        private static T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parent = VisualTreeHelper.GetParent(child);

            while (parent != null && parent is not T)
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            return parent as T;
        }

        private void btn_Terug_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.frame.Navigate(new Page_Agenda()); //voor een clean page PAGENAME AANPASSEN
        }
        private void btn_Verder_Click(object sender, RoutedEventArgs e)
        {
            var vm = (Page_Beschikbare_FietsenViewModel)DataContext;

            var mainWindow = (MainWindow)Application.Current.MainWindow;

            mainWindow.frame.Navigate(
                new Page_Winkelkar(new ObservableCollection<Bike>(vm.SelectedBikes), _days)
            );
        }

    }
}
