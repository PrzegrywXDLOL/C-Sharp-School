using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Blue___Red
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Button[,] buttons = new Button[3, 5];
        public MainWindow()
        {
            InitializeComponent();

            for (int i = 0; i < buttons.GetLength(0); i++)
            {
                for (int j = 0; j < buttons.GetLength(1); j++)
                {
                    buttons[i, j] = new Button();
                    buttons[i, j].Content = "";

                    gamespace.Children.Add(buttons[i, j]);
                    Grid.SetRow(buttons[i, j], i);
                    Grid.SetColumn(buttons[i, j], j);

                    buttons[i, j].Click += new RoutedEventHandler(OnClick);
                }
            }
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var col = Grid.GetColumn(btn);
            var row = Grid.GetRow(btn);

            try 
            {
                if ((string)buttons[row, col + 1].Content == "")
                    buttons[row, col + 1].Content = " ";
                else
                    buttons[row, col + 1].Content = "";

            } catch { }
            
            try
            {
               if ((string)buttons[row + 1, col].Content == "")
                    buttons[row + 1, col].Content = " ";
                else
                    buttons[row + 1, col].Content = "";
            }
            catch { }
            
            try 
            {
                if ((string)buttons[row - 1, col].Content == "")
                    buttons[row - 1, col].Content = " ";
                else
                    buttons[row - 1, col].Content = "";
            } catch { }
            try
            {
                if ((string)buttons[row, col - 1].Content == "")
                    buttons[row, col - 1].Content = " ";
                else
                    buttons[row, col - 1].Content = "";

            } catch { }

            if ((string)buttons[row, col].Content == "")
                buttons[row, col].Content = " ";
            else
                buttons[row, col].Content = "";


            if (CheckWin())
            {
                MessageBox.Show("Congratulations You Win!");
                ResetGame();
            }

        }

        private bool CheckWin()
        {
            bool win = true;

            for (int i = 0; i < buttons.GetLength(0); i++)
            {
                for (int j = 0; j < buttons.GetLength(1); j++)
                {
                    if ((string)buttons[i, j].Content != " ")
                        win = false;
                }
            }

            return win;
        }

        private void ResetGame()
        {
            for (int i = 0; i < buttons.GetLength(0); i++)
            {
                for (int j = 0; j < buttons.GetLength(1); j++)
                {
                    buttons[i, j].Content = "";
                }
            }
        }
    }
}
