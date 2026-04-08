using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Task.Commands;
using Task.Services;
using Task.Views;

namespace Task.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private AuthService _authService;
        private string _username = "";
        private string _password = "";
        private bool _isRegisterMode;
        private string _registerUsername = "";
        private string _registerPassword = "";
        private string _registerRole = "";

        public LoginViewModel()
        {
            _authService = new AuthService();
            LoginCommand = new RelayCommand(_ => Login(), _ => CanLogin());
            RegisterCommand = new RelayCommand(_ => Register(), _ => CanRegister());
            SwitchModeCommand = new RelayCommand(_ => SwitchMode());
        }

        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        public bool IsRegisterMode
        {
            get => _isRegisterMode;
            set { _isRegisterMode = value; OnPropertyChanged(); }
        }

        public string RegisterUsername
        {
            get => _registerUsername;
            set { _registerUsername = value; OnPropertyChanged(); }
        }

        public string RegisterPassword
        {
            get => _registerPassword;
            set { _registerPassword = value; OnPropertyChanged(); }
        }

        public string RegisterRole
        {
            get => _registerRole;
            set { _registerRole = value; OnPropertyChanged(); }
        }

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }
        public ICommand SwitchModeCommand { get; }

        private bool CanLogin()
        {
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
        }

        private void Login()
        {
            if (_authService.Login(Username, Password))
            {
                var mainWindow = new MainWindow();
                mainWindow.DataContext = new JournalViewModel(_authService);
                mainWindow.Show();
                Application.Current.Windows[0]?.Close();
            }
            else
            {
                MessageBox.Show("Неверное имя пользователя или пароль", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CanRegister()
        {
            return !string.IsNullOrEmpty(RegisterUsername) &&
                   !string.IsNullOrEmpty(RegisterPassword) &&
                   !string.IsNullOrEmpty(RegisterRole);
        }

        private void Register()
        {
            if (_authService.Register(RegisterUsername, RegisterPassword, RegisterRole))
            {
                MessageBox.Show("Регистрация успешна!", "Успех",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                IsRegisterMode = false;
                Username = RegisterUsername;
                Password = RegisterPassword;

                RegisterUsername = "";
                RegisterPassword = "";
                RegisterRole = "";
            }
            else
            {
                MessageBox.Show("Пользователь уже существует", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SwitchMode()
        {
            IsRegisterMode = !IsRegisterMode;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}