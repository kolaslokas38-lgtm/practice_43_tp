using System.Windows;
using System.Windows.Controls;

namespace Task;

public partial class EditGradeWindow : Window
{
    private Student student;

    public EditGradeWindow(Student student)
    {
        InitializeComponent();
        this.student = student;

        txtStudentName.Text = student.Name;

        foreach (ComboBoxItem item in cmbGrade.Items)
        {
            if (item.Content.ToString() == student.Grade)
            {
                cmbGrade.SelectedItem = item;
                break;
            }
        }

        txtComment.Text = student.Comment;
    }

    private void BtnSave_Click(object sender, RoutedEventArgs e)
    {
        string grade = (cmbGrade.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "3";
        string comment = txtComment.Text;

        student.Grade = grade;
        student.Comment = comment;

        DialogResult = true;
        Close();
    }

    private void BtnCancel_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        Close();
    }
}