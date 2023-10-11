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
        int countwin = 0;
        Random random;
        public MainWindow()
        {
            InitializeComponent();
            random = new Random();

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
            bool color3 = false;
            
            if(countwin > 0 && countwin % 2 == 0)
            {
                color3 = true;
            }

            var btn = sender as Button;
            var col = Grid.GetColumn(btn);
            var row = Grid.GetRow(btn);

            ChangeColor(row, col, color3);
            ChangeColor(row - 1, col, color3);
            ChangeColor(row + 1, col, color3);
            ChangeColor(row, col - 1, color3);
            ChangeColor(row, col + 1, color3);

            if (CheckWin(color3))
            {
                MessageBoxResult result = MessageBox.Show("Congratulations You Won", "Win", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    countwin++;
                    ResetGame();
                }
                else
                {
                    Environment.Exit(0);
                }
            }

        }

        private bool CheckWin(bool c3)
        {
            if (!c3)
            {
                bool win = true;
                for (int i = 0; i < buttons.GetLength(0); i++)
                    for (int j = 0; j < buttons.GetLength(1); j++)
                        if ((string)buttons[i, j].Content != " ")
                            win = false;
                return win;
            }
            else
            {
                bool red = true;
                for (int i = 0; i < buttons.GetLength(0); i++)
                    for (int j = 0; j < buttons.GetLength(1); j++)
                        if ((string)buttons[i, j].Content != " ")
                            red = false;

                bool green = true;
                for (int i = 0; i < buttons.GetLength(0); i++)
                    for (int j = 0; j < buttons.GetLength(1); j++)
                        if ((string)buttons[i, j].Content != "  ")
                            green = false;

                if (red || green)
                    return true;
                else
                    return false;
            }
        }

        private void ResetGame()
        {
            int colors;

            for (int i = 0; i < buttons.GetLength(0); i++)
            {
                for (int j = 0; j < buttons.GetLength(1); j++)
                {
                    buttons[i, j].Content = "";
                }
            }

            if (countwin > 0 && countwin % 2 == 1)
                colors = countwin + 1;
            else
                colors = countwin;

            while(colors > 0)
            {
                int r = random.Next(3);
                int c = random.Next(5);
                if (buttons[r,c].Content.ToString() == "".ToString())
                {
                    buttons[r,c].Content = " ".ToString();
                    colors--;
                }
            }

        }

        private void ChangeColor(int row, int col, bool c3)
        {
            try
            {
                if ((string)buttons[row, col].Content == "")
                    buttons[row, col].Content = " ";
                else if (c3)
                {
                    if ((string)buttons[row, col].Content == " ")
                        buttons[row, col].Content = "  ";
                    else
                        buttons[row, col].Content = "";
                }
                else
                    buttons[row, col].Content = "";
            }
            catch { }
        }
    }
}
