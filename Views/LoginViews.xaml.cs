using System.Windows;
using System.Windows.Media;

namespace Animal_Spa
{
    public partial class LoginViews : Window
    {
        private string _selectedRole = "Customer";

        private bool _isPasswordVisible = false;

        private readonly SolidColorBrush ActiveColor =
            new SolidColorBrush(Color.FromRgb(131, 184, 228));

        private readonly SolidColorBrush TransparentColor =
            Brushes.Transparent;

        public LoginViews()
        {
            InitializeComponent();
        }

        private void UserButton_Click(object sender, RoutedEventArgs e)
        {
            _selectedRole = "Customer";

            LoginTitle.Text = "USER LOGIN";

            UserButton.Background = ActiveColor;
            UserButton.Foreground = Brushes.White;

            StaffButton.Background = TransparentColor;
            StaffButton.Foreground = Brushes.White;
        }

        private void StaffButton_Click(object sender, RoutedEventArgs e)
        {
            _selectedRole = "Staff";

            LoginTitle.Text = "STAFF LOGIN";

            StaffButton.Background = ActiveColor;
            StaffButton.Foreground = Brushes.White;

            UserButton.Background = TransparentColor;
            UserButton.Foreground = Brushes.White;
        }

        private void ShowPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isPasswordVisible)
            {

                PasswordInput.Password = PasswordVisibleInput.Text;

                PasswordVisibleInput.Visibility = Visibility.Collapsed;
                PasswordInput.Visibility = Visibility.Visible;

                ShowPasswordButton.ToolTip = "Show password";

                PasswordInput.Focus();

                _isPasswordVisible = false;
            }
            else
            {

                PasswordVisibleInput.Text = PasswordInput.Password;

                PasswordInput.Visibility = Visibility.Collapsed;
                PasswordVisibleInput.Visibility = Visibility.Visible;

                ShowPasswordButton.ToolTip = "Hide password";

                PasswordVisibleInput.Focus();

                PasswordVisibleInput.CaretIndex =
                    PasswordVisibleInput.Text.Length;

                _isPasswordVisible = true;
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text.Trim();

            string password = _isPasswordVisible
                ? PasswordVisibleInput.Text
                : PasswordInput.Password;

            ErrorText.Text = "";

            if (string.IsNullOrWhiteSpace(username))
            {
                ErrorText.Text = "Please enter your username.";

                UsernameTextBox.Focus();

                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                ErrorText.Text = "Please enter your password.";

                if (_isPasswordVisible)
                {
                    PasswordVisibleInput.Focus();
                }
                else
                {
                    PasswordInput.Focus();
                }

                return;
            }

            

            if (_selectedRole == "Staff")
            {
                MainWindow mainWindow = new MainWindow("Receptionist");
                mainWindow.Show();
                Close();
                return;
            }

            MainWindow customerWindow = new MainWindow("Customer");
            customerWindow.Show();
            Close();
        }
    }
}