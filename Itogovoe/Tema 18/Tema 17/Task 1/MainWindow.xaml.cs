using System.Windows;
using System.Windows.Media.Animation;
using JournalApp.ViewModels;

namespace JournalApp
{
    public partial class MainWindow : Window
    {
        private JournalViewModel? _viewModel;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainGrid_Loaded(object sender, RoutedEventArgs e)
        {
            _viewModel = DataContext as JournalViewModel;

            var fadeIn = Resources["FadeInAnimation"] as Storyboard;
            fadeIn?.Begin(MainGrid);

            if (_viewModel != null)
            {
                _viewModel.PropertyChanged += ViewModel_PropertyChanged;
                UpdateProgressBar(_viewModel.ProgressPercent);
            }
        }

        private void ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(JournalViewModel.ProgressPercent) && _viewModel != null)
            {
                UpdateProgressBar(_viewModel.ProgressPercent);
            }
        }

        private void UpdateProgressBar(double percent)
        {
            double maxWidth = 300;
            double targetWidth = (percent / 100) * maxWidth;

            var animation = new DoubleAnimation
            {
                From = 0,
                To = targetWidth,
                Duration = TimeSpan.FromSeconds(0.5),
                EasingFunction = new QuadraticEase()
            };
            ProgressRect.BeginAnimation(WidthProperty, animation);

            PercentText.Text = $"{percent:F0}%";
        }
    }
}