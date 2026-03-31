using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Task;

public partial class AddGradeWindow : Window
{
    private ObservableCollection<Student> students;
    private Student selectedStudent;

    public AddGradeWindow(ObservableCollection<Student> students)
    {
        InitializeComponent();
        this.students = students;
        cmbStudent.ItemsSource = students;
        cmbStudent.SelectedIndex = 0;
    }

    private void BtnOk_Click(object sender, RoutedEventArgs e)
    {
        if (cmbStudent.SelectedItem == null)
        {
            MessageBox.Show("Выберите студента", "Ошибка",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        selectedStudent = (Student)cmbStudent.SelectedItem;
        string grade = (cmbGrade.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "3";
        string comment = txtComment.Text;

        selectedStudent.Grade = grade;
        selectedStudent.Comment = comment;

        DialogResult = true;
        Close();
    }

    private void BtnCancel_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        Close();
    }
}