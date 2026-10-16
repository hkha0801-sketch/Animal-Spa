using System;
using System.ComponentModel;
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

            if (DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }

            CustomerList.ViewDetailRequested += ShowCustomerDetail;
            CustomerList.AddCustomerRequested += ShowAddCustomer;

            AddCustomer.CancelRequested += HideAddCustomer;
            CustomerDetail.AddPetRequested += ShowAddPet;
            AddPet.CancelRequested += HideAddPet;
            Category.CategorySelected += ShowServiceCategory;
            Service.ServiceSelected += ShowServiceDetail;
            ServiceDetail.ServiceChosen += AddServiceToBooking;
            ServiceDetail.CloseRequested += HideServiceDetail;
            Booking.SaveRequested += ShowRoomStaff;
            RoomStaff.CancelRequested += HideRoomStaff;
            RoomStaff.Confirmed += CompleteBooking;
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
            Booking.SetCustomer(customerId, CustomerDetail.CurrentCustomerName);
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

        private void ShowServiceCategory(string category)
        {
            RoomStaff.Visibility = Visibility.Collapsed;
            ServiceDetail.Visibility = Visibility.Collapsed;
            Service.LoadCategory(category);
            Service.Visibility = Visibility.Visible;

            Dispatcher.BeginInvoke(
                new Action(() => Service.BringIntoView()),
                DispatcherPriority.Loaded);
        }

        private void ShowServiceDetail(string name, string description, string duration)
        {
            RoomStaff.Visibility = Visibility.Collapsed;
            ServiceDetail.LoadService(name, description, duration);
            ServiceDetail.Visibility = Visibility.Visible;

            Dispatcher.BeginInvoke(
                new Action(() => ServiceDetail.BringIntoView()),
                DispatcherPriority.Loaded);
        }


        private void AddServiceToBooking(Animal_Spa.Models.BookingServiceItem item)
        {
            RoomStaff.Visibility = Visibility.Collapsed;
            Booking.AddService(item);
            ServiceDetail.Visibility = Visibility.Collapsed;

            Dispatcher.BeginInvoke(
                new Action(() => Booking.BringIntoView()),
                DispatcherPriority.Loaded);
        }


        private void ShowRoomStaff()
        {
            RoomStaff.PrepareSelection();
            RoomStaff.Visibility = Visibility.Visible;

            Dispatcher.BeginInvoke(
                new Action(() => RoomStaff.BringIntoView()),
                DispatcherPriority.Loaded);
        }

        private void HideRoomStaff()
        {
            RoomStaff.Visibility = Visibility.Collapsed;

            Dispatcher.BeginInvoke(
                new Action(() => Booking.BringIntoView()),
                DispatcherPriority.Loaded);
        }

        private void CompleteBooking()
        {
            RoomStaff.Visibility = Visibility.Collapsed;
            Booking.ClearBooking();

            Dispatcher.BeginInvoke(
                new Action(() => Booking.BringIntoView()),
                DispatcherPriority.Loaded);
        }

        private void HideServiceDetail()
        {
            ServiceDetail.Visibility = Visibility.Collapsed;

            Dispatcher.BeginInvoke(
                new Action(() => Service.BringIntoView()),
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
