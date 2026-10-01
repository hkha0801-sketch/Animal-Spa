using System.Windows;
using Animal_Spa.Views.Receptionist;

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

                default:
                    MainContent.Content = new ReceptionistDashboard();
                    break;
            }
        }
    }
}
