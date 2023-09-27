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

namespace WPF_Binding
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            pierwszy.Text = "tekst";
            drugi.Text = "tekst";
            txtSlider.Text = 10.ToString();
            pierwszy.FontSize = 10;
            slider.Value = 10;
        }

        private void pierwszy_TextChanged(object sender, TextChangedEventArgs e)
        {
            drugi.Text = pierwszy.Text;
        }

        private void drugi_TextChanged(object sender, TextChangedEventArgs e)
        {
            pierwszy.Text = drugi.Text;
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //txtSlider.Text = slider.Value.ToString();
            pierwszy.FontSize = slider.Value;
        }
        private void txtSlider_TextChanged(object sender, TextChangedEventArgs e)
        {
            slider.Value = int.Parse(txtSlider.Text);
        }
    }
}
