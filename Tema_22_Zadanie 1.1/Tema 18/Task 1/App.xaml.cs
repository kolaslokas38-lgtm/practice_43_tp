using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Data.Sqlite;
using System.Linq;
using System.Windows;
using Task_1.Data;
using Task_1.Models;
using Task_1.Repositories;
using Task_1.Services;
using Task_1.ViewModels;
using Task_1.Views;

namespace Task_1
{
    public partial class App : Application
    {
        private ServiceProvider? _serviceProvider;

        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                ShutdownMode = ShutdownMode.OnExplicitShutdown;
                var services = new ServiceCollection();

                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlite("Data Source=journal.db"));

                services.AddScoped<IStudentRepository, StudentRepository>();
                services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
                services.AddScoped<ICourseRepository, CourseRepository>();
                services.AddScoped<AuthService>();

                _serviceProvider = services.BuildServiceProvider();

                using (var scope = _serviceProvider.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    EnsureCompatibleSchema(context);
                    context.Database.EnsureCreated();

                    if (!context.Students.Any())
                    {
                        context.Students.Add(new Models.Student { Name = "Пётр Иванов" });
                        context.Students.Add(new Models.Student { Name = "Ольга Смирнова" });
                        context.Students.Add(new Models.Student { Name = "Анна Кузнецова" });
                        context.SaveChanges();
                    }

                    if (!context.Courses.Any())
                    {
                        context.Courses.AddRange(
                            new Course { Name = "Математика" },
                            new Course { Name = "Физика" },
                            new Course { Name = "Программирование" },
                            new Course { Name = "Химия" },
                            new Course { Name = "Биология" });
                        context.SaveChanges();
                    }

                    if (!context.Enrollments.Any())
                    {
                        var students = context.Students.ToList();
                        var courses = context.Courses.ToDictionary(c => c.Name, c => c.Id);

                        context.Enrollments.Add(new Models.Enrollment
                        {
                            StudentId = students[0].Id,
                            CourseId = courses["Математика"],
                            Grade = 5
                        });
                        context.Enrollments.Add(new Models.Enrollment
                        {
                            StudentId = students[0].Id,
                            CourseId = courses["Физика"],
                            Grade = 4
                        });
                        context.Enrollments.Add(new Models.Enrollment
                        {
                            StudentId = students[1].Id,
                            CourseId = courses["Программирование"],
                            Grade = 5
                        });
                        context.Enrollments.Add(new Models.Enrollment
                        {
                            StudentId = students[1].Id,
                            CourseId = courses["Химия"],
                            Grade = 3
                        });
                        context.Enrollments.Add(new Models.Enrollment
                        {
                            StudentId = students[2].Id,
                            CourseId = courses["Биология"],
                            Grade = 4
                        });
                        context.SaveChanges();
                    }
                }

                using var authScope = _serviceProvider.CreateScope();
                var authService = authScope.ServiceProvider.GetRequiredService<AuthService>();
                var contextForUsers = authScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                if (!contextForUsers.Users.Any())
                {
                    _ = authService.RegisterAsync("teacher", "teacher123", UserRole.Teacher).GetAwaiter().GetResult();
                }

                authService.EnsureStudentUsersForExistingStudentsAsync().GetAwaiter().GetResult();

                var authWindow = new AuthWindow(authService);
                if (authWindow.ShowDialog() != true || authWindow.AuthenticatedUser == null)
                {
                    Shutdown();
                    return;
                }

                var mainScope = _serviceProvider.CreateScope();
                var currentUser = authWindow.AuthenticatedUser;
                var mainWindow = new MainWindow(
                    mainScope.ServiceProvider.GetRequiredService<ApplicationDbContext>(),
                    new JournalViewModel(
                        currentUser,
                        mainScope.ServiceProvider.GetRequiredService<ApplicationDbContext>(),
                        mainScope.ServiceProvider.GetRequiredService<IStudentRepository>(),
                        mainScope.ServiceProvider.GetRequiredService<IEnrollmentRepository>()));
                MainWindow = mainWindow;
                ShutdownMode = ShutdownMode.OnMainWindowClose;
                mainWindow.Show();

                base.OnStartup(e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Ошибка запуска приложения: {ex.Message}",
                    "Критическая ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                Shutdown();
            }
        }

        private static void EnsureCompatibleSchema(ApplicationDbContext context)
        {
            if (!context.Database.CanConnect())
            {
                return;
            }

            var shouldRecreateDatabase = false;

            using var connection = new SqliteConnection("Data Source=journal.db");
            connection.Open();

            var requiredTables = new[] { "Students", "Enrollments", "Courses", "Users", "ChatMessages" };
            foreach (var table in requiredTables)
            {
                using var checkTableCmd = connection.CreateCommand();
                checkTableCmd.CommandText = "SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name=$name";
                checkTableCmd.Parameters.AddWithValue("$name", table);
                var exists = Convert.ToInt32(checkTableCmd.ExecuteScalar()) > 0;
                if (!exists)
                {
                    shouldRecreateDatabase = true;
                    break;
                }
            }

            if (!shouldRecreateDatabase)
            {
                using var checkColumnCmd = connection.CreateCommand();
                checkColumnCmd.CommandText = "PRAGMA table_info('Enrollments')";
                using var reader = checkColumnCmd.ExecuteReader();
                var hasCourseId = false;
                while (reader.Read())
                {
                    if (reader["name"]?.ToString() == "CourseId")
                    {
                        hasCourseId = true;
                        break;
                    }
                }

                if (!hasCourseId)
                {
                    shouldRecreateDatabase = true;
                }
            }

            connection.Close();

            if (shouldRecreateDatabase)
            {
                context.Database.EnsureDeleted();
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _serviceProvider?.Dispose();
            base.OnExit(e);
        }
    }
}