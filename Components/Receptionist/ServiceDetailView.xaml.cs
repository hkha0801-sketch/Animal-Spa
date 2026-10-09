using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Animal_Spa.Components.Receptionist
{
    public partial class ServiceDetailView : UserControl
    {
        private readonly Brush _normalCellBackground =
            new SolidColorBrush(Color.FromRgb(168, 201, 232));

        private readonly Brush _selectedCellBackground =
            new SolidColorBrush(Color.FromRgb(78, 166, 232));

        private readonly Brush _selectedBorder =
            new SolidColorBrush(Color.FromRgb(57, 126, 214));

        private readonly Dictionary<string, string> _currentPrices = new();

        public event Action? CloseRequested;

        public ServiceDetailView()
        {
            InitializeComponent();
            SetDefaultPricing();
            ResetSelection();
        }

        public void LoadService(string name, string description, string duration)
        {
            ServiceNameText.Text = name;
            DescriptionText.Text = description;
            DurationText.Text = duration;

            ApplyPricingForService(name);
            ResetSelection();
        }

        private void PriceCell_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not Button button || button.Tag is not string tag)
            {
                return;
            }

            string[] parts = tag.Split('|');
            if (parts.Length != 2)
            {
                return;
            }

            string animalType = parts[0];
            string weight = parts[1];
            string key = $"{animalType}|{weight}";

            if (!_currentPrices.TryGetValue(key, out string? price))
            {
                return;
            }

            ResetPriceButtons();
            button.Background = _selectedCellBackground;
            button.BorderBrush = _selectedBorder;

            AnimalTypeText.Text = animalType;
            WeightText.Text = weight;
            PriceText.Text = price;
        }

        private void ApplyPricingForService(string serviceName)
        {
            switch (serviceName)
            {
                case "Premium Bath":
                    SetPricing("180.000đ", "250.000đ", "150.000đ", "220.000đ", "100.000đ", "160.000đ");
                    break;

                case "Bath + Dry Combo":
                    SetPricing("150.000đ", "220.000đ", "130.000đ", "190.000đ", "85.000đ", "140.000đ");
                    break;

                case "Basic Trim":
                    SetPricing("140.000đ", "200.000đ", "120.000đ", "180.000đ", "80.000đ", "130.000đ");
                    break;

                case "Style Trim":
                    SetPricing("180.000đ", "250.000đ", "160.000đ", "220.000đ", "100.000đ", "150.000đ");
                    break;

                case "Full Groom":
                    SetPricing("250.000đ", "350.000đ", "220.000đ", "320.000đ", "140.000đ", "210.000đ");
                    break;

                case "Basic Ear Clean":
                    SetPricing("60.000đ", "80.000đ", "55.000đ", "75.000đ", "40.000đ", "55.000đ");
                    break;

                case "Deep Ear Clean":
                    SetPricing("90.000đ", "120.000đ", "80.000đ", "110.000đ", "55.000đ", "75.000đ");
                    break;

                case "Ear Care":
                    SetPricing("110.000đ", "140.000đ", "100.000đ", "130.000đ", "65.000đ", "90.000đ");
                    break;

                default:
                    SetDefaultPricing();
                    break;
            }
        }

        private void SetDefaultPricing()
        {
            SetPricing("120.000đ", "180.000đ", "100.000đ", "150.000đ", "70.000đ", "120.000đ");
        }

        private void SetPricing(
            string dogUnder,
            string dogOver,
            string catUnder,
            string catOver,
            string hamsterUnder,
            string hamsterOver)
        {
            DogUnderPrice.Text = dogUnder;
            DogOverPrice.Text = dogOver;
            CatUnderPrice.Text = catUnder;
            CatOverPrice.Text = catOver;
            HamsterUnderPrice.Text = hamsterUnder;
            HamsterOverPrice.Text = hamsterOver;

            _currentPrices.Clear();
            _currentPrices["Dog|Under 10kg"] = dogUnder;
            _currentPrices["Dog|Over 10kg"] = dogOver;
            _currentPrices["Cat|Under 10kg"] = catUnder;
            _currentPrices["Cat|Over 10kg"] = catOver;
            _currentPrices["Hamster|Under 10kg"] = hamsterUnder;
            _currentPrices["Hamster|Over 10kg"] = hamsterOver;
        }

        private void ResetSelection()
        {
            AnimalTypeText.Text = "Choose from table";
            WeightText.Text = "Choose from table";
            PriceText.Text = "Choose from table";
            ResetPriceButtons();
        }

        private void ResetPriceButtons()
        {
            Button[] buttons =
            {
                DogUnderButton,
                DogOverButton,
                CatUnderButton,
                CatOverButton,
                HamsterUnderButton,
                HamsterOverButton
            };

            foreach (Button button in buttons)
            {
                button.Background = _normalCellBackground;
                button.BorderBrush = Brushes.Transparent;
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            CloseRequested?.Invoke();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            CloseRequested?.Invoke();
        }
    }
}
