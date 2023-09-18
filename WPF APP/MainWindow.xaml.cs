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

namespace WPF_APP
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random random = new Random();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string labelValue = label.Content.ToString();
            int value = int.Parse(labelValue);
            value++;

            label.Content = value.ToString();

            Grid.SetColumn(button, random.Next(3));

            progress.Value = value * 10;
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            label.Content = 0.ToString();
            progress.Value = 0;
        }

        private void progress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (e.NewValue == 100) 
            {
                MessageBox.Show("Wygrales");
                progress.Value = 0;
                label.Content = 0.ToString();
            }
        }
    }
}
