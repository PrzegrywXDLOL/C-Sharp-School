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
using System.Windows.Shapes;

namespace crud5FNet7
{
    /// <summary>
    /// Logika interakcji dla klasy EditPass.xaml
    /// </summary>
    public partial class EditPass : Window
    {
        User user;
        private readonly crud5FNet7DbContext _dbContext;
        public EditPass(User u)
        {
            _dbContext = new crud5FNet7DbContext();
            user = u;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (oldPass.Text.Length > 0 && newPass1.Text.Length > 0 && newPass2.Text.Length > 0)
            {
                var pass = User.HashPassword(oldPass.Text.ToString());
                if (pass == user.Password)
                {
                    if (newPass1.Text == newPass2.Text)
                    {
                        pass = User.HashPassword(newPass1.Text.ToString());

                        var temp = _dbContext.Users.FirstOrDefault(x => x.Id == user.Id);
                        
                        temp.Password = pass;

                        _dbContext.SaveChanges();
                        this.Close();
                    }
                    else
                        MessageBox.Show("New password boxes are not the same");
                }
                else
                    MessageBox.Show("Wrong old password");
            }
            else
                MessageBox.Show("Fill all boxes");
        }
    }
}
