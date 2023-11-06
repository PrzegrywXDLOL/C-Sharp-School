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

namespace crud5F
{
    /// <summary>
    /// Logika interakcji dla klasy Create_Edit.xaml
    /// </summary>
    public partial class Create_Edit : Window
    {
        public bool close = false;
        public Create_Edit()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(name.Text != "" & surname.Text != "" & email.Text != "")
            {
                close = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Not all windows are filled");
            }
        }
    }
}
