using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp2.Interface;
using WpfApp2.UI;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        StudentsContext db = StudentsContext.GetContext();
        public MainWindow()
        {
            InitializeComponent();
            db.Студенты.Load();
            dgStudents.ItemsSource = db.Студенты.Local.ToBindingList();
        }

        private void ExitProg(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void editObj_Click(object sender, RoutedEventArgs e)
        {
            EditObjWin editObjWin = new EditObjWin();
            int indexRow = dgStudents.SelectedIndex;
            if (indexRow != -1)
            {
                var item = (Студенты)dgStudents.Items[indexRow];
                Data.Id = item.Номер_зачетной_книжки;
                editObjWin.ShowDialog();
            }
            dgStudents.Items.Refresh();
        }

        private void addObj_Click(object sender, RoutedEventArgs e)
        {
            AddObjWin addObjWin = new AddObjWin();
            addObjWin.ShowDialog();
            dgStudents.Items.Refresh();
        }

        private void delObj_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Удалить запись?", "Удаление записи", MessageBoxButton.YesNo);
            if (res == MessageBoxResult.Yes)
            {
                int indexRow = dgStudents.SelectedIndex;
                if (indexRow != -1)
                {
                    var item = (Студенты)dgStudents.Items[indexRow];
                    db.Студенты.Remove(item);
                    db.SaveChanges();
                }
            }
            else
            {
                return;
            }
        }

        List<Студенты> _jjk;


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _jjk = db.Студенты.ToList();
            var filtered = _jjk.Where(_jjk => Convert.ToString(_jjk.Живет_ли_студент_в_общежитии) == filter.Text);
            dgStudents.ItemsSource = filtered;
        }

        private void stop(object sender, RoutedEventArgs e)
        {
            dgStudents.ItemsSource = db.Студенты.Local.ToBindingList();
            filter.Clear();
        }

        private void filter_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
    public static class Data
    {
        public static int Id;
    }
}