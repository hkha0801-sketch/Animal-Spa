using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Animal_Spa.Components.Receptionist
{
    public partial class RoomStaffView : UserControl
    {
        private readonly Brush _selectedBorder = new SolidColorBrush(Color.FromRgb(62, 132, 216));
        private readonly Brush _normalBorder = Brushes.Transparent;
        private readonly Brush _selectedDot = new SolidColorBrush(Color.FromRgb(168, 201, 232));

        public event Action? CancelRequested;
        public event Action? Confirmed;

        public string SelectedRoom { get; private set; } = "ROOM 1";
        public string SelectedStaff { get; private set; } = "Nguyen Van B";

        public RoomStaffView()
        {
            InitializeComponent();
        }

        public void PrepareSelection()
        {
            SelectRoom(Room1Button, Room1Outer, Room1Inner, "ROOM 1");
            SelectStaff(StaffBButton, StaffBOuter, StaffBInner, "Nguyen Van B");
        }

        private void Room_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not Button button || button.Tag is not string room)
            {
                return;
            }

            if (button == Room1Button)
            {
                SelectRoom(Room1Button, Room1Outer, Room1Inner, room);
            }
            else if (button == Room2Button)
            {
                SelectRoom(Room2Button, Room2Outer, Room2Inner, room);
            }
            else if (button == Room3Button)
            {
                SelectRoom(Room3Button, Room3Outer, Room3Inner, room);
            }
        }

        private void Staff_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not Button button || button.Tag is not string staff)
            {
                return;
            }

            if (button == StaffAButton)
            {
                SelectStaff(StaffAButton, StaffAOuter, StaffAInner, staff);
            }
            else if (button == StaffBButton)
            {
                SelectStaff(StaffBButton, StaffBOuter, StaffBInner, staff);
            }
            else if (button == StaffCButton)
            {
                SelectStaff(StaffCButton, StaffCOuter, StaffCInner, staff);
            }
        }

        private void SelectRoom(Button selectedButton, Ellipse selectedOuter, Ellipse selectedInner, string room)
        {
            ResetRooms();
            selectedButton.BorderBrush = _selectedBorder;
            selectedOuter.Stroke = _selectedBorder;
            selectedInner.Fill = _selectedDot;
            SelectedRoom = room;
            SelectedRoomText.Text = room.Replace("ROOM ", string.Empty);
        }

        private void SelectStaff(Button selectedButton, Ellipse selectedOuter, Ellipse selectedInner, string staff)
        {
            ResetStaff();
            selectedButton.BorderBrush = _selectedBorder;
            selectedOuter.Stroke = _selectedBorder;
            selectedInner.Fill = _selectedDot;
            SelectedStaff = staff;
            SelectedStaffText.Text = staff;
        }

        private void ResetRooms()
        {
            ResetChoice(Room1Button, Room1Outer, Room1Inner);
            ResetChoice(Room2Button, Room2Outer, Room2Inner);
            ResetChoice(Room3Button, Room3Outer, Room3Inner);
        }

        private void ResetStaff()
        {
            ResetChoice(StaffAButton, StaffAOuter, StaffAInner);
            ResetChoice(StaffBButton, StaffBOuter, StaffBInner);
            ResetChoice(StaffCButton, StaffCOuter, StaffCInner);
        }

        private void ResetChoice(Button button, Ellipse outer, Ellipse inner)
        {
            button.BorderBrush = _normalBorder;
            outer.Stroke = Brushes.Black;
            inner.Fill = Brushes.Transparent;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            CancelRequested?.Invoke();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            Confirmed?.Invoke();
        }
    }
}
