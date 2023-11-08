using crud5FNet7.Entity;
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

namespace crud5FNet7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly crud5FNet7DbContext _dbContext;
        public MainWindow()
        {
            _dbContext = new crud5FNet7DbContext();
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(Login.Text.Length > 0 && Password.Text.Length > 0)
            {
                User user = new User()
                {
                    Name = Name.Text.ToString(),
                    Login = Login.Text.ToString(),
                    Password = User.HashPassword(Password.Text.ToString())
                };
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();
            }
            else
                MessageBox.Show("Fill login and password");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Login.Text.Length > 0 && Password.Text.Length > 0)
            {
                var pass = User.HashPassword(Password.Text.ToString());
                var user = _dbContext.Users
                    .FirstOrDefault(u => u.Login == Login.Text.ToString() && u.Password == pass);
                if (user != null)
                {
                    EditPass window = new EditPass(user);
                    window.ShowDialog();
                }
                else
                    MessageBox.Show("Wrong login / password");
            }
            else
                MessageBox.Show("Fill login and password");
        }
    }
}
