using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Task.Models;

namespace Task.Services
{
    public class DataService
    {
        private string _usersPath = "users.json";
        private string _journalPath = "journal.json";

        public List<UserModel> LoadUsers()
        {
            if (!File.Exists(_usersPath))
            {
                var defaultUsers = new List<UserModel>
                {
                    new UserModel { Id = 1, Username = "teacher", Password = "123", Role = "Teacher" },
                    new UserModel { Id = 2, Username = "student1", Password = "123", Role = "Student", StudentId = 1 },
                    new UserModel { Id = 3, Username = "student2", Password = "123", Role = "Student", StudentId = 2 },
                    new UserModel { Id = 4, Username = "student3", Password = "123", Role = "Student", StudentId = 3 }
                };

                SaveUsers(defaultUsers);
                return defaultUsers;
            }

            string json = File.ReadAllText(_usersPath);
            return JsonSerializer.Deserialize<List<UserModel>>(json) ?? new List<UserModel>();
        }

        public void SaveUsers(List<UserModel> users)
        {
            string json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_usersPath, json);
        }

        public JournalData LoadJournal()
        {
            if (!File.Exists(_journalPath))
            {
                var defaultData = new JournalData
                {
                    Students = new List<StudentModel>
                    {
                        new StudentModel { Id = 1, Name = "Иванов Иван" },
                        new StudentModel { Id = 2, Name = "Петров Петр" },
                        new StudentModel { Id = 3, Name = "Сидорова Анна" }
                    },
                    Courses = new List<CourseModel>
                    {
                        new CourseModel { Id = 1, Name = "Математика" },
                        new CourseModel { Id = 2, Name = "Физика" },
                        new CourseModel { Id = 3, Name = "Программирование" }
                    },
                    Grades = new List<GradeModel>(),
                    Notifications = new List<NotificationModel>()
                };

                SaveJournal(defaultData);
                return defaultData;
            }

            string json = File.ReadAllText(_journalPath);
            return JsonSerializer.Deserialize<JournalData>(json) ?? new JournalData();
        }

        public void SaveJournal(JournalData data)
        {
            string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_journalPath, json);
        }
    }

    public class JournalData
    {
        public List<StudentModel> Students { get; set; } = new();
        public List<CourseModel> Courses { get; set; } = new();
        public List<GradeModel> Grades { get; set; } = new();
        public List<NotificationModel> Notifications { get; set; } = new();
    }
}