using System.ComponentModel;
using System.Windows;

namespace Animal_Spa.Models;

/// <summary>Presentation data for a service card, separate from a booked service.</summary>
public sealed class ServiceOption : INotifyPropertyChanged
{
    public string Name { get; }
    public string Description { get; }
    public string Duration { get; }
    public double TitleFontSize { get; }
    public Thickness CardMargin { get; }
    private bool _isSelected;
    public bool IsSelected
    {
        get => _isSelected;
        set
        {
            if (_isSelected == value) return;
            _isSelected = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsSelected)));
        }
    }
    public event PropertyChangedEventHandler? PropertyChanged;
    public ServiceOption(string name, string description, string duration, double titleFontSize, Thickness cardMargin)
    {
        Name = name;
        Description = description;
        Duration = duration;
        TitleFontSize = titleFontSize;
        CardMargin = cardMargin;
    }
}
