using System;
using System.IO.MemoryMappedFiles;
using System.Text;
using System.Threading.Tasks;
using Task.Models;

namespace Task.Services
{
    public class NotificationService : IDisposable
    {
        private const string MapName = "JournalNotifications";
        private MemoryMappedFile? _mmf;
        private MemoryMappedViewAccessor? _accessor;
        private bool _disposed;

        public NotificationService()
        {
            try
            {
                _mmf = MemoryMappedFile.CreateOrOpen(MapName, 4096);
                _accessor = _mmf.CreateViewAccessor();
            }
            catch { }
        }

        public void SendNotification(NotificationModel notification)
        {
            if (_accessor == null) return;

            string message = $"{notification.Title}|{notification.Message}|{notification.Date}|{notification.FromUser}|{notification.ToRole}";

            byte[] bytes = Encoding.UTF8.GetBytes(message);
            byte[] length = BitConverter.GetBytes(bytes.Length);

            _accessor.WriteArray(0, length, 0, length.Length);
            _accessor.WriteArray(4, bytes, 0, bytes.Length);
        }

        public void StartListening(Action<NotificationModel> onNotification)
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                if (_accessor == null) return;

                while (true)
                {
                    byte[] lengthBytes = new byte[4];
                    _accessor.ReadArray(0, lengthBytes, 0, 4);
                    int length = BitConverter.ToInt32(lengthBytes, 0);

                    if (length > 0)
                    {
                        byte[] messageBytes = new byte[length];
                        _accessor.ReadArray(4, messageBytes, 0, length);
                        string message = Encoding.UTF8.GetString(messageBytes);

                        string[] parts = message.Split('|');

                        if (parts.Length == 5)
                        {
                            var notification = new NotificationModel
                            {
                                Title = parts[0],
                                Message = parts[1],
                                Date = parts[2],
                                FromUser = parts[3],
                                ToRole = parts[4]
                            };

                            onNotification?.Invoke(notification);
                        }
                    }

                    System.Threading.Tasks.Task.Delay(100).Wait();
                }
            });
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _accessor?.Dispose();
                _mmf?.Dispose();
                _disposed = true;
            }
        }
    }
}