using System;
using System.Collections.Generic;

namespace Task;

public class Program
{
    public static void Main()
    {
        StudentGrade student = new StudentGrade();

        List<int> grades = [85, 99, 78, 92];

        try
        {
            double avg = student.CalculateAverage(grades);
            Console.WriteLine($"Средний балл: {avg:F2}");
        }
        catch (InvalidGradeException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}