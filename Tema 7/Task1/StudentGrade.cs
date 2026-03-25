using System;
using System.Collections.Generic;

namespace Task;

public class StudentGrade
{
    public double CalculateAverage(List<int> grades)
    {
        if (grades.Count == 0)
        {
            throw new InvalidGradeException("Список оценок пуст");
        }

        int sum = 0;

        for (int i = 0; i < grades.Count; i++)
        {
            if (grades[i] < 0 || grades[i] > 100)
            {
                throw new InvalidGradeException($"Оценка {grades[i]} вне диапазона 0-100");
            }

            sum += grades[i];
        }

        return (double)sum / grades.Count;
    }
}