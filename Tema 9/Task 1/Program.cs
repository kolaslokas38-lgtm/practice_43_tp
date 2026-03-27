    using System;
    using System.IO;

    namespace Task;

    public class Program
    {
        public static void Main()
        {
            string testDir = @"C:\Users\MSI\Documents\Tema 9\Task 1\bin\Debug\net8.0";
            string file1 = Path.Combine(testDir, "shved_ruslan.txt");
            string copyFile = Path.Combine(testDir, "shved_ruslan_copy.txt");
            string movedFile = Path.Combine(testDir, "shved_ruslan_moved.txt");

            Directory.CreateDirectory(testDir);

            FileManager fm = new FileManager();
            FileInfoProvider info = new FileInfoProvider();

            fm.CreateFile(file1, "Hello World!");
            string content = File.ReadAllText(file1);
            Console.WriteLine($"Прочитано: {content}\n");

            Console.WriteLine($"Файл существует: {File.Exists(file1)}\n");

            info.GetInfo(file1);
            Console.WriteLine();

            fm.CopyFile(file1, copyFile);
            Console.WriteLine($"Копия существует: {File.Exists(copyFile)}\n");

            fm.MoveFile(copyFile, movedFile);
            Console.WriteLine();

            fm.RenameFile(movedFile, "shved_ruslan.io");
            string renamed = Path.Combine(testDir, "shved_ruslan.io");
            Console.WriteLine();

            fm.DeleteFile(Path.Combine(testDir, "nonexistent.txt"));
            Console.WriteLine();

            info.CompareSize(file1, renamed);
            Console.WriteLine();

            fm.CreateFile(Path.Combine(testDir, "shved_ruslan.ii"), "test");
            fm.CreateFile(Path.Combine(testDir, "data.ii"), "data");
            fm.CreateFile(Path.Combine(testDir, "keep.txt"), "keep");
            fm.DeleteFilesByPattern(testDir, "*.ii");
            Console.WriteLine();

            var files = fm.GetFiles(testDir);
            Console.WriteLine("Файлы в директории:");

            foreach (string f in files)
            {
                Console.WriteLine($"  {Path.GetFileName(f)}");
            }

            Console.WriteLine();

            string readOnlyFile = Path.Combine(testDir, "shved_ruslan_readonly.txt");
            fm.CreateFile(readOnlyFile, "original");
            info.SetReadOnly(readOnlyFile, true);
            info.TryWriteToReadOnly(readOnlyFile, "new content");
            Console.WriteLine();

            info.CheckPermissions(readOnlyFile);

            Console.WriteLine("\nОчистка...");
            fm.DeleteFile(file1);
            fm.DeleteFile(renamed);
            fm.DeleteFile(readOnlyFile);
            fm.DeleteFile(Path.Combine(testDir, "keep.txt"));
        }
    }