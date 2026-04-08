using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Task.Commands;
using Task.Models;
using Task.Services;

namespace Task.ViewModels
{
    public class JournalViewModel : INotifyPropertyChanged
    {
        private DataService _dataService;
        private AuthService _authService;
        private NotificationService _notificationService;
        private ChatService _chatService;
        private JournalData? _journalData;

        private bool _isLoading;
        private StudentModel? _selectedStudent;
        private CourseModel? _selectedCourse;
        private GradeModel? _selectedGrade;
        private int _newGradeValue = 5;
        private string _newGradeComment = "";
        private string _chatMessage = "";

        public ObservableCollection<StudentModel> Students { get; set; }
        public ObservableCollection<CourseModel> Courses { get; set; }
        public ObservableCollection<GradeModel> Grades { get; set; }
        public ObservableCollection<NotificationModel> Notifications { get; set; }
        public ObservableCollection<string> ChatMessages { get; set; }

        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; OnPropertyChanged(); }
        }

        public StudentModel? SelectedStudent
        {
            get => _selectedStudent;
            set { _selectedStudent = value; OnPropertyChanged(); LoadGradesForStudent(); }
        }

        public CourseModel? SelectedCourse
        {
            get => _selectedCourse;
            set { _selectedCourse = value; OnPropertyChanged(); }
        }

        public GradeModel? SelectedGrade
        {
            get => _selectedGrade;
            set { _selectedGrade = value; OnPropertyChanged(); }
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

        public string ChatMessage
        {
            get => _chatMessage;
            set { _chatMessage = value; OnPropertyChanged(); }
        }

        public bool IsTeacher => _authService.IsTeacher;

        public ICommand LoadDataCommand { get; }
        public ICommand AddGradeCommand { get; }
        public ICommand EditGradeCommand { get; }
        public ICommand SendNotificationCommand { get; }
        public ICommand SendChatMessageCommand { get; }
        public ICommand StartChatCommand { get; }

        public JournalViewModel(AuthService authService)
        {
            _authService = authService;
            _dataService = new DataService();
            _notificationService = new NotificationService();
            _chatService = new ChatService();

            Students = new ObservableCollection<StudentModel>();
            Courses = new ObservableCollection<CourseModel>();
            Grades = new ObservableCollection<GradeModel>();
            Notifications = new ObservableCollection<NotificationModel>();
            ChatMessages = _chatService.Messages;

            LoadDataCommand = new RelayCommand(_ => LoadData());
            AddGradeCommand = new RelayCommand(_ => AddGrade(), _ => IsTeacher && SelectedStudent != null && SelectedCourse != null);
            EditGradeCommand = new RelayCommand(_ => EditGrade(), _ => IsTeacher && SelectedGrade != null);
            SendNotificationCommand = new RelayCommand(_ => SendNotification(), _ => IsTeacher);
            SendChatMessageCommand = new RelayCommand(_ => SendChatMessage(), _ => !string.IsNullOrEmpty(ChatMessage));
            StartChatCommand = new RelayCommand(_ => StartChat());

            LoadData();
            StartChat();
            StartNotificationListener();
        }

        private void LoadData()
        {
            _journalData = _dataService.LoadJournal();

            if (_journalData == null) return;

            Students.Clear();
            Courses.Clear();

            foreach (var s in _journalData.Students)
                Students.Add(s);

            foreach (var c in _journalData.Courses)
                Courses.Add(c);

            if (!IsTeacher && _authService.StudentId.HasValue)
            {
                var studentGrades = _journalData.Grades.Where(g => g.StudentId == _authService.StudentId.Value).ToList();

                Grades.Clear();

                foreach (var g in studentGrades)
                    Grades.Add(g);
            }

            Notifications.Clear();

            foreach (var n in _journalData.Notifications)
            {
                if (n.ToRole == "All" || n.ToRole == (IsTeacher ? "Teacher" : "Student"))
                {
                    Notifications.Add(n);
                }
            }
        }

        private void LoadGradesForStudent()
        {
            if (SelectedStudent == null || !IsTeacher || _journalData == null) return;

            var studentGrades = _journalData.Grades.Where(g => g.StudentId == SelectedStudent.Id).ToList();

            Grades.Clear();

            foreach (var g in studentGrades)
                Grades.Add(g);
        }

        private void AddGrade()
        {
            if (SelectedStudent == null || SelectedCourse == null || _journalData == null) return;

            var existingGrade = _journalData.Grades.FirstOrDefault(g =>
                g.StudentId == SelectedStudent.Id && g.CourseId == SelectedCourse.Id);

            if (existingGrade != null)
            {
                existingGrade.Value = NewGradeValue;
                existingGrade.Comment = NewGradeComment;
                existingGrade.Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            }
            else
            {
                _journalData.Grades.Add(new GradeModel
                {
                    StudentId = SelectedStudent.Id,
                    CourseId = SelectedCourse.Id,
                    Value = NewGradeValue,
                    Comment = NewGradeComment,
                    Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm")
                });
            }

            _dataService.SaveJournal(_journalData);
            LoadGradesForStudent();

            NewGradeValue = 5;
            NewGradeComment = "";

            MessageBox.Show("Оценка сохранена", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void EditGrade()
        {
            if (SelectedGrade == null || _journalData == null) return;

            SelectedGrade.Value = NewGradeValue;
            SelectedGrade.Comment = NewGradeComment;
            SelectedGrade.Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

            _dataService.SaveJournal(_journalData);
            LoadGradesForStudent();

            MessageBox.Show("Оценка обновлена", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SendNotification()
        {
            if (_journalData == null) return;

            var notification = new NotificationModel
            {
                Id = Notifications.Count + 1,
                Title = "Новое домашнее задание",
                Message = $"Задание: {NewGradeComment}",
                Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm"),
                FromUser = _authService.CurrentUser?.Username ?? "Teacher",
                ToRole = "All"
            };

            _journalData.Notifications.Add(notification);
            _dataService.SaveJournal(_journalData);

            _notificationService.SendNotification(notification);

            Notifications.Add(notification);

            MessageBox.Show("Уведомление отправлено", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void StartChat()
        {
            _chatService.StartServer(_authService.CurrentUser?.Username ?? "User");
        }

        private void SendChatMessage()
        {
            _chatService.SendMessage(_authService.CurrentUser?.Username ?? "User", ChatMessage);
            ChatMessage = "";
        }

        private void StartNotificationListener()
        {
            _notificationService.StartListening(notification =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Notifications.Insert(0, notification);

                    MessageBox.Show($"{notification.Title}\n{notification.Message}",
                        "Новое уведомление",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                });
            });
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}