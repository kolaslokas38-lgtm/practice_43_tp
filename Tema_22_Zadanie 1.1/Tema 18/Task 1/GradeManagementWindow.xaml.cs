using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;
using Task_1.Data;
using Task_1.Models;

namespace Task_1.Views
{
    public partial class GradeManagementWindow : Window
    {
        private readonly ApplicationDbContext _context;

        public GradeManagementWindow(ApplicationDbContext context)
        {
            InitializeComponent();
            _context = context;
            _ = LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            StudentComboBox.ItemsSource = await _context.Students.OrderBy(s => s.Name).ToListAsync();
            CourseComboBox.ItemsSource = await _context.Courses.OrderBy(c => c.Name).ToListAsync();
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (StudentComboBox.SelectedItem is not Student student || CourseComboBox.SelectedItem is not Course course)
            {
                MessageBox.Show("Выберите студента и курс.");
                return;
            }

            var grade = int.Parse(((ComboBoxItem)GradeComboBox.SelectedItem).Content.ToString()!);
            var enrollment = await _context.Enrollments.FirstOrDefaultAsync(e =>
                e.StudentId == student.Id && e.CourseId == course.Id);

            if (enrollment == null)
            {
                await _context.Enrollments.AddAsync(new Enrollment
                {
                    StudentId = student.Id,
                    CourseId = course.Id,
                    Grade = grade
                });
            }
            else
            {
                enrollment.Grade = grade;
                _context.Enrollments.Update(enrollment);
            }

            await _context.SaveChangesAsync();
            MessageBox.Show("Оценка сохранена.", "Готово", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}

