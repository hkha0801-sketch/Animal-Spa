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
        }

        private void ShowAddCustomer()
        {
            // Keep one contextual component open at a time.
            CustomerDetail.Visibility = Visibility.Collapsed;
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
            // If the user was adding a customer, switch context to detail view.
            AddCustomer.Visibility = Visibility.Collapsed;

            CustomerDetail.LoadCustomer(customerId);
            CustomerDetail.Visibility = Visibility.Visible;

            Dispatcher.BeginInvoke(
                new Action(() => CustomerDetail.BringIntoView()),
                DispatcherPriority.Loaded);
        }
    }
}
