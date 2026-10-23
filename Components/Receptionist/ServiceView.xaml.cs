using System.Collections.ObjectModel;
using Animal_Spa.Models;
using System.Windows;
using System.Windows.Controls;

namespace Animal_Spa.Components.Receptionist
{
    public partial class ServiceView : UserControl
    {
        public event System.Action<string, string, string>? ServiceSelected;
        public ObservableCollection<ServiceOption> Services { get; } = new();

        public ServiceView()
        {
            InitializeComponent();
            ServiceItems.ItemsSource = Services;
            SetServices("Premium Bath", "Deep clean + conditioner", "90 mins",
                "Basic Bath", "Quick clean + dry", "60 mins",
                "Bath + Dry Combo", "Clean + dry + deodorizing", "80 mins");
        }

        public void LoadCategory(string category)
        {
            CategoryNameText.Text = category;

            switch (category)
            {
                case "Bath & Dry":
                    SetServices(
                        "Premium Bath",
                        "Deep clean + conditioner",
                        "90 mins",
                        "Basic Bath",
                        "Quick clean + dry",
                        "60 mins",
                        "Bath + Dry Combo",
                        "Clean + dry + deodorizing",
                        "80 mins");
                    break;

                case "Hair Trimming":
                    SetServices(
                        "Basic Trim",
                        "Simple coat trimming",
                        "45 mins",
                        "Style Trim",
                        "Shape + styling",
                        "60 mins",
                        "Full Groom",
                        "Complete trim + finishing",
                        "90 mins");
                    break;

                case "Ear Cleaning":
                    SetServices(
                        "Basic Ear Clean",
                        "Gentle outer-ear cleaning",
                        "20 mins",
                        "Deep Ear Clean",
                        "Detailed ear hygiene",
                        "30 mins",
                        "Ear Care",
                        "Clean + deodorizing care",
                        "25 mins");
                    break;

                case "Nail Care":
                    SetServices(
                        "Basic Nail Trim",
                        "Simple nail trimming",
                        "20 mins",
                        "Nail Grinding",
                        "Smooth nail finishing",
                        "30 mins",
                        "Paw Care",
                        "Nail + paw care",
                        "35 mins");
                    break;

                default:
                    SetServices(
                        "Service 1",
                        "Service description",
                        "30 mins",
                        "Service 2",
                        "Service description",
                        "45 mins",
                        "Service 3",
                        "Service description",
                        "60 mins");
                    break;
            }
        }

        private void SetServices(
            string name1,
            string description1,
            string duration1,
            string name2,
            string description2,
            string duration2,
            string name3,
            string description3,
            string duration3)
        {
            Services.Clear();
            Services.Add(new ServiceOption(name1, description1, duration1, 35, new Thickness(0, 0, 30, 0)));
            Services.Add(new ServiceOption(name2, description2, duration2, 35, new Thickness(0, 0, 30, 0)));
            Services.Add(new ServiceOption(name3, description3, duration3, 33, new Thickness(0)));
        }

        private void ServiceCard_Chosen(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement { DataContext: ServiceOption selected })
            {
                foreach (var service in Services)
                    service.IsSelected = ReferenceEquals(service, selected);
                ServiceSelected?.Invoke(selected.Name, selected.Description, selected.Duration);
            }
        }
    }
}
