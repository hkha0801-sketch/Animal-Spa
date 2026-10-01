using System.Windows;
using System.Windows.Media;

namespace Animal_Spa
{
    public partial class LoginViews : Window
    {
        // Role mặc định khi mở trang Login
        private string _selectedRole = "Customer";

        // Trạng thái hiện / ẩn Password
        private bool _isPasswordVisible = false;


        // Màu của tab đang được chọn
        private readonly SolidColorBrush ActiveColor =
            new SolidColorBrush(Color.FromRgb(131, 184, 228));


        private readonly SolidColorBrush TransparentColor =
            Brushes.Transparent;


        public LoginViews()
        {
            InitializeComponent();
        }


        // =========================================================
        // USER BUTTON
        // =========================================================
        private void UserButton_Click(object sender, RoutedEventArgs e)
        {
            _selectedRole = "Customer";

            LoginTitle.Text = "USER LOGIN";

            UserButton.Background = ActiveColor;
            UserButton.Foreground = Brushes.White;

            StaffButton.Background = TransparentColor;
            StaffButton.Foreground = Brushes.White;
        }


        // =========================================================
        // STAFF BUTTON
        // =========================================================
        private void StaffButton_Click(object sender, RoutedEventArgs e)
        {
            _selectedRole = "Staff";

            LoginTitle.Text = "STAFF LOGIN";

            StaffButton.Background = ActiveColor;
            StaffButton.Foreground = Brushes.White;

            UserButton.Background = TransparentColor;
            UserButton.Foreground = Brushes.White;
        }


        // =========================================================
        // SHOW / HIDE PASSWORD
        // =========================================================
        private void ShowPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isPasswordVisible)
            {
                // =================================================
                // PASSWORD ĐANG HIỆN
                // -> chuyển lại thành dạng ******
                // =================================================

                PasswordInput.Password = PasswordVisibleInput.Text;

                PasswordVisibleInput.Visibility = Visibility.Collapsed;
                PasswordInput.Visibility = Visibility.Visible;

                ShowPasswordButton.ToolTip = "Show password";

                PasswordInput.Focus();

                _isPasswordVisible = false;
            }
            else
            {
                // =================================================
                // PASSWORD ĐANG ẨN
                // -> hiện password thật
                // =================================================

                PasswordVisibleInput.Text = PasswordInput.Password;

                PasswordInput.Visibility = Visibility.Collapsed;
                PasswordVisibleInput.Visibility = Visibility.Visible;

                ShowPasswordButton.ToolTip = "Hide password";

                PasswordVisibleInput.Focus();

                // Đưa con trỏ về cuối password
                PasswordVisibleInput.CaretIndex =
                    PasswordVisibleInput.Text.Length;

                _isPasswordVisible = true;
            }
        }


        // =========================================================
        // LOGIN BUTTON
        // =========================================================
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text.Trim();


            // Lấy password đúng theo trạng thái hiện tại
            string password = _isPasswordVisible
                ? PasswordVisibleInput.Text
                : PasswordInput.Password;


            ErrorText.Text = "";


            // =====================================================
            // VALIDATE USERNAME
            // =====================================================
            if (string.IsNullOrWhiteSpace(username))
            {
                ErrorText.Text = "Please enter your username.";

                UsernameTextBox.Focus();

                return;
            }


            // =====================================================
            // VALIDATE PASSWORD
            // =====================================================
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


            /*
             * TODO:
             *
             * Sau này gọi AuthService.LoginAsync() ở đây.
             *
             * username
             * password
             * _selectedRole
             *
             * _selectedRole có thể là:
             *
             * Customer
             * Staff
             */


            // TEMP: Trong giai đoạn dựng UI, STAFF sẽ mở Receptionist Dashboard để test.
            // Khi backend login hoàn tất, thay bằng role thật trả về từ API.
            if (_selectedRole == "Staff")
            {
                MainWindow mainWindow = new MainWindow("Receptionist");
                mainWindow.Show();
                Close();
                return;
            }

            MessageBox.Show(
                "Customer dashboard is not implemented yet.",
                "Animal Spa",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
    }
}