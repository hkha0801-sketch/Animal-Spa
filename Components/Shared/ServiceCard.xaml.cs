using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Animal_Spa.Components.Shared;

/// <summary>A reusable service button. Does not replace the consumer's DataContext.</summary>
public partial class ServiceCard : UserControl
{
    public static readonly DependencyProperty ServiceNameProperty = DependencyProperty.Register(
        nameof(ServiceName), typeof(string), typeof(ServiceCard), new PropertyMetadata(string.Empty));
    public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register(
        nameof(Description), typeof(string), typeof(ServiceCard), new PropertyMetadata(string.Empty));
    public static readonly DependencyProperty DurationProperty = DependencyProperty.Register(
        nameof(Duration), typeof(string), typeof(ServiceCard), new PropertyMetadata(string.Empty));
    public static readonly DependencyProperty TitleFontSizeProperty = DependencyProperty.Register(
        nameof(TitleFontSize), typeof(double), typeof(ServiceCard), new PropertyMetadata(35d));
    public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(
        nameof(IsSelected), typeof(bool), typeof(ServiceCard), new PropertyMetadata(false, SelectionChanged));

    public string ServiceName { get => (string)GetValue(ServiceNameProperty); set => SetValue(ServiceNameProperty, value); }
    public string Description { get => (string)GetValue(DescriptionProperty); set => SetValue(DescriptionProperty, value); }
    public string Duration { get => (string)GetValue(DurationProperty); set => SetValue(DurationProperty, value); }
    public double TitleFontSize { get => (double)GetValue(TitleFontSizeProperty); set => SetValue(TitleFontSizeProperty, value); }
    public bool IsSelected { get => (bool)GetValue(IsSelectedProperty); set => SetValue(IsSelectedProperty, value); }

    public static readonly RoutedEvent ChosenEvent = EventManager.RegisterRoutedEvent(
        nameof(Chosen), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ServiceCard));
    public event RoutedEventHandler Chosen { add => AddHandler(ChosenEvent, value); remove => RemoveHandler(ChosenEvent, value); }

    public ServiceCard()
    {
        InitializeComponent();
        UpdateSelection();
    }

    private void Card_Click(object sender, RoutedEventArgs e) => RaiseEvent(new RoutedEventArgs(ChosenEvent, this));
    private static void SelectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => ((ServiceCard)d).UpdateSelection();

    private void UpdateSelection()
    {
        if (CardButton == null || Radio1 == null) return;
        CardButton.Background = new SolidColorBrush(IsSelected ? Color.FromRgb(126, 184, 226) : Color.FromRgb(131, 184, 222));
        CardButton.BorderBrush = IsSelected ? new SolidColorBrush(Color.FromRgb(57, 126, 214)) : Brushes.Transparent;
        Radio1.Fill = IsSelected ? new SolidColorBrush(Color.FromRgb(78, 166, 232)) : Brushes.Transparent;
    }
}
