using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Task_1.Data;
using Task_1.Models;

namespace Task_1.Services
{
    public class AuthService
    {
        private readonly ApplicationDbContext _context;

        public AuthService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AppUser?> LoginAsync(string username, string password)
        {
            var user = await _context.Users.Include(u => u.Student).FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
                return null;
            }

            return VerifyPassword(password, user.PasswordHash, user.PasswordSalt) ? user : null;
        }

        public async Task<(bool Success, string Error)> RegisterAsync(string username, string password, UserRole role)
        {
            if (await _context.Users.AnyAsync(u => u.Username == username))
            {
                return (false, "Пользователь с таким логином уже существует.");
            }

            CreatePasswordHash(password, out var hash, out var salt);

            Student? student = null;
            if (role == UserRole.Student)
            {
                student = new Student { Name = username };
                await _context.Students.AddAsync(student);
                await _context.SaveChangesAsync();
            }

            await _context.Users.AddAsync(new AppUser
            {
                Username = username,
                PasswordHash = hash,
                PasswordSalt = salt,
                Role = role,
                StudentId = student?.Id
            });

            await _context.SaveChangesAsync();
            return (true, string.Empty);
        }

        public async Task EnsureStudentUsersForExistingStudentsAsync()
        {
            var students = await _context.Students.AsNoTracking().ToListAsync();
            foreach (var student in students)
            {
                var hasAccount = await _context.Users.AnyAsync(u => u.StudentId == student.Id);
                if (hasAccount)
                {
                    continue;
                }

                var baseUsername = $"student{student.Id}";
                var username = baseUsername;
                var suffix = 1;
                while (await _context.Users.AnyAsync(u => u.Username == username))
                {
                    username = $"{baseUsername}_{suffix}";
                    suffix++;
                }

                CreatePasswordHash("student123", out var hash, out var salt);
                await _context.Users.AddAsync(new AppUser
                {
                    Username = username,
                    PasswordHash = hash,
                    PasswordSalt = salt,
                    Role = UserRole.Student,
                    StudentId = student.Id
                });
            }

            await _context.SaveChangesAsync();
        }

        private static void CreatePasswordHash(string password, out byte[] hash, out byte[] salt)
        {
            salt = RandomNumberGenerator.GetBytes(16);
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256);
            hash = pbkdf2.GetBytes(32);
        }

        private static bool VerifyPassword(string password, byte[] hash, byte[] salt)
        {
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256);
            var candidate = pbkdf2.GetBytes(32);
            return CryptographicOperations.FixedTimeEquals(candidate, hash);
        }
    }
}

