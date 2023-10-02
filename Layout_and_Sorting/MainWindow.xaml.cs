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

namespace Layout
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Button[] buttons;
        Random random;
        int count;
        int[] tab;
        public MainWindow()
        {
            InitializeComponent();
            buttons = new Button[4];
            random = new Random();
            count = 0;

            for(int i = 0; i < buttons.Length; i++)
            {
                buttons[i] = new Button();
                buttons[i].Content = random.Next(10);
                buttons[i].FontSize = 50;

                buttons[i].Click += leftClick;
                buttons[i].MouseRightButtonDown += rightClick;

                uniform.Children.Add(buttons[i]);
            }
        }

        private void leftClick(object sender, RoutedEventArgs e)
        {
            count = 0;
            var button = (Button)sender;
            button.Content = random.Next(10);
        }

        private void rightClick(object sender, MouseButtonEventArgs e)
        {
            count = 0;
            foreach (var item in buttons) 
            {
                item.Content = random.Next(10);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string textTemp = string.Empty;
            tab = new int[buttons.Length];
            
            for (int i = 0; i < buttons.Length; i++)
                tab[i] = int.Parse(buttons[i].Content.ToString());
            
            while (!IsSort())
            {
                count++;
                for(int i = 0; i <= buttons.Length; i++)
                {
                    int a = random.Next(buttons.Length);
                    int b = random.Next(buttons.Length);

                    var temp = tab[a];
                    tab[a] = tab[b];
                    tab[b] = temp;
                }

                textTemp += count.ToString() + ". ";
                foreach (var item in tab)
                    textTemp += item.ToString();
                textTemp += "\n";
            }

            for(int i = 0; i < buttons.Length; i++)
                buttons[i].Content = tab[i].ToString();

            text.Text = textTemp;

            MessageBox.Show("Liczba losowań: " + count);
        }

        private bool IsSort()
        {
            bool sort = true;

            for(int i = 1; i < buttons.Length; i++)
                if (int.Parse(tab[i].ToString())
                    < int.Parse(tab[i - 1].ToString()))
                {
                    sort = false;
                    break;
                }

            return sort;
        }
    }
}
