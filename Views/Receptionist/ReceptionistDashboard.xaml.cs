using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Animal_Spa.Views.Receptionist
{
    public partial class ReceptionistDashboard : UserControl
    {
        public ReceptionistDashboard()
        {
            InitializeComponent();

            CustomerList.ViewDetailRequested += ShowCustomerDetail;
            CustomerList.AddCustomerRequested += ShowAddCustomer;

            AddCustomer.CancelRequested += HideAddCustomer;
            CustomerDetail.AddPetRequested += ShowAddPet;
            AddPet.CancelRequested += HideAddPet;
        }

        private void ShowAddCustomer()
        {
            CustomerDetail.Visibility = Visibility.Collapsed;
            AddPet.Visibility = Visibility.Collapsed;
            AddCustomer.Visibility = Visibility.Visible;

            Dispatcher.BeginInvoke(
                new Action(() => AddCustomer.BringIntoView()),
                DispatcherPriority.Loaded);
        }

        private void HideAddCustomer()
        {
            AddCustomer.Visibility = Visibility.Collapsed;

            Dispatcher.BeginInvoke(
                new Action(() => CustomerList.BringIntoView()),
                DispatcherPriority.Loaded);
        }

        private void ShowCustomerDetail(int customerId)
        {
            AddCustomer.Visibility = Visibility.Collapsed;
            AddPet.Visibility = Visibility.Collapsed;

            CustomerDetail.LoadCustomer(customerId);
            CustomerDetail.Visibility = Visibility.Visible;

            Dispatcher.BeginInvoke(
                new Action(() => CustomerDetail.BringIntoView()),
                DispatcherPriority.Loaded);
        }

        private void ShowAddPet(int customerId)
        {
            AddCustomer.Visibility = Visibility.Collapsed;
            CustomerDetail.Visibility = Visibility.Visible;

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
