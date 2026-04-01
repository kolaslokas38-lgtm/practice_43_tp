using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Task;

public partial class MainWindow : Window
{
    private ObservableCollection<Subject> subjects;
    private Subject? currentSubject;

    public ICommand AddGradeCommand { get; }
    public ICommand EditGradeCommand { get; }
    public ICommand DeleteGradeCommand { get; }

    public Student? SelectedStudent { get; set; }

    public MainWindow()
    {
        InitializeComponent();

        AddGradeCommand = new RelayCommand(AddGrade, () => currentSubject != null);
        EditGradeCommand = new RelayCommand(EditGrade, () => SelectedStudent != null);
        DeleteGradeCommand = new RelayCommand(DeleteGrade, () => SelectedStudent != null);

        DataContext = this;
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

        subjects.Add(math);
        subjects.Add(physics);

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

    private void AddGrade()
    {
        if (currentSubject == null) return;

        AddGradeWindow addWindow = new AddGradeWindow(currentSubject.Students);
        addWindow.Owner = this;
        addWindow.ShowDialog();
    }

    private void EditGrade()
    {
        if (SelectedStudent == null)
        {
            MessageBox.Show("Выберите студента", "Ошибка",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        EditGradeWindow editWindow = new EditGradeWindow(SelectedStudent);
        editWindow.Owner = this;
        editWindow.ShowDialog();

        dgStudents.Items.Refresh();
    }

    private void DeleteGrade()
    {
        if (SelectedStudent == null)
        {
            MessageBox.Show("Выберите студента", "Ошибка",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        MessageBoxResult result = MessageBox.Show(
            $"Удалить оценку студента {SelectedStudent.Name}?",
            "Подтверждение удаления",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);

        if (result == MessageBoxResult.Yes)
        {
            SelectedStudent.Grade = "";
            SelectedStudent.Comment = "";
            MessageBox.Show("Оценка удалена", "Успех",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    private void Exit_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void Students_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Список студентов:\n- Иванов Иван\n- Петров Петр\n- Сидорова Анна",
            "Студенты", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private void Subjects_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Доступные предметы:\n- Математика\n- Физика",
            "Предметы", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private void About_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Электронный журнал преподавателя\nВерсия 1.0",
            "О программе", MessageBoxButton.OK, MessageBoxImage.Information);
    }
}