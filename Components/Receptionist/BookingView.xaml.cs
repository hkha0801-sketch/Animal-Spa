using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using Animal_Spa.Models;

namespace Animal_Spa.Components.Receptionist
{
    public partial class BookingView : UserControl
    {
        public sealed class CustomerOption
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
        }

        private readonly List<CustomerOption> _customers = new()
        {
            new CustomerOption { Id = 1, Name = "VÕ HẢI" },
            new CustomerOption { Id = 2, Name = "VÕ HẢI" },
            new CustomerOption { Id = 3, Name = "KHA BÙI" },
            new CustomerOption { Id = 4, Name = "VÕ HẢI" },
            new CustomerOption { Id = 5, Name = "VÕ HẢI" },
            new CustomerOption { Id = 6, Name = "VÕ HẢI" }
        };

        public event Action? SaveRequested;

        public ObservableCollection<BookingServiceItem> BookingItems { get; } = new();

        public int SelectedCustomerId { get; private set; } = 3;
        public string SelectedCustomerName { get; private set; } = "KHA BÙI";

        public BookingView()
        {
            InitializeComponent();
            DataContext = this;

            CustomerComboBox.ItemsSource = _customers;
            CustomerComboBox.SelectedValue = SelectedCustomerId;

            BookingItems.CollectionChanged += (_, _) => UpdateBookingSummary();
            UpdateBookingSummary();
        }

        public void SetCustomer(int customerId, string customerName)
        {
            SelectedCustomerId = customerId;
            SelectedCustomerName = customerName;

            CustomerOption? option = _customers.Find(x => x.Id == customerId);
            if (option == null)
            {
                option = new CustomerOption { Id = customerId, Name = customerName };
                _customers.Add(option);
                CustomerComboBox.Items.Refresh();
            }

            CustomerComboBox.SelectedValue = customerId;
        }

        public void AddService(BookingServiceItem item)
        {
            BookingItems.Add(item);
            BookingItemsControl.ScrollIntoViewIfPossible(item);
        }

        public void ClearBooking()
        {
            BookingItems.Clear();
        }

        private void CustomerComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CustomerComboBox.SelectedItem is CustomerOption customer)
            {
                SelectedCustomerId = customer.Id;
                SelectedCustomerName = customer.Name;
            }
        }

        private void RemoveService_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is BookingServiceItem item)
            {
                BookingItems.Remove(item);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            ClearBooking();
        }

        private void SaveBooking_Click(object sender, RoutedEventArgs e)
        {
            if (BookingItems.Count == 0)
            {
                MessageBox.Show(
                    "Please choose at least one service before saving the booking.",
                    "Animal Spa",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }

            SaveRequested?.Invoke();
        }

        private void UpdateBookingSummary()
        {
            EmptyBookingText.Visibility = BookingItems.Count == 0
                ? Visibility.Visible
                : Visibility.Collapsed;

            TotalServicesText.Text = $"Total services: {BookingItems.Count}";

            decimal total = 0;
            foreach (BookingServiceItem item in BookingItems)
            {
                total += item.PriceValue;
            }

            TotalPriceText.Text = FormatPrice(total);
        }

        private static string FormatPrice(decimal value)
        {
            string formatted = value.ToString("N0", CultureInfo.InvariantCulture).Replace(",", ".");
            return $"{formatted}đ";
        }
    }

    internal static class ItemsControlExtensions
    {
        public static void ScrollIntoViewIfPossible(this ItemsControl itemsControl, object item)
        {
            if (itemsControl.ItemContainerGenerator.ContainerFromItem(item) is FrameworkElement element)
            {
                element.BringIntoView();
                return;
            }

            itemsControl.Dispatcher.BeginInvoke(new Action(() =>
            {
                if (itemsControl.ItemContainerGenerator.ContainerFromItem(item) is FrameworkElement generatedElement)
                {
                    generatedElement.BringIntoView();
                }
            }));
        }
    }
}
