using Microsoft.EntityFrameworkCore;
using System.Windows;
using Task_1.Data;
using Task_1.Models;

namespace Task_1.Views
{
    public partial class CoursesWindow : Window
    {
        private readonly ApplicationDbContext _context;

        public CoursesWindow(ApplicationDbContext context)
        {
            InitializeComponent();
            _context = context;
            _ = ReloadAsync();
        }

        private async Task ReloadAsync()
        {
            CoursesGrid.ItemsSource = await _context.Courses.OrderBy(c => c.Name).ToListAsync();
        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var name = NewCourseTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                return;
            }

            await _context.Courses.AddAsync(new Course { Name = name });
            await _context.SaveChangesAsync();
            NewCourseTextBox.Text = "";
            await ReloadAsync();
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (CoursesGrid.SelectedItem is not Course course)
            {
                return;
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            await ReloadAsync();
        }
    }
}

