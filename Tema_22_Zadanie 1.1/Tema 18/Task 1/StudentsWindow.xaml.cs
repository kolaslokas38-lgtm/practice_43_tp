using Microsoft.EntityFrameworkCore;
using System.Windows;
using Task_1.Data;
using Task_1.Models;

namespace Task_1.Views
{
    public partial class StudentsWindow : Window
    {
        private readonly ApplicationDbContext _context;

        public StudentsWindow(ApplicationDbContext context)
        {
            InitializeComponent();
            _context = context;
            _ = ReloadAsync();
        }

        private async Task ReloadAsync()
        {
            StudentsGrid.ItemsSource = await _context.Students.OrderBy(s => s.Name).ToListAsync();
        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var name = NewStudentTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                return;
            }

            await _context.Students.AddAsync(new Student { Name = name });
            await _context.SaveChangesAsync();
            NewStudentTextBox.Text = "";
            await ReloadAsync();
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (StudentsGrid.SelectedItem is not Student student)
            {
                return;
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            await ReloadAsync();
        }
    }
}

