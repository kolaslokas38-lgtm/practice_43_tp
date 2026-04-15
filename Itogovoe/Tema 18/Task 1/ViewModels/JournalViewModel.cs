using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using Task_1.Data;
using Task_1.Models;
using Task_1.Repositories;

namespace Task_1.ViewModels
{
    public class JournalViewModel : INotifyPropertyChanged
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly ApplicationDbContext _context;
        private readonly AppUser _currentUser;
        private Student? _selectedStudent;
        private string _newMessageText = string.Empty;
        private double _averageGrade;
        private int _totalCourses;
        private int _bestGrade;
        private int _worstGrade;

        public ObservableCollection<Student> Students { get; } = [];
        public ObservableCollection<Enrollment> Enrollments { get; } = [];
        public ObservableCollection<ChatMessageItem> Messages { get; } = [];

        public string CurrentUsername => _currentUser.Username;
        public bool IsTeacher => _currentUser.Role == UserRole.Teacher;
        public bool IsStudent => !IsTeacher;
        public bool CanSendMessage => !string.IsNullOrWhiteSpace(NewMessageText);
        public string AverageGradeDisplay => AverageGrade > 0 ? AverageGrade.ToString("0.00") : "Нет оценок";
        public string ProgressStatus
        {
            get
            {
                if (AverageGrade <= 0)
                {
                    return "Недостаточно данных";
                }

                if (AverageGrade >= 4.5)
                {
                    return "Отличный прогресс";
                }

                if (AverageGrade >= 3.5)
                {
                    return "Хороший прогресс";
                }

                return "Требуется улучшение";
            }
        }

        public Student? SelectedStudent
        {
            get => _selectedStudent;
            set
            {
                _selectedStudent = value;
                OnPropertyChanged();
                _ = LoadEnrollmentsAsync();
                _ = LoadMessagesAsync();
            }
        }

        public string NewMessageText
        {
            get => _newMessageText;
            set
            {
                _newMessageText = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanSendMessage));
            }
        }

        public double AverageGrade
        {
            get => _averageGrade;
            private set
            {
                _averageGrade = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(AverageGradeDisplay));
                OnPropertyChanged(nameof(ProgressStatus));
            }
        }

        public int TotalCourses
        {
            get => _totalCourses;
            private set
            {
                _totalCourses = value;
                OnPropertyChanged();
            }
        }

        public int BestGrade
        {
            get => _bestGrade;
            private set
            {
                _bestGrade = value;
                OnPropertyChanged();
            }
        }

        public int WorstGrade
        {
            get => _worstGrade;
            private set
            {
                _worstGrade = value;
                OnPropertyChanged();
            }
        }

        public JournalViewModel(
            AppUser currentUser,
            ApplicationDbContext context,
            IStudentRepository studentRepository,
            IEnrollmentRepository enrollmentRepository)
        {
            _currentUser = currentUser;
            _context = context;
            _studentRepository = studentRepository;
            _enrollmentRepository = enrollmentRepository;
            _ = InitializeAsync();
        }

        public async Task RefreshAsync()
        {
            await InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            Students.Clear();

            if (IsTeacher)
            {
                var allStudents = await _studentRepository.GetAllAsync();
                foreach (var student in allStudents.OrderBy(s => s.Name))
                {
                    Students.Add(student);
                }

                SelectedStudent ??= Students.FirstOrDefault();
            }
            else if (_currentUser.StudentId.HasValue)
            {
                var student = await _studentRepository.GetByIdAsync(_currentUser.StudentId.Value);
                if (student != null)
                {
                    Students.Add(student);
                    SelectedStudent = student;
                }
            }

            await LoadEnrollmentsAsync();
            await LoadMessagesAsync();
        }

        private async Task LoadEnrollmentsAsync()
        {
            Enrollments.Clear();
            if (SelectedStudent == null)
            {
                return;
            }

            var items = await _enrollmentRepository.GetByStudentIdAsync(SelectedStudent.Id);
            foreach (var enrollment in items)
            {
                Enrollments.Add(enrollment);
            }

            RecalculateProgress();
        }

        private void RecalculateProgress()
        {
            var grades = Enrollments.Select(e => e.Grade).ToList();
            if (grades.Count == 0)
            {
                AverageGrade = 0;
                TotalCourses = 0;
                BestGrade = 0;
                WorstGrade = 0;
                return;
            }

            AverageGrade = grades.Average();
            TotalCourses = grades.Count;
            BestGrade = grades.Max();
            WorstGrade = grades.Min();
        }

        private async Task LoadMessagesAsync()
        {
            Messages.Clear();
            var chatPartner = await ResolveChatPartnerUserAsync();
            if (chatPartner == null)
            {
                return;
            }

            var conversation = await _context.ChatMessages
                .Where(m =>
                    (m.SenderId == _currentUser.Id && m.ReceiverId == chatPartner.Id) ||
                    (m.SenderId == chatPartner.Id && m.ReceiverId == _currentUser.Id))
                .OrderBy(m => m.SentAt)
                .ToListAsync();

            foreach (var msg in conversation)
            {
                Messages.Add(new ChatMessageItem
                {
                    Text = msg.Text,
                    SentAt = msg.SentAt.ToLocalTime(),
                    IsMine = msg.SenderId == _currentUser.Id,
                    SenderName = msg.SenderId == _currentUser.Id ? "Вы" : chatPartner.Username
                });
            }
        }

        public async Task<(bool Success, string Error)> SendMessageAsync()
        {
            var chatPartner = await ResolveChatPartnerUserAsync();
            if (chatPartner == null)
            {
                return (false, "Выберите собеседника в чате.");
            }

            if (string.IsNullOrWhiteSpace(NewMessageText))
            {
                return (false, "Введите текст сообщения.");
            }

            var message = new ChatMessage
            {
                SenderId = _currentUser.Id,
                ReceiverId = chatPartner.Id,
                Text = NewMessageText.Trim(),
                SentAt = DateTime.UtcNow
            };

            await _context.ChatMessages.AddAsync(message);
            await _context.SaveChangesAsync();

            NewMessageText = string.Empty;
            await LoadMessagesAsync();
            return (true, string.Empty);
        }

        private async Task<AppUser?> ResolveChatPartnerUserAsync()
        {
            if (IsTeacher)
            {
                if (SelectedStudent == null)
                {
                    return null;
                }

                return await _context.Users.FirstOrDefaultAsync(u => u.StudentId == SelectedStudent.Id);
            }

            return await _context.Users
                .Where(u => u.Role == UserRole.Teacher)
                .OrderBy(u => u.Username)
                .FirstOrDefaultAsync();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public class ChatMessageItem
    {
        public string SenderName { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public DateTime SentAt { get; set; }
        public bool IsMine { get; set; }
        public string Display => $"[{SentAt:dd.MM HH:mm}] {SenderName}: {Text}";
    }
}