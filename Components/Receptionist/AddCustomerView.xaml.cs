using System;
using System.Windows;
using System.Windows.Controls;

namespace Animal_Spa.Components.Receptionist
{
    public partial class AddCustomerView : UserControl
    {
        public event Action? CancelRequested;

        public AddCustomerView()
        {
            InitializeComponent();
        }

        private void PhotoPicker_StatusChanged(string message)
        {
            StatusText.Text = message;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            StatusText.Text = "UI ready — Save is not connected to customer creation yet.";
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
            CancelRequested?.Invoke();
        }

        public void ClearForm()
        {
            CustomerNameTextBox.Clear();
            GenderComboBox.SelectedIndex = -1;
            PhoneNumberTextBox.Clear();
            AddressTextBox.Clear();
            NoteTextBox.Clear();
            StatusText.Text = string.Empty;
            PhotoPicker.ResetPhoto();
        }
    }
}
