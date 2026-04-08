using System.Linq;
using Task.Models;

namespace Task.Services
{
    public class AuthService
    {
        private DataService _dataService;
        private UserModel? _currentUser;

        public AuthService()
        {
            _dataService = new DataService();
        }

        public UserModel? CurrentUser => _currentUser;
        public bool IsAuthenticated => _currentUser != null;
        public bool IsTeacher => _currentUser?.Role == "Teacher";
        public int? StudentId => _currentUser?.StudentId;

        public bool Login(string username, string password)
        {
            var users = _dataService.LoadUsers();
            _currentUser = users.FirstOrDefault(u => u.Username == username && u.Password == password);
            return _currentUser != null;
        }

        public void Logout()
        {
            _currentUser = null;
        }

        public bool Register(string username, string password, string role, int? studentId = null)
        {
            var users = _dataService.LoadUsers();

            if (users.Any(u => u.Username == username))
            {
                return false;
            }

            int newId = users.Count > 0 ? users.Max(u => u.Id) + 1 : 1;

            var newUser = new UserModel
            {
                Id = newId,
                Username = username,
                Password = password,
                Role = role,
                StudentId = studentId
            };

            users.Add(newUser);
            _dataService.SaveUsers(users);

            return true;
        }
    }
}