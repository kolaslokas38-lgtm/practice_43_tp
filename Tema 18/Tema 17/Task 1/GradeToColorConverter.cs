using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace JournalApp
{
    public class GradeToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int grade)
            {
                if (grade == 5)
                    return new SolidColorBrush(Colors.LightGreen);
                if (grade == 4)
                    return new SolidColorBrush(Colors.LightBlue);
                if (grade == 3)
                    return new SolidColorBrush(Colors.LightYellow);
                if (grade <= 2)
                    return new SolidColorBrush(Colors.LightCoral);
            }
            return new SolidColorBrush(Colors.White);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}