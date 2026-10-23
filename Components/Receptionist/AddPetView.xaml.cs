using System;
using System.Windows;
using System.Windows.Controls;

namespace Animal_Spa.Components.Receptionist
{
    public partial class AddPetView : UserControl
    {
        public event Action? CancelRequested;

        public int? CurrentCustomerId { get; private set; }
        public AddPetView()
        {
            InitializeComponent();
        }

        public void PrepareForCustomer(int customerId)
        {
            CurrentCustomerId = customerId;
            ClearForm(keepCustomer: true);
        }

        private void PhotoPicker_StatusChanged(string message)
        {
            StatusText.Text = message;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            StatusText.Text = CurrentCustomerId is int customerId
                ? $"UI ready for customer #{customerId} — Save is not connected to pet creation yet."
                : "UI ready — Save is not connected to pet creation yet.";
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            ClearForm(keepCustomer: false);
            CancelRequested?.Invoke();
        }

        public void ClearForm(bool keepCustomer = false)
        {
            PetNameTextBox.Clear();
            SpeciesTextBox.Clear();
            MaleRadioButton.IsChecked = false;
            FemaleRadioButton.IsChecked = false;
            SpecialTextBox.Clear();
            BirthDayTextBox.Clear();
            StatusText.Text = string.Empty;
            PhotoPicker.ResetPhoto();

            if (!keepCustomer)
                CurrentCustomerId = null;
        }
    }
}
