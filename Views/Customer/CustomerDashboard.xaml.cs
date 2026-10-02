using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Animal_Spa.Views.Customer
{
    public partial class CustomerDashboard : UserControl
    {
        private const int MockCustomerId = 3;

        public CustomerDashboard()
        {
            InitializeComponent();

            CustomerDetail.LoadCustomer(MockCustomerId);
            CustomerDetail.AddPetRequested += ShowAddPet;
            AddPet.CancelRequested += HideAddPet;
        }

        private void ShowAddPet(int customerId)
        {
            AddPet.PrepareForCustomer(customerId);
            AddPet.Visibility = Visibility.Visible;

            Dispatcher.BeginInvoke(
                new Action(() => AddPet.BringIntoView()),
                DispatcherPriority.Loaded);
        }

        private void HideAddPet()
        {
            AddPet.Visibility = Visibility.Collapsed;

            Dispatcher.BeginInvoke(
                new Action(() => CustomerDetail.BringIntoView()),
                DispatcherPriority.Loaded);
        }
    }
}
