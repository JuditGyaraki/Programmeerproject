using BikesForRentWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikesForRentWPF
{
    internal class Winkelkar_ViewModel : INotifyPropertyChanged //WPF kan automatisch de UI updaten wanneer properties veranderen
    {
        public ObservableCollection<Bike> SelectedBikes { get; set; }

        public Winkelkar_ViewModel()
        {
            SelectedBikes = new ObservableCollection<Bike>();

            SelectedBikes.CollectionChanged += SelectedBikes_CollectionChanged;
        }

        private void SelectedBikes_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(TotalPrice));
        }

        public double TotalPrice => SelectedBikes.Sum(b => (b.Priceperday ?? 0) * b.Days);

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(name));
        }





    }
}
