using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using JournalApp.Data;
using JournalApp.Models;
using JournalApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace JournalApp.ViewModels
{
    public class JournalViewModel : INotifyPropertyChanged
    {
        private readonly JournalDbContext _context;
        private readonly StudentRepository _studentRepository;
        private readonly CourseRepository _courseRepository;
        private readonly EnrollmentRepository _enrollmentRepository;

        private StudentModel? _selectedStudent;
        private CourseModel? _selectedCourse;
        private EnrollmentModel? _selectedEnrollment;
        private GradeModel? _selectedGrade;
        private bool _isLoading;
        private string _studentProgress = "";
        private double _progressPercent;
        private int _newGradeValue = 5;
        private string _newGradeComment = "";

        public ObservableCollection<StudentModel> Students { get; set; }
        public ObservableCollection<CourseModel> Courses { get; set; }
        public ObservableCollection<EnrollmentModel> Enrollments { get; set; }
        public ObservableCollection<GradeModel> Grades { get; set; }

        public ICommand AddGradeCommand { get; set; }
        public ICommand DeleteGradeCommand { get; set; }
        public ICommand EnrollStudentCommand { get; set; }

        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; OnPropertyChanged(); }
        }

        public StudentModel? SelectedStudent
        {
            get => _selectedStudent;
            set
            {
                _selectedStudent = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanAddGrade));
                OnPropertyChanged(nameof(CanDeleteGrade));
                OnPropertyChanged(nameof(CanEnrollStudent));
                if (value != null)
                {
                    _ = LoadStudentDataAsync();
                }
            }
        }

        public CourseModel? SelectedCourse
        {
            get => _selectedCourse;
            set
            {
                _selectedCourse = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanAddGrade));
                OnPropertyChanged(nameof(CanEnrollStudent));
                if (value != null && SelectedStudent != null)
                    _ = LoadStudentDataAsync();
            }
        }

        public EnrollmentModel? SelectedEnrollment
        {
            get => _selectedEnrollment;
            set
            {
                _selectedEnrollment = value;
                OnPropertyChanged();
                _ = LoadGradesForEnrollmentAsync();
            }
        }

        public GradeModel? SelectedGrade
        {
            get => _selectedGrade;
            set
            {
                _selectedGrade = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanDeleteGrade));
                if (value != null)
                {
                    NewGradeValue = value.Value;
                    NewGradeComment = value.Comment;
                }
            }
        }

        public string StudentProgress
        {
            get => _studentProgress;
            set { _studentProgress = value; OnPropertyChanged(); }
        }

        public double ProgressPercent
        {
            get => _progressPercent;
            set { _progressPercent = value; OnPropertyChanged(); }
        }

        public int NewGradeValue
        {
            get => _newGradeValue;
            set { _newGradeValue = value; OnPropertyChanged(); }
        }

        public string NewGradeComment
        {
            get => _newGradeComment;
            set { _newGradeComment = value; OnPropertyChanged(); }
        }

        public bool CanAddGrade => SelectedEnrollment != null;
        public bool CanDeleteGrade => SelectedGrade != null;
        public bool CanEnrollStudent => SelectedStudent != null && SelectedCourse != null;

        public JournalViewModel()
        {
            _context = new JournalDbContext();
            _studentRepository = new StudentRepository(_context);
            _courseRepository = new CourseRepository(_context);
            _enrollmentRepository = new EnrollmentRepository(_context);

            Students = new ObservableCollection<StudentModel>();
            Courses = new ObservableCollection<CourseModel>();
            Enrollments = new ObservableCollection<EnrollmentModel>();
            Grades = new ObservableCollection<GradeModel>();

            AddGradeCommand = new AsyncRelayCommand(_ => AddGradeAsync(), _ => CanAddGrade);
            DeleteGradeCommand = new AsyncRelayCommand(_ => DeleteGradeAsync(), _ => CanDeleteGrade);
            EnrollStudentCommand = new AsyncRelayCommand(_ => EnrollStudentAsync(), _ => CanEnrollStudent);

            _ = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            IsLoading = true;
            try
            {
                await _context.Database.EnsureCreatedAsync();
                await SeedIfEmptyAsync();
                await ReloadStudentsCoursesAsync();
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task SeedIfEmptyAsync()
        {
            if (await _context.Students.AnyAsync() || await _context.Courses.AnyAsync())
                return;

            _context.Students.AddRange(
                new StudentModel { Name = "Иванов Иван" },
                new StudentModel { Name = "Петров Петр" },
                new StudentModel { Name = "Сидорова Анна" }
            );

            _context.Courses.AddRange(
                new CourseModel { Name = "Математика" },
                new CourseModel { Name = "Физика" },
                new CourseModel { Name = "Программирование" }
            );

            await _context.SaveChangesAsync();

            var students = await _context.Students.OrderBy(s => s.Id).ToListAsync();
            var courses = await _context.Courses.OrderBy(c => c.Id).ToListAsync();

            var e1 = new EnrollmentModel { StudentId = students[0].Id, CourseId = courses[0].Id };
            var e2 = new EnrollmentModel { StudentId = students[0].Id, CourseId = courses[1].Id };
            var e3 = new EnrollmentModel { StudentId = students[0].Id, CourseId = courses[2].Id };
            var e4 = new EnrollmentModel { StudentId = students[1].Id, CourseId = courses[0].Id };
            var e5 = new EnrollmentModel { StudentId = students[1].Id, CourseId = courses[1].Id };
            var e6 = new EnrollmentModel { StudentId = students[1].Id, CourseId = courses[2].Id };
            var e7 = new EnrollmentModel { StudentId = students[2].Id, CourseId = courses[0].Id };
            var e8 = new EnrollmentModel { StudentId = students[2].Id, CourseId = courses[1].Id };
            var e9 = new EnrollmentModel { StudentId = students[2].Id, CourseId = courses[2].Id };

            _context.Enrollments.AddRange(e1, e2, e3, e4, e5, e6, e7, e8, e9);
            await _context.SaveChangesAsync();

            _context.Grades.AddRange(
                new GradeModel { EnrollmentId = e1.Id, Value = 5, Comment = "Отлично", Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm"), CourseName = courses[0].Name },
                new GradeModel { EnrollmentId = e2.Id, Value = 4, Comment = "Хорошо", Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm"), CourseName = courses[1].Name },
                new GradeModel { EnrollmentId = e3.Id, Value = 5, Comment = "Отлично", Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm"), CourseName = courses[2].Name },

                new GradeModel { EnrollmentId = e4.Id, Value = 3, Comment = "Удовлетворительно", Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm"), CourseName = courses[0].Name },
                new GradeModel { EnrollmentId = e5.Id, Value = 3, Comment = "Удовлетворительно", Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm"), CourseName = courses[1].Name },
                new GradeModel { EnrollmentId = e6.Id, Value = 2, Comment = "Плохо", Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm"), CourseName = courses[2].Name },

                new GradeModel { EnrollmentId = e7.Id, Value = 4, Comment = "Хорошо", Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm"), CourseName = courses[0].Name },
                new GradeModel { EnrollmentId = e8.Id, Value = 5, Comment = "Отлично", Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm"), CourseName = courses[1].Name },
                new GradeModel { EnrollmentId = e9.Id, Value = 4, Comment = "Хорошо", Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm"), CourseName = courses[2].Name }
            );

            await _context.SaveChangesAsync();
        }

        private async Task ReloadStudentsCoursesAsync()
        {
            Students.Clear();
            foreach (var s in await _studentRepository.GetAllAsync())
                Students.Add(s);

            Courses.Clear();
            foreach (var c in await _courseRepository.GetAllAsync())
                Courses.Add(c);

            await UpdateStudentsAverageAndLowGradesAsync();
        }

        private async Task LoadStudentDataAsync()
        {
            if (SelectedStudent == null) return;

            IsLoading = true;
            try
            {
                var enrollments = await _enrollmentRepository.GetByStudentIdAsync(SelectedStudent.Id);
                Enrollments.Clear();
                foreach (var e in enrollments)
                    Enrollments.Add(e);

                if (SelectedCourse != null)
                {
                    SelectedEnrollment = Enrollments.FirstOrDefault(e => e.CourseId == SelectedCourse.Id);
                }

                await LoadStudentProgressAsync(SelectedStudent);
                await LoadGradesForEnrollmentAsync();
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task LoadGradesForEnrollmentAsync()
        {
            Grades.Clear();

            if (SelectedEnrollment == null) return;

            var enrollment = await _enrollmentRepository.GetByIdAsync(SelectedEnrollment.Id);
            if (enrollment == null) return;

            foreach (var g in enrollment.Grades.OrderByDescending(g => g.Id))
                Grades.Add(g);
        }

        public async Task EnrollStudentAsync()
        {
            if (SelectedStudent == null || SelectedCourse == null) return;

            var existing = await _enrollmentRepository.FindAsync(SelectedStudent.Id, SelectedCourse.Id);
            if (existing != null)
            {
                MessageBox.Show("Студент уже записан на этот курс", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                SelectedEnrollment = existing;
                return;
            }

            _context.Enrollments.Add(new EnrollmentModel
            {
                StudentId = SelectedStudent.Id,
                CourseId = SelectedCourse.Id
            });

            await _context.SaveChangesAsync();

            MessageBox.Show("Студент записан на курс", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            await LoadStudentDataAsync();
        }

        public async Task AddGradeAsync()
        {
            if (SelectedEnrollment == null) return;

            var enrollment = await _enrollmentRepository.GetByIdAsync(SelectedEnrollment.Id);
            if (enrollment == null) return;

            var existingGrade = enrollment.Grades.FirstOrDefault();

            if (existingGrade != null)
            {
                existingGrade.Value = NewGradeValue;
                existingGrade.Comment = NewGradeComment;
                existingGrade.Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                existingGrade.CourseName = enrollment.Course?.Name ?? existingGrade.CourseName;
                MessageBox.Show("Оценка обновлена", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                _context.Grades.Add(new GradeModel
                {
                    EnrollmentId = enrollment.Id,
                    CourseName = enrollment.Course?.Name ?? "",
                    Value = NewGradeValue,
                    Comment = NewGradeComment,
                    Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm")
                });
                MessageBox.Show("Оценка добавлена", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            await _context.SaveChangesAsync();

            await UpdateStudentsAverageAndLowGradesAsync();
            if (SelectedStudent != null)
                await LoadStudentProgressAsync(SelectedStudent);
            await LoadGradesForEnrollmentAsync();

            NewGradeValue = 5;
            NewGradeComment = "";
        }

        public async Task DeleteGradeAsync()
        {
            if (SelectedGrade == null) return;

            var result = MessageBox.Show($"Удалить оценку {SelectedGrade.Value}?",
                "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _context.Grades.Remove(SelectedGrade);
                await _context.SaveChangesAsync();

                await UpdateStudentsAverageAndLowGradesAsync();
                await LoadGradesForEnrollmentAsync();

                if (SelectedStudent != null)
                    await LoadStudentProgressAsync(SelectedStudent);

                MessageBox.Show("Оценка удалена", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private async Task UpdateStudentsAverageAndLowGradesAsync()
        {
            var students = await _context.Students.ToListAsync();
            foreach (var student in students)
            {
                var grades = await _context.Grades
                    .Where(g => g.Enrollment!.StudentId == student.Id)
                    .Include(g => g.Enrollment)
                    .ToListAsync();

                if (grades.Any())
                {
                    student.AverageGrade = grades.Average(g => g.Value);
                    student.HasLowGrades = grades.Any(g => g.Value <= 2);
                }
                else
                {
                    student.AverageGrade = 0;
                    student.HasLowGrades = false;
                }
            }

            await _context.SaveChangesAsync();

            if (Students.Count > 0)
            {
                for (int i = 0; i < Students.Count; i++)
                {
                    var updated = students.FirstOrDefault(s => s.Id == Students[i].Id);
                    if (updated != null)
                    {
                        Students[i].AverageGrade = updated.AverageGrade;
                        Students[i].HasLowGrades = updated.HasLowGrades;
                    }
                }
            }
        }

        private async Task LoadStudentProgressAsync(StudentModel student)
        {
            var grades = await _context.Grades
                .Where(g => g.Enrollment!.StudentId == student.Id)
                .Include(g => g.Enrollment)
                .ToListAsync();

            if (grades.Any())
            {
                double avg = grades.Average(g => g.Value);
                ProgressPercent = (avg / 5) * 100;

                if (avg >= 4.5)
                    StudentProgress = $"Отличная успеваемость! Средний балл: {avg:F2}";
                else if (avg >= 3.5)
                    StudentProgress = $"Хорошая успеваемость. Средний балл: {avg:F2}";
                else if (avg >= 2.5)
                    StudentProgress = $"Удовлетворительная успеваемость. Средний балл: {avg:F2}";
                else
                    StudentProgress = $"Низкая успеваемость! Требуется помощь. Средний балл: {avg:F2}";
            }
            else
            {
                ProgressPercent = 0;
                StudentProgress = "Нет оценок";
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object?> _execute;
        private readonly Func<object?, bool>? _canExecute;

        public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object? parameter)
        {
            _execute(parameter);
        }
    }

    public class AsyncRelayCommand : ICommand
    {
        private readonly Func<object?, Task> _executeAsync;
        private readonly Func<object?, bool>? _canExecute;
        private bool _isExecuting;

        public AsyncRelayCommand(Func<object?, Task> executeAsync, Func<object?, bool>? canExecute = null)
        {
            _executeAsync = executeAsync;
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            if (_isExecuting) return false;
            return _canExecute == null || _canExecute(parameter);
        }

        public async void Execute(object? parameter)
        {
            _isExecuting = true;
            try
            {
                await _executeAsync(parameter);
            }
            finally
            {
                _isExecuting = false;
                CommandManager.InvalidateRequerySuggested();
            }
        }
    }
}