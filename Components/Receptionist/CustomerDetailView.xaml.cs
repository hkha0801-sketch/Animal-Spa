using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Animal_Spa.Models;

namespace Animal_Spa.Components.Receptionist
{
    public partial class CustomerDetailView : UserControl
    {
        public int? CurrentCustomerId { get; private set; }

        public CustomerDetailView()
        {
            InitializeComponent();
        }

        public void LoadCustomer(int customerId)
        {
            CustomerDetailItem customer = BuildMockCustomer(customerId);
            CurrentCustomerId = customer.Id;

            CustomerNameText.Text = customer.Name;
            CustomerPhoneText.Text = customer.Phone;
            AddressText.Text = customer.Address;
            NoteText.Text = customer.Note;

            CustomerPetIconsControl.ItemsSource = customer.Pets;
            PetItemsControl.ItemsSource = customer.Pets;
        }

        private static CustomerDetailItem BuildMockCustomer(int customerId)
        {
            if (customerId == 3)
            {
                return new CustomerDetailItem
                {
                    Id = 3,
                    Name = "KHA BÙI",
                    Phone = "+84xxxxxx647",
                    Address = "335, LONG HÀO, THỦ ĐỨC",
                    Note = "YÊU CẦU CAO VỀ VỆ SINH LÔNG",
                    Pets = new List<PetDetailItem>
                    {
                        CreatePet(301, "BICKY", "Cat", "MIU", "12/06/2021", "CAT.png"),
                        CreatePet(302, "BICKY", "Cat", "MIU", "12/06/2021", "CAT.png"),
                        CreatePet(303, "BICKY", "Dog", "GOLDEN", "12/06/2021", "DOG.png")
                    }
                };
            }

            int petCount = customerId == 5 ? 2 : 1;
            var pets = new List<PetDetailItem>
            {
                CreatePet(customerId * 100 + 1, "MILO", "Dog", "CORGI", "10/08/2022", "DOG.png")
            };

            if (petCount > 1)
            {
                pets.Add(CreatePet(customerId * 100 + 2, "MIMI", "Cat", "MIU", "18/03/2023", "CAT.png"));
            }

            return new CustomerDetailItem
            {
                Id = customerId,
                Name = "VÕ HẢI",
                Phone = "+84xxxxxx647",
                Address = "THỦ ĐỨC, TP. HỒ CHÍ MINH",
                Note = "CHƯA CÓ GHI CHÚ ĐẶC BIỆT",
                Pets = pets
            };
        }

        private static PetDetailItem CreatePet(
            int id,
            string name,
            string type,
            string breed,
            string dateOfBirth,
            string iconFile)
        {
            return new PetDetailItem
            {
                Id = id,
                Name = name,
                Type = type,
                Breed = breed,
                DateOfBirth = dateOfBirth,
                AnimalIcon = $"/Animal-Spa;component/Asset/animal/{iconFile}"
            };
        }

        private void EditCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentCustomerId is int customerId)
            {
                MessageBox.Show(
                    $"Edit Customer component will be connected for customer #{customerId}.",
                    "Animal Spa",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }

        private void AddPet_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentCustomerId is int customerId)
            {
                MessageBox.Show(
                    $"Add Pet component will be connected for customer #{customerId}.",
                    "Animal Spa",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }
    }
}
