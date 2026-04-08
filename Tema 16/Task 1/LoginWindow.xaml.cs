using System.Windows;

namespace JournalApp
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();

            LoginButton.Click += (s, e) =>
            {
                string login = LoginTextBox.Text;
                string password = PasswordBox.Password;

                if (login == "admin" && password == "123")
                {
                    MessageBox.Show("Добро пожаловать!", "Успех",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль!", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            };
        }
    }
}