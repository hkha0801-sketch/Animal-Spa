using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace Animal_Spa.Components.Receptionist
{
    public partial class AddPetView : UserControl
    {
        public event Action? CancelRequested;

        public int? CurrentCustomerId { get; private set; }
        private string? _selectedPhotoPath;

        public AddPetView()
        {
            InitializeComponent();

            if (DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }

            LoadDefaultUploadFrame();
        }

        public void PrepareForCustomer(int customerId)
        {
            CurrentCustomerId = customerId;
            ClearForm(keepCustomer: true);
        }

        private void LoadDefaultUploadFrame()
        {
            try
            {
                var uri = new Uri(
                    "pack://application:,,,/Animal-Spa;component/Asset/FRAME-UPLOAD.jpg",
                    UriKind.Absolute);

                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = uri;
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                bitmap.Freeze();

                SetUploadImage(bitmap);
            }
            catch
            {
                UploadRectangle.Fill = new SolidColorBrush(Color.FromRgb(245, 245, 245));
                UploadFallback.Visibility = Visibility.Visible;
            }
        }

        private void UploadPhotoArea_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Title = "Choose pet photo",
                Filter = "Image files (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp|All files (*.*)|*.*",
                CheckFileExists = true,
                Multiselect = false
            };

            if (dialog.ShowDialog() != true)
                return;

            try
            {
                var bitmap = LoadBitmapWithoutLockingFile(dialog.FileName);
                SetUploadImage(bitmap);

                _selectedPhotoPath = dialog.FileName;
                StatusText.Text = "Photo selected. Click the photo again to replace it.";
            }
            catch (Exception ex)
            {
                StatusText.Text = $"Could not load image: {ex.Message}";
            }
        }

        private static BitmapImage LoadBitmapWithoutLockingFile(string path)
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

        private void SetUploadImage(ImageSource imageSource)
        {
            UploadRectangle.Fill = new ImageBrush(imageSource)
            {
                Stretch = Stretch.UniformToFill,
                AlignmentX = AlignmentX.Center,
                AlignmentY = AlignmentY.Center
            };

            UploadFallback.Visibility = Visibility.Collapsed;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            StatusText.Text = CurrentCustomerId is int customerId
                ? $"UI ready for customer #{customerId} — Save is not connected to pet creation yet."
                : "UI ready — Save is not connected to pet creation yet.";
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            ClearForm(keepCustomer: false);
            CancelRequested?.Invoke();
        }

        public void ClearForm(bool keepCustomer = false)
        {
            PetNameTextBox.Clear();
            SpeciesTextBox.Clear();
            MaleRadioButton.IsChecked = false;
            FemaleRadioButton.IsChecked = false;
            SpecialTextBox.Clear();
            BirthDayTextBox.Clear();
            StatusText.Text = string.Empty;
            _selectedPhotoPath = null;

            if (!keepCustomer)
                CurrentCustomerId = null;

            UploadRectangle.Fill = new SolidColorBrush(Color.FromRgb(245, 245, 245));
            UploadFallback.Visibility = Visibility.Visible;
            LoadDefaultUploadFrame();
        }
    }
}
