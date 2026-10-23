using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace Animal_Spa.Components.Shared;

/// <summary>Shared image selection with caller-provided fallback artwork.</summary>
public partial class PhotoUpload : UserControl
{
    public static readonly DependencyProperty PhotoFillProperty = DependencyProperty.Register(
        nameof(PhotoFill), typeof(Brush), typeof(PhotoUpload), new PropertyMetadata(Brushes.WhiteSmoke));
    public Brush PhotoFill { get => (Brush)GetValue(PhotoFillProperty); set => SetValue(PhotoFillProperty, value); }
    public static readonly DependencyProperty PhotoPathProperty = DependencyProperty.Register(
        nameof(PhotoPath), typeof(string), typeof(PhotoUpload),
        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, PhotoPathChanged));
    public static readonly DependencyProperty FallbackContentProperty = DependencyProperty.Register(
        nameof(FallbackContent), typeof(object), typeof(PhotoUpload), new PropertyMetadata(null));
    public static readonly DependencyProperty SubjectProperty = DependencyProperty.Register(
        nameof(Subject), typeof(string), typeof(PhotoUpload), new PropertyMetadata("pet"));
    public static readonly DependencyProperty StrokeDashArrayProperty = DependencyProperty.Register(
        nameof(StrokeDashArray), typeof(DoubleCollection), typeof(PhotoUpload), new PropertyMetadata(null));

    public string? PhotoPath { get => (string?)GetValue(PhotoPathProperty); set => SetValue(PhotoPathProperty, value); }
    public object? FallbackContent { get => GetValue(FallbackContentProperty); set => SetValue(FallbackContentProperty, value); }
    public string Subject { get => (string)GetValue(SubjectProperty); set => SetValue(SubjectProperty, value); }
    public DoubleCollection? StrokeDashArray { get => (DoubleCollection?)GetValue(StrokeDashArrayProperty); set => SetValue(StrokeDashArrayProperty, value); }
    public event Action<string>? StatusChanged;

    public PhotoUpload()
    {
        InitializeComponent();
        if (!DesignerProperties.GetIsInDesignMode(this)) LoadDefaultPhoto();
    }

    public void ResetPhoto()
    {
        SetCurrentValue(PhotoPathProperty, null);
        LoadDefaultPhoto();
    }

    private void LoadDefaultPhoto()
    {
        try
        {
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri("pack://application:,,,/Animal-Spa;component/Asset/FRAME-UPLOAD.jpg", UriKind.Absolute);
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();
            bitmap.Freeze();
            ShowPhoto(bitmap);
        }
        catch
        {
            UploadRectangle.Fill = new SolidColorBrush(Color.FromRgb(245, 245, 245));
            FallbackPresenter.Visibility = Visibility.Visible;
        }
    }

    private static BitmapImage ReadPhoto(string path)
    {
        using var stream = File.OpenRead(path);
        var bitmap = new BitmapImage();
        bitmap.BeginInit();
        bitmap.CacheOption = BitmapCacheOption.OnLoad;
        bitmap.StreamSource = stream;
        bitmap.EndInit();
        bitmap.Freeze();
        return bitmap;
    }

    private void ShowPhoto(ImageSource photo)
    {
        UploadRectangle.Fill = new ImageBrush(photo)
        {
            Stretch = Stretch.UniformToFill,
            AlignmentX = AlignmentX.Center,
            AlignmentY = AlignmentY.Center
        };
        FallbackPresenter.Visibility = Visibility.Collapsed;
    }

    private static void PhotoPathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var picker = (PhotoUpload)d;
        if (picker.UploadRectangle == null || DesignerProperties.GetIsInDesignMode(picker)) return;
        if (string.IsNullOrEmpty((string?)e.NewValue)) { picker.LoadDefaultPhoto(); return; }
        try { picker.ShowPhoto(ReadPhoto((string)e.NewValue)); }
        catch (Exception ex) { picker.StatusChanged?.Invoke($"Could not load image: {ex.Message}"); }
    }

    private void ChoosePhoto(object sender, MouseButtonEventArgs e)
    {
        var dialog = new OpenFileDialog
        {
            Title = $"Choose {Subject} photo",
            Filter = "Image files (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp|All files (*.*)|*.*",
            CheckFileExists = true,
            Multiselect = false
        };
        if (dialog.ShowDialog() != true) return;
        try
        {
            var photo = ReadPhoto(dialog.FileName);
            SetCurrentValue(PhotoPathProperty, dialog.FileName);
            ShowPhoto(photo);
            StatusChanged?.Invoke("Photo selected. Click the photo again to replace it.");
        }
        catch (Exception ex) { StatusChanged?.Invoke($"Could not load image: {ex.Message}"); }
    }
}
