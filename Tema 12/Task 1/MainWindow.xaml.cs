using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Task;

public partial class MainWindow : Window
{
    private ObservableCollection<Subject> subjects;
    private Subject currentSubject;

    public MainWindow()
    {
        InitializeComponent();
        InitializeData();
    }

    private void InitializeData()
    {
        subjects = new ObservableCollection<Subject>();

        Subject math = new Subject("Математика");
        math.Students.Add(new Student("Швед Руслан", "5", "Отлично"));
        math.Students.Add(new Student("Петров Роман", "4", "Хорошо"));
        math.Students.Add(new Student("Мирослав Седеневский", "3", "Удовлетворительно"));

        Subject physics = new Subject("Физика");
        physics.Students.Add(new Student("Швед Руслан", "4", "Хорошо"));
        physics.Students.Add(new Student("Петров Роман", "5", "Отлично"));
        physics.Students.Add(new Student("Мирослав Седеневский", "4", "Хорошо"));

        Subject programming = new Subject("Программирование");
        programming.Students.Add(new Student("Швед Руслан", "5", "Отлично"));
        programming.Students.Add(new Student("Петров Роман", "4", "Хорошо"));
        programming.Students.Add(new Student("Мирослав Седеневский", "5", "Отлично"));

        subjects.Add(math);
        subjects.Add(physics);
        subjects.Add(programming);

        cmbSubject.ItemsSource = subjects;
        cmbSubject.DisplayMemberPath = "Name";
        cmbSubject.SelectedIndex = 0;
    }

    private void CmbSubject_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        if (cmbSubject.SelectedItem != null)
        {
            currentSubject = (Subject)cmbSubject.SelectedItem;
            dgStudents.ItemsSource = currentSubject.Students;
        }
    }

    private void BtnAddGrade_Click(object sender, RoutedEventArgs e)
    {
        if (currentSubject == null)
        {
            MessageBox.Show("Выберите предмет", "Ошибка",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        AddGradeWindow addWindow = new AddGradeWindow(currentSubject.Students);
        addWindow.Owner = this;

        if (addWindow.ShowDialog() == true)
        {
            dgStudents.Items.Refresh();
            MessageBox.Show("Оценка добавлена", "Успех",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}