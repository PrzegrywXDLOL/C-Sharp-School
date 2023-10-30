using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Lists_WPF
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Person> items = new List<Person>();
        public MainWindow()
        {
            InitializeComponent();
            items.Add(new Person("Jan", "Kowalski"));
            items.Add(new Person("Adam", "Nowak"));
            items.Add(new Person("Ewa", "Jakis"));

            list.ItemsSource = items;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Add_Edit add = new Add_Edit();
            add.ShowDialog();

            if (add.close)
            {
                items.Add(new Person(add.Name.Text, add.Surname.Text));
                list.ItemsSource = null;
                list.ItemsSource = items;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Person person = list.SelectedItem as Person;
            if (person != null)
            {
                Add_Edit edit = new Add_Edit();
                edit.Name.Text = person.Name;
                edit.Surname.Text = person.Surname;
                edit.btn.Content = "Edit";

                edit.ShowDialog();

                if (edit.close)
                {
                    person.Name = edit.Name.Text;
                    person.Surname = edit.Surname.Text;

                    list.ItemsSource = null;
                    list.ItemsSource = items;
                }
            }
            else
            {
                MessageBox.Show("None object selected",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Person person = list.SelectedItem as Person;
            if(person != null)
            {
                items.Remove(person);
                MessageBox.Show("Deleted: " + person.Name + " " + person.Surname,
                    "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("None object selected", 
                    "Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            var temp = items.Where(
                l => l.Name.ToLower().Contains(Search.Text.ToLower().ToString())
                || l.Surname.ToLower().Contains(Search.Text.ToLower().ToString()));
            list.ItemsSource = temp;
        }
    }
}
