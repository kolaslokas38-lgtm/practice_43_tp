using System.Windows;
using System.Windows.Controls;
using Task_1.Models;
using Task_1.Services;

namespace Task_1.Views
{
    public partial class AuthWindow : Window
    {
        private readonly AuthService _authService;
        public AppUser? AuthenticatedUser { get; private set; }

        public AuthWindow(AuthService authService)
        {
            InitializeComponent();
            _authService = authService;
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (!TryValidateInputs(out var username, out var password))
            {
                return;
            }

            var user = await _authService.LoginAsync(username, password);
            if (user == null)
            {
                MessageBox.Show("Неверный логин или пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            AuthenticatedUser = user;
            DialogResult = true;
            Close();
        }

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (!TryValidateInputs(out var username, out var password))
            {
                return;
            }

            var role = (RoleComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString() == "Преподаватель"
                ? UserRole.Teacher
                : UserRole.Student;

            var result = await _authService.RegisterAsync(username, password, role);
            if (!result.Success)
            {
                MessageBox.Show(result.Error, "Ошибка регистрации", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBox.Show("Регистрация успешна. Теперь можно войти.", "Готово", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private bool TryValidateInputs(out string username, out string password)
        {
            username = UsernameTextBox.Text.Trim();
            password = PasswordBox.Password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Введите логин и пароль.", "Проверка", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (password.Length < 4)
            {
                MessageBox.Show("Пароль должен быть не короче 4 символов.", "Проверка", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            return true;
        }
    }
}

