using System.IO;
using System.Text;
using System.Windows;
using Microsoft.Win32;
using JournalApp.ViewModels;

namespace JournalApp.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ExportToExcel_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is not MainViewModel vm) return;

            var dialog = new SaveFileDialog
            {
                Filter = "CSV файл (*.csv)|*.csv",
                FileName = $"journal_{DateTime.Now:yyyyMMdd_HHmmss}.csv",
                Title = "Сохранить как Excel (CSV)"
            };

            if (dialog.ShowDialog() == true)
            {
                var sb = new StringBuilder();
                sb.AppendLine("Студент,Оценка,Посещаемость,Средний балл");

                foreach (var student in vm.Students)
                {
                    sb.AppendLine($"{student.Name},{student.Grade},\"{(student.IsPresent ? "Да" : "Нет")}\",{student.AverageScore:F2}");
                }

                File.WriteAllText(dialog.FileName, sb.ToString(), Encoding.UTF8);
                MessageBox.Show($"✅ Экспорт выполнен!\n\nФайл сохранён:\n{dialog.FileName}",
                                "Успех",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
            }
        }
    }
}