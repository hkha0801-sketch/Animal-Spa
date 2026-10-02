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
        }

        private void ShowCustomerDetail(int customerId)
        {
            CustomerDetail.LoadCustomer(customerId);
            CustomerDetail.Visibility = Visibility.Visible;

            // Wait until WPF finishes arranging the newly visible component,
            // then scroll the long Receptionist page to Customer Detail.
            Dispatcher.BeginInvoke(
                new Action(() => CustomerDetail.BringIntoView()),
                DispatcherPriority.Loaded);
        }
    }
}
