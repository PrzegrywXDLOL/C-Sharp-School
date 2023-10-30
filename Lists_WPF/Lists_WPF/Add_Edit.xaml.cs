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

namespace Lists_WPF
{
    /// <summary>
    /// Logika interakcji dla klasy Add_Edit.xaml
    /// </summary>
    public partial class Add_Edit : Window
    {
        public bool close = false;
        public Add_Edit()
        {
            InitializeComponent();
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            if(Name.Text.Length == 0 || Surname.Text.Length == 0)
                MessageBox.Show("Fill all boxes", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            else
                close = true;
                this.Close();
        }
    }
}
