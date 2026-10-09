using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Animal_Spa.Components.Receptionist
{
    public partial class ServiceView : UserControl
    {
        public event System.Action<string, string, string>? ServiceSelected;
        private readonly Brush _normalBackground =
            new SolidColorBrush(Color.FromRgb(131, 184, 222));

        private readonly Brush _selectedBackground =
            new SolidColorBrush(Color.FromRgb(126, 184, 226));

        private readonly Brush _selectedBorder =
            new SolidColorBrush(Color.FromRgb(57, 126, 214));

        private readonly Brush _selectedRadio =
            new SolidColorBrush(Color.FromRgb(78, 166, 232));

        public ServiceView()
        {
            InitializeComponent();
            ResetServiceCards();
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

            ResetServiceCards();
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
            Service1Name.Text = name1;
            Service1Description.Text = description1;
            Service1Duration.Text = duration1;

            Service2Name.Text = name2;
            Service2Description.Text = description2;
            Service2Duration.Text = duration2;

            Service3Name.Text = name3;
            Service3Description.Text = description3;
            Service3Duration.Text = duration3;
        }

        private void ServiceCard_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button &&
                int.TryParse(button.Tag?.ToString(), out int index))
            {
                SelectService(index);

                switch (index)
                {
                    case 0:
                        ServiceSelected?.Invoke(Service1Name.Text, Service1Description.Text, Service1Duration.Text);
                        break;
                    case 1:
                        ServiceSelected?.Invoke(Service2Name.Text, Service2Description.Text, Service2Duration.Text);
                        break;
                    case 2:
                        ServiceSelected?.Invoke(Service3Name.Text, Service3Description.Text, Service3Duration.Text);
                        break;
                }
            }
        }

        private void SelectService(int index)
        {
            ResetServiceCards();

            switch (index)
            {
                case 0:
                    SetSelected(ServiceCard1, Radio1);
                    break;
                case 1:
                    SetSelected(ServiceCard2, Radio2);
                    break;
                case 2:
                    SetSelected(ServiceCard3, Radio3);
                    break;
            }
        }

        private void SetSelected(Button card, System.Windows.Shapes.Ellipse radio)
        {
            card.BorderBrush = _selectedBorder;
            card.Background = _selectedBackground;
            radio.Fill = _selectedRadio;
        }

        private void ResetServiceCards()
        {
            ServiceCard1.Background = _normalBackground;
            ServiceCard2.Background = _normalBackground;
            ServiceCard3.Background = _normalBackground;

            ServiceCard1.BorderBrush = Brushes.Transparent;
            ServiceCard2.BorderBrush = Brushes.Transparent;
            ServiceCard3.BorderBrush = Brushes.Transparent;

            Radio1.Fill = Brushes.Transparent;
            Radio2.Fill = Brushes.Transparent;
            Radio3.Fill = Brushes.Transparent;
        }
    }
}
