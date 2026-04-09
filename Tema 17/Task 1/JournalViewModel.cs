using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using JournalApp.Models;

namespace JournalApp.ViewModels
{
    public class JournalViewModel : INotifyPropertyChanged
    {
        private StudentModel? _selectedStudent;
        private CourseModel? _selectedCourse;
        private GradeModel? _selectedGrade;
        private bool _isLoading;
        private string _studentProgress = "";
        private double _progressPercent;
        private int _newGradeValue = 5;
        private string _newGradeComment = "";

        public ObservableCollection<StudentModel> Students { get; set; }
        public ObservableCollection<CourseModel> Courses { get; set; }
        public ObservableCollection<GradeModel> Grades { get; set; }

        public ICommand AddGradeCommand { get; set; }
        public ICommand DeleteGradeCommand { get; set; }

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
                if (value != null)
                {
                    LoadStudentProgress(value);
                    LoadGradesForStudent();
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
                if (value != null && SelectedStudent != null)
                    LoadGradesForStudent();
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

        public bool CanAddGrade => SelectedStudent != null && SelectedCourse != null;
        public bool CanDeleteGrade => SelectedGrade != null;

        public JournalViewModel()
        {
            Students = new ObservableCollection<StudentModel>();
            Courses = new ObservableCollection<CourseModel>();
            Grades = new ObservableCollection<GradeModel>();

            Students.Add(new StudentModel { Id = 1, Name = "Иванов Иван" });
            Students.Add(new StudentModel { Id = 2, Name = "Петров Петр" });
            Students.Add(new StudentModel { Id = 3, Name = "Сидорова Анна" });

            Courses.Add(new CourseModel { Id = 1, Name = "Математика" });
            Courses.Add(new CourseModel { Id = 2, Name = "Физика" });
            Courses.Add(new CourseModel { Id = 3, Name = "Программирование" });

            Grades.Add(new GradeModel { StudentId = 1, CourseId = 1, Value = 5, Comment = "Отлично", Date = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm"), CourseName = "Математика" });
            Grades.Add(new GradeModel { StudentId = 1, CourseId = 2, Value = 4, Comment = "Хорошо", Date = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm"), CourseName = "Физика" });
            Grades.Add(new GradeModel { StudentId = 1, CourseId = 3, Value = 5, Comment = "Отлично", Date = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm"), CourseName = "Программирование" });

            Grades.Add(new GradeModel { StudentId = 2, CourseId = 1, Value = 3, Comment = "Удовлетворительно", Date = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm"), CourseName = "Математика" });
            Grades.Add(new GradeModel { StudentId = 2, CourseId = 2, Value = 3, Comment = "Удовлетворительно", Date = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm"), CourseName = "Физика" });
            Grades.Add(new GradeModel { StudentId = 2, CourseId = 3, Value = 2, Comment = "Плохо", Date = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm"), CourseName = "Программирование" });

            Grades.Add(new GradeModel { StudentId = 3, CourseId = 1, Value = 4, Comment = "Хорошо", Date = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm"), CourseName = "Математика" });
            Grades.Add(new GradeModel { StudentId = 3, CourseId = 2, Value = 5, Comment = "Отлично", Date = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm"), CourseName = "Физика" });
            Grades.Add(new GradeModel { StudentId = 3, CourseId = 3, Value = 4, Comment = "Хорошо", Date = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm"), CourseName = "Программирование" });

            AddGradeCommand = new RelayCommand(_ => AddGrade(), _ => CanAddGrade);
            DeleteGradeCommand = new RelayCommand(_ => DeleteGrade(), _ => CanDeleteGrade);

            UpdateStudentsAverageAndLowGrades();
        }

        public void AddGrade()
        {
            if (SelectedStudent == null || SelectedCourse == null) return;

            var existingGrade = Grades.FirstOrDefault(g =>
                g.StudentId == SelectedStudent.Id && g.CourseId == SelectedCourse.Id);

            if (existingGrade != null)
            {
                existingGrade.Value = NewGradeValue;
                existingGrade.Comment = NewGradeComment;
                existingGrade.Date = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                MessageBox.Show("Оценка обновлена", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                Grades.Add(new GradeModel
                {
                    StudentId = SelectedStudent.Id,
                    CourseId = SelectedCourse.Id,
                    CourseName = SelectedCourse.Name,
                    Value = NewGradeValue,
                    Comment = NewGradeComment,
                    Date = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm")
                });
                MessageBox.Show("Оценка добавлена", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            UpdateStudentsAverageAndLowGrades();
            LoadGradesForStudent();
            LoadStudentProgress(SelectedStudent);

            NewGradeValue = 5;
            NewGradeComment = "";
        }

        public void DeleteGrade()
        {
            if (SelectedGrade == null) return;

            var result = MessageBox.Show($"Удалить оценку {SelectedGrade.Value}?",
                "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Grades.Remove(SelectedGrade);
                UpdateStudentsAverageAndLowGrades();
                LoadGradesForStudent();

                if (SelectedStudent != null)
                    LoadStudentProgress(SelectedStudent);

                MessageBox.Show("Оценка удалена", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void LoadGradesForStudent()
        {
            if (SelectedStudent == null) return;

            var studentGrades = Grades.Where(g => g.StudentId == SelectedStudent.Id).ToList();

            Grades.Clear();
            foreach (var g in studentGrades)
            {
                Grades.Add(g);
            }
        }

        private void UpdateStudentsAverageAndLowGrades()
        {
            foreach (var student in Students)
            {
                var studentGrades = Grades.Where(g => g.StudentId == student.Id).ToList();

                if (studentGrades.Any())
                {
                    student.AverageGrade = studentGrades.Average(g => g.Value);
                    student.HasLowGrades = studentGrades.Any(g => g.Value <= 2);
                }
                else
                {
                    student.AverageGrade = 0;
                    student.HasLowGrades = false;
                }
            }
        }

        private void LoadStudentProgress(StudentModel student)
        {
            var studentGrades = Grades.Where(g => g.StudentId == student.Id).ToList();

            if (studentGrades.Any())
            {
                double avg = studentGrades.Average(g => g.Value);
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
}