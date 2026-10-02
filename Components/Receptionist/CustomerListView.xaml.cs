using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Animal_Spa.Models;

namespace Animal_Spa.Components.Receptionist
{
    public partial class CustomerListView : UserControl
    {
        public event Action<int>? ViewDetailRequested;
        public event Action? AddCustomerRequested;

        public ObservableCollection<CustomerListItem> Customers { get; } = new();
        public ICollectionView CustomersView { get; }

        public CustomerListView()
        {
            InitializeComponent();

            Customers.Add(new CustomerListItem { Id = 1, Name = "VÕ HẢI", Phone = "+84xxxxxx647", PetCount = 1 });
            Customers.Add(new CustomerListItem { Id = 2, Name = "VÕ HẢI", Phone = "+84xxxxxx647", PetCount = 1 });
            Customers.Add(new CustomerListItem { Id = 3, Name = "KHA BÙI", Phone = "+84xxxxxx647", PetCount = 3 });
            Customers.Add(new CustomerListItem { Id = 4, Name = "VÕ HẢI", Phone = "+84xxxxxx647", PetCount = 1 });
            Customers.Add(new CustomerListItem { Id = 5, Name = "VÕ HẢI", Phone = "+84xxxxxx647", PetCount = 2 });
            Customers.Add(new CustomerListItem { Id = 6, Name = "VÕ HẢI", Phone = "+84xxxxxx647", PetCount = 1 });

            CustomersView = CollectionViewSource.GetDefaultView(Customers);
            CustomersView.Filter = FilterCustomer;

            DataContext = this;
            UpdateEmptyState();
        }

        private bool FilterCustomer(object item)
        {
            if (item is not CustomerListItem customer)
                return false;

            string keyword = SearchTextBox?.Text?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(keyword))
                return true;

            return customer.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase)
                || customer.Phone.Contains(keyword, StringComparison.OrdinalIgnoreCase);
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchPlaceholder.Visibility = string.IsNullOrEmpty(SearchTextBox.Text)
                ? Visibility.Visible
                : Visibility.Collapsed;

            CustomersView?.Refresh();
            UpdateEmptyState();
        }

        private void UpdateEmptyState()
        {
            if (CustomersView == null)
                return;

            EmptyText.Visibility = CustomersView.IsEmpty
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        private void AddCustomer_Click(object sender, RoutedEventArgs e)
        {
            AddCustomerRequested?.Invoke();
        }

        private void ViewDetail_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is int customerId)
            {
                ViewDetailRequested?.Invoke(customerId);
            }
        }
    }
}
