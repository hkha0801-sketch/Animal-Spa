using System.Windows;
using Animal_Spa.Services;

namespace Animal_Spa
{
    public partial class MainWindow : Window
    {
        private readonly ApiClient _apiClient = new();

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void CheckApi_Click(
            object sender,
            RoutedEventArgs e)
        {
            StatusText.Text = "Checking...";

            var result = await _apiClient.CheckHealthAsync();

            StatusText.Text = result.Success
                ? $"YES - {result.Message}"
                : $"NO - {result.Message}";
        }
    }
}