using System.Windows;

namespace Animal_Spa.Components.Shared;

/// <summary>Shared template settings; each form retains its original corner radius.</summary>
public static class ComponentAppearance
{
    public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.RegisterAttached(
        "CornerRadius", typeof(CornerRadius), typeof(ComponentAppearance), new PropertyMetadata(new CornerRadius(16)));
    public static CornerRadius GetCornerRadius(DependencyObject element) => (CornerRadius)element.GetValue(CornerRadiusProperty);
    public static void SetCornerRadius(DependencyObject element, CornerRadius value) => element.SetValue(CornerRadiusProperty, value);
}
