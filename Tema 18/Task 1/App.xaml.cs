using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Windows;
using Task_1.Data;
using Task_1.Repositories;
using Task_1.ViewModels;
using Task_1.Views;

namespace Task_1
{
    public partial class App : Application
    {
        private ServiceProvider _serviceProvider;

        protected override void OnStartup(StartupEventArgs e)
        {
            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite("Data Source=journal.db"));

            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
            services.AddScoped<JournalViewModel>();

            services.AddSingleton<MainWindow>();

            _serviceProvider = services.BuildServiceProvider();

            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                if (!context.Students.Any())
                {
                    context.Students.Add(new Models.Student { Name = "Пётр Иванов" });
                    context.Students.Add(new Models.Student { Name = "Ольга Смирнова" });
                    context.Students.Add(new Models.Student { Name = "Анна Кузнецова" });
                    context.SaveChanges();
                }

                if (!context.Enrollments.Any())
                {
                    var students = context.Students.ToList();

                    context.Enrollments.Add(new Models.Enrollment
                    {
                        StudentId = students[0].Id,
                        Course = "Математика",
                        Grade = 5
                    });
                    context.Enrollments.Add(new Models.Enrollment
                    {
                        StudentId = students[0].Id,
                        Course = "Физика",
                        Grade = 4
                    });
                    context.Enrollments.Add(new Models.Enrollment
                    {
                        StudentId = students[1].Id,
                        Course = "Программирование",
                        Grade = 5
                    });
                    context.Enrollments.Add(new Models.Enrollment
                    {
                        StudentId = students[1].Id,
                        Course = "Химия",
                        Grade = 3
                    });
                    context.Enrollments.Add(new Models.Enrollment
                    {
                        StudentId = students[2].Id,
                        Course = "Биология",
                        Grade = 4
                    });
                    context.SaveChanges();
                }
            }

            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.DataContext = _serviceProvider.GetRequiredService<JournalViewModel>();
            mainWindow.Show();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _serviceProvider?.Dispose();
            base.OnExit(e);
        }
    }
}