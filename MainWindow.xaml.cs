using System.Windows;
using Animal_Spa.Views.Receptionist;
using Animal_Spa.Views.Customer;
using Animal_Spa.Views.Staff;

namespace Animal_Spa
{
    public partial class MainWindow : Window
    {
        public MainWindow(string role = "Receptionist")
        {
            InitializeComponent();
            LoadDashboard(role);
        }

        private void LoadDashboard(string role)
        {
            switch (role)
            {
                case "Receptionist":
                    MainContent.Content = new ReceptionistDashboard();
                    break;

                case "Customer":
                    MainContent.Content = new CustomerDashboard();
                    break;

                case "Staff":
                    MainContent.Content = new StaffDashboard();
                    break;

                default:
                    MainContent.Content = new CustomerDashboard();
                    break;
            }
        }
    }
}
