using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace WpfApp2.UI
{
    /// <summary>
    /// Логика взаимодействия для EditObjWin.xaml
    /// </summary>
    public partial class EditObjWin : Window
    {
        private readonly List<string> list = new List<string>
        {
            "Да", "Нет"
        };
        StudentsContext db = StudentsContext.GetContext();
        Студенты students = new Студенты();

        public EditObjWin()
        {
            InitializeComponent();
            NameTextBox.Focus();

            cbRPM.ItemsSource = list;
            cbDatabase.ItemsSource = list;
            cbOKFCK.ItemsSource = list;
            cbP_TPT.ItemsSource = list;
            cbTD.ItemsSource = list;
            cbHostel.ItemsSource = list;
        }

        private void ChangeData_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (SurnameTextBox.Text.Length == 0 || NameTextBox.Text.Length == 0 || MiddleNameTextBox.Text.Length == 0) { errors.AppendLine("Введите ФИО"); }
            if (int.TryParse(indexGroup.Text, out int group) && indexGroup.Text.Length == 0) errors.AppendLine("Введите индекс группы");
            if (cbHostel.SelectedItem == null) errors.AppendLine("Выберите живет ли студент в общежитие");

            if (cbRPM.SelectedItem == null) errors.AppendLine("Выберите из выпадающего списка вариант желат изучать или нет");
            if (cbDatabase.SelectedItem == null) errors.AppendLine("Выберите из выпадающего списка вариант желат изучать или нет");
            if (cbOKFCK.SelectedItem == null) errors.AppendLine("Выберите из выпадающего списка вариант желат изучать или нет");
            if (cbP_TPT.SelectedItem == null) errors.AppendLine("Выберите из выпадающего списка вариант желат изучать или нет");
            if (cbTD.SelectedItem == null) errors.AppendLine("Выберите из выпадающего списка вариант желат изучать или нет");

            if (errors.Length > 0) { MessageBox.Show($"{errors}"); return; }

            students.Фамилия = SurnameTextBox.Text;
            students.Отчество = MiddleNameTextBox.Text;
            students.Имя = NameTextBox.Text;
            students.Индекс_группы = group;
            students.Живет_ли_студент_в_общежитии = Convert.ToString(cbHostel.SelectedItem);

            students.РПМ = Convert.ToString(cbRPM.SelectedItem);
            students.ОКФСК = Convert.ToString(cbOKFCK.SelectedItem);
            students.П_и_ТПТ = Convert.ToString(cbP_TPT.SelectedItem);
            students.СС_и_ТД = Convert.ToString(cbTD.SelectedItem);
            students.ТРиЗБД = Convert.ToString(cbDatabase.SelectedItem);

            try
            {
                db.Студенты.Add(students);
                db.SaveChanges();
                this.Close();
            }
            catch (Exception ex)
            {
                errors.AppendLine(ex.Message);
            }

        }

        private void Stopbut_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            students = db.Студенты.Find(Data.Id);
            NameTextBox.Text = students.Имя;
            MiddleNameTextBox.Text = students.Фамилия;
            SurnameTextBox.Text = students.Отчество;
            indexGroup.Text = Convert.ToString(students.Индекс_группы);

            cbHostel.Text = students.Живет_ли_студент_в_общежитии;
            cbRPM.Text = students.РПМ;
            cbP_TPT.Text = students.П_и_ТПТ;
            cbOKFCK.Text = students.ОКФСК;
            cbTD.Text = students.СС_и_ТД;
            cbDatabase.Text = students.ТРиЗБД;
        }
    }
}
