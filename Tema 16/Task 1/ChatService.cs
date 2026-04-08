using System;
using System.IO.Pipes;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;

namespace Task.Services
{
    public class ChatService
    {
        public ObservableCollection<string> Messages { get; set; }

        public ChatService()
        {
            Messages = new ObservableCollection<string>();
        }

        public void StartServer(string userName)
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                try
                {
                    using var pipeServer = new NamedPipeServerStream("JournalChat", PipeDirection.In);
                    pipeServer.WaitForConnection();

                    byte[] buffer = new byte[1024];

                    while (pipeServer.IsConnected)
                    {
                        int bytesRead = pipeServer.Read(buffer, 0, buffer.Length);

                        if (bytesRead > 0)
                        {
                            string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                Messages.Add(message);
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Messages.Add($"Ошибка чата: {ex.Message}");
                    });
                }
            });
        }

        public void SendMessage(string userName, string message)
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                try
                {
                    using var client = new NamedPipeClientStream(".", "JournalChat", PipeDirection.Out);
                    client.Connect(1000);

                    string fullMessage = $"{userName}: {message}";
                    byte[] bytes = Encoding.UTF8.GetBytes(fullMessage);
                    client.Write(bytes, 0, bytes.Length);
                    client.Flush();
                }
                catch
                {
                }
            });
        }
    }
}