using System;
using System.Collections.Generic;
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

namespace crud5F
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        entity _db = new entity();
        public MainWindow()
        {
            InitializeComponent();
            data.ItemsSource = _db.users.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Create_Edit window = new Create_Edit();

            window.ShowDialog();

            if (window.close)
            {
                user user = new user()
                {
                    Name = window.name.Text,
                    Surname = window.surname.Text,
                    Email = window.email.Text
                };

                _db.users.Add(user);
                _db.SaveChanges();

                data.ItemsSource = null;
                data.ItemsSource = _db.users.ToList();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var user = data.SelectedItem as user;
            
            if(user != null)
            {
                Create_Edit window = new Create_Edit();

                window.name.Text = user.Name;
                window.surname.Text = user.Surname;
                window.email.Text = user.Email;
                window.btn.Content = "Edit";

                window.ShowDialog();

                if (window.close)
                {
                    user.Name = window.name.Text;
                    user.Surname = window.surname.Text;
                    user.Email = window.email.Text;

                    _db.SaveChanges();
                    data.ItemsSource = null;
                    data.ItemsSource = _db.users.ToList();
                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var user = data.SelectedItem as user;
            
            _db.users.Remove(user);

            _db.SaveChanges();
            data.ItemsSource = null;
            data.ItemsSource = _db.users.ToList();
        }

        private void search_TextChanged(object sender, TextChangedEventArgs e)
        {
            var text = search.Text.ToString();

            if (text != "")
            {
                var temp = _db.users.Where(x => x.Name.ToLower().Contains(text) ||
                x.Surname.ToLower().Contains(text)).ToList();

                data.ItemsSource = null;
                data.ItemsSource = temp;
            }
            else
            {
                data.ItemsSource = null;
                data.ItemsSource = _db.users.ToList();
            }
        }
    }
}
