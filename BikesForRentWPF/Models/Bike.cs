using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace BikesForRentWPF.Models;

public partial class Bike : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string name) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Brand { get; set; }

    public string? Type { get; set; }

    public double? Priceperday { get; set; }

    public string? Sex { get; set; }

    public int? Number { get; set; }

    public int? HotelId { get; set; }

    public Uri ImagePath { get; set; }

    private int _days;

    public int Days
    {
        get => _days;
        set
        {
            _days = value;
            OnPropertyChanged(nameof(Days));
            OnPropertyChanged(nameof(TotalPrice));
        }
    }

    public double TotalPrice => (Priceperday ?? 0) * Days;

    //[NotMapped]
    //public bool IsSelected { get; set; }
    private bool _isSelected;

    [NotMapped]
    public bool IsSelected
    {
        get => _isSelected;
        set
        {
            if (_isSelected == value) return;

            _isSelected = value;
            OnPropertyChanged(nameof(IsSelected));
            OnPropertyChanged(nameof(TotalPrice)); // 🔥 BELANGRIJK
        }
    }

    public virtual Hotel? Hotel { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
