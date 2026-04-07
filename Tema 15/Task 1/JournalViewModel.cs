using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using JournalApp.Models;
using JournalApp.Services;

namespace JournalApp.ViewModels
{
    public class JournalViewModel : INotifyPropertyChanged
    {
        private readonly JournalService _journalService;

        private ObservableCollection<StudentModel> _students = new();
        private ObservableCollection<CourseModel> _courses = new();
        private ObservableCollection<GradeModel> _grades = new();

        private bool _isLoading;
        private int _loadingProgress;
        private StudentModel? _selectedStudent;
        private CourseModel? _selectedCourse;
        private int _newGradeValue;

        public JournalViewModel()
        {
            _journalService = new JournalService();

            LoadDataCommand = new RelayCommand(async _ => await LoadDataAsync());
            SaveGradeCommand = new RelayCommand(async _ => await SaveGradeAsync(), _ => CanSaveGrade);
            EditGradeCommand = new RelayCommand(grade => EditGrade(grade));

            _ = LoadDataAsync();
        }

        public ObservableCollection<StudentModel> Students
        {
            get => _students;
            set { _students = value; OnPropertyChanged(); }
        }

        public ObservableCollection<CourseModel> Courses
        {
            get => _courses;
            set { _courses = value; OnPropertyChanged(); }
        }

        public ObservableCollection<GradeModel> Grades
        {
            get => _grades;
            set { _grades = value; OnPropertyChanged(); }
        }

        public StudentModel? SelectedStudent
        {
            get => _selectedStudent;
            set
            {
                _selectedStudent = value;
                OnPropertyChanged();
                LoadStudentGrades();
                ((RelayCommand)SaveGradeCommand).RaiseCanExecuteChanged();
            }
        }

        public CourseModel? SelectedCourse
        {
            get => _selectedCourse;
            set
            {
                _selectedCourse = value;
                OnPropertyChanged();
                ((RelayCommand)SaveGradeCommand).RaiseCanExecuteChanged();
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; OnPropertyChanged(); }
        }

        public int LoadingProgress
        {
            get => _loadingProgress;
            set { _loadingProgress = value; OnPropertyChanged(); }
        }

        public int NewGradeValue
        {
            get => _newGradeValue;
            set
            {
                _newGradeValue = value;
                OnPropertyChanged();
                ((RelayCommand)SaveGradeCommand).RaiseCanExecuteChanged();
            }
        }

        public double SelectedStudentAverageGrade
        {
            get
            {
                if (SelectedStudent == null) return 0;
                return _journalService.GetAverageGradeForStudent(SelectedStudent.Id);
            }
        }

        public ICommand LoadDataCommand { get; }
        public ICommand SaveGradeCommand { get; }
        public ICommand EditGradeCommand { get; }

        private bool CanSaveGrade => SelectedStudent != null &&
                                      SelectedCourse != null &&
                                      NewGradeValue >= 1 &&
                                      NewGradeValue <= 5;

        private async Task LoadDataAsync()
        {
            try
            {
                IsLoading = true;
                LoadingProgress = 0;

                LoadingProgress = 20;
                var students = await _journalService.LoadStudentsAsync();

                Students.Clear();
                foreach (var student in students)
                    Students.Add(student);

                LoadingProgress = 50;
                var courses = await _journalService.LoadCoursesAsync();

                Courses.Clear();
                foreach (var course in courses)
                    Courses.Add(course);

                LoadingProgress = 80;
                var grades = await _journalService.LoadGradesAsync();

                Grades.Clear();
                foreach (var grade in grades)
                    Grades.Add(grade);

                LoadingProgress = 100;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Ошибка загрузки: {ex.Message}", "Ошибка",
                    System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            finally
            {
                await Task.Delay(500);
                IsLoading = false;
                LoadingProgress = 0;
            }
        }

        private async Task SaveGradeAsync()
        {
            if (!CanSaveGrade) return;

            try
            {
                IsLoading = true;

                var newGrade = new GradeModel
                {
                    StudentId = SelectedStudent!.Id,
                    CourseId = SelectedCourse!.Id,
                    Value = NewGradeValue,
                    Date = DateTime.Now
                };

                await _journalService.SaveGradeAsync(newGrade);

                var existingGrade = Grades.FirstOrDefault(g =>
                    g.StudentId == SelectedStudent.Id && g.CourseId == SelectedCourse.Id);

                if (existingGrade != null)
                {
                    existingGrade.Value = NewGradeValue;
                    existingGrade.Date = DateTime.Now;
                }
                else
                {
                    Grades.Add(newGrade);
                }

                NewGradeValue = 0;
                OnPropertyChanged(nameof(SelectedStudentAverageGrade));

                System.Windows.MessageBox.Show("Оценка сохранена!", "Успех",
                    System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка",
                    System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void EditGrade(object? parameter)
        {
            if (parameter is GradeModel grade)
            {
                SelectedStudent = Students.FirstOrDefault(s => s.Id == grade.StudentId);
                SelectedCourse = Courses.FirstOrDefault(c => c.Id == grade.CourseId);
                NewGradeValue = grade.Value;
            }
        }

        private void LoadStudentGrades()
        {
            if (SelectedStudent != null)
            {
                OnPropertyChanged(nameof(SelectedStudentAverageGrade));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}