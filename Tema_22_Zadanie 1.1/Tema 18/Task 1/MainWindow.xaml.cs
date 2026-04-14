using System.Windows;
using Task_1.Data;
using Task_1.ViewModels;

namespace Task_1.Views
{
    public partial class MainWindow : Window
    {
        private readonly ApplicationDbContext _context;
        private readonly JournalViewModel _viewModel;

        public MainWindow(ApplicationDbContext context, JournalViewModel viewModel)
        {
            InitializeComponent();
            _context = context;
            _viewModel = viewModel;
            DataContext = _viewModel;
        }

        private void OpenStudentsWindow_Click(object sender, RoutedEventArgs e)
        {
            new StudentsWindow(_context) { Owner = this }.ShowDialog();
        }

        private void OpenCoursesWindow_Click(object sender, RoutedEventArgs e)
        {
            new CoursesWindow(_context) { Owner = this }.ShowDialog();
        }

        private void OpenGradeManagementWindow_Click(object sender, RoutedEventArgs e)
        {
            new GradeManagementWindow(_context) { Owner = this }.ShowDialog();
        }

        private async void Refresh_Click(object sender, RoutedEventArgs e)
        {
            await _viewModel.RefreshAsync();
        }

        private async void SendMessage_Click(object sender, RoutedEventArgs e)
        {
            var result = await _viewModel.SendMessageAsync();
            if (!result.Success)
            {
                MessageBox.Show(result.Error, "Чат", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}