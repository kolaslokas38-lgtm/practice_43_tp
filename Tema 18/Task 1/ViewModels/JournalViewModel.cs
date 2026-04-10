using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Task_1.Models;
using Task_1.Repositories;

namespace Task_1.ViewModels
{
    public class JournalViewModel : INotifyPropertyChanged
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IEnrollmentRepository _enrollmentRepository;

        private Student? _selectedStudent;
        private Enrollment? _selectedEnrollment;
        private bool _isLoading;
        private string _selectedCourse = "";
        private int _newGrade = 5;
        private string _newCourse = "";

        public ObservableCollection<Student> Students { get; set; }
        public ObservableCollection<Enrollment> Enrollments { get; set; }
        public ObservableCollection<string> Courses { get; set; }

        public ICommand LoadDataCommand { get; set; }
        public ICommand EnrollStudentCommand { get; set; }
        public ICommand AddGradeCommand { get; set; }
        public ICommand DeleteEnrollmentCommand { get; set; }
        public ICommand RefreshCommand { get; set; }

        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; OnPropertyChanged(); }
        }

        public Student? SelectedStudent
        {
            get => _selectedStudent;
            set
            {
                _selectedStudent = value;
                OnPropertyChanged();
                if (value != null)
                {
                    _ = LoadEnrollmentsForStudent(value.Id);
                }
            }
        }

        public Enrollment? SelectedEnrollment
        {
            get => _selectedEnrollment;
            set
            {
                _selectedEnrollment = value;
                OnPropertyChanged();
                if (value != null)
                {
                    NewGrade = value.Grade;
                    NewCourse = value.Course;
                }
            }
        }

        public string SelectedCourse
        {
            get => _selectedCourse;
            set { _selectedCourse = value; OnPropertyChanged(); }
        }

        public string NewCourse
        {
            get => _newCourse;
            set { _newCourse = value; OnPropertyChanged(); }
        }

        public int NewGrade
        {
            get => _newGrade;
            set { _newGrade = value; OnPropertyChanged(); }
        }

        public JournalViewModel(IStudentRepository studentRepository, IEnrollmentRepository enrollmentRepository)
        {
            _studentRepository = studentRepository;
            _enrollmentRepository = enrollmentRepository;

            Students = new ObservableCollection<Student>();
            Enrollments = new ObservableCollection<Enrollment>();
            Courses = new ObservableCollection<string>
            {
                "Математика",
                "Физика",
                "Программирование",
                "Химия",
                "Биология"
            };

            LoadDataCommand = new RelayCommand(async _ => await LoadDataAsync(), _ => true);
            EnrollStudentCommand = new RelayCommand(async _ => await EnrollStudentAsync(), _ => CanEnrollStudent());
            AddGradeCommand = new RelayCommand(async _ => await AddGradeAsync(), _ => CanAddGrade());
            DeleteEnrollmentCommand = new RelayCommand(async _ => await DeleteEnrollmentAsync(), _ => CanDeleteEnrollment());
            RefreshCommand = new RelayCommand(async _ => await LoadDataAsync(), _ => true);

            _ = LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            IsLoading = true;

            try
            {
                var students = await _studentRepository.GetAllAsync();

                Students.Clear();
                foreach (var student in students)
                {
                    Students.Add(student);
                }

                if (Students.Count > 0 && SelectedStudent == null)
                {
                    SelectedStudent = Students[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task LoadEnrollmentsForStudent(int studentId)
        {
            var enrollments = await _enrollmentRepository.GetByStudentIdAsync(studentId);

            Enrollments.Clear();
            foreach (var enrollment in enrollments)
            {
                Enrollments.Add(enrollment);
            }
        }

        private bool CanEnrollStudent()
        {
            return SelectedStudent != null && !string.IsNullOrEmpty(SelectedCourse);
        }

        private async Task EnrollStudentAsync()
        {
            if (SelectedStudent == null) return;

            var enrollment = new Enrollment
            {
                StudentId = SelectedStudent.Id,
                Course = SelectedCourse,
                Grade = 0
            };

            await _enrollmentRepository.AddAsync(enrollment);
            await _enrollmentRepository.SaveChangesAsync();

            await LoadEnrollmentsForStudent(SelectedStudent.Id);

            MessageBox.Show("Студент зачислен на курс", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private bool CanAddGrade()
        {
            return SelectedEnrollment != null && NewGrade >= 2 && NewGrade <= 5;
        }

        private async Task AddGradeAsync()
        {
            if (SelectedEnrollment == null)
            {
                MessageBox.Show("Выберите оценку для редактирования", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Меняем оценку
            SelectedEnrollment.Grade = NewGrade;

            // Обновляем в базе
            await _enrollmentRepository.UpdateAsync(SelectedEnrollment);
            await _enrollmentRepository.SaveChangesAsync();

            // Обновляем список
            if (SelectedStudent != null)
            {
                await LoadEnrollmentsForStudent(SelectedStudent.Id);
            }

            MessageBox.Show($"Оценка изменена на {NewGrade}", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private bool CanDeleteEnrollment()
        {
            return SelectedEnrollment != null;
        }

        private async Task DeleteEnrollmentAsync()
        {
            if (SelectedEnrollment == null) return;

            var result = MessageBox.Show($"Удалить запись о курсе {SelectedEnrollment.Course}?",
                "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                await _enrollmentRepository.DeleteAsync(SelectedEnrollment.Id);
                await _enrollmentRepository.SaveChangesAsync();

                if (SelectedStudent != null)
                {
                    await LoadEnrollmentsForStudent(SelectedStudent.Id);
                }

                MessageBox.Show("Запись удалена", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
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
        private readonly Func<object?, Task> _executeAsync;
        private readonly Func<object?, bool>? _canExecute;

        public RelayCommand(Func<object?, Task> executeAsync, Func<object?, bool>? canExecute = null)
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
            return _canExecute == null || _canExecute(parameter);
        }

        public async void Execute(object? parameter)
        {
            await _executeAsync(parameter);
        }
    }
}