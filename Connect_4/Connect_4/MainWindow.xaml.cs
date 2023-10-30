using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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
using System.Xml.Linq;

namespace Connect_4
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Ellipse[,] ellipses = new Ellipse[6,5];
        Button[] arrows = new Button[5];
        string curPlayer = "red";
        public MainWindow()
        {
            InitializeComponent();
            for (int i = 1; i < ellipses.GetLength(0); i++)
                for (int j = 0; j < ellipses.GetLength(1); j++)
                {
                    ellipses[i, j] = new Ellipse();
                    ellipses[i, j].Name = "blank";
                    ellipses[i, j].Width = 100;
                    ellipses[i, j].Height = 100;

                    board.Children.Add(ellipses[i, j]);
                    Grid.SetRow(ellipses[i, j], i);
                    Grid.SetColumn(ellipses[i, j], j);
                }

            for (int i = 0; i < arrows.GetLength(0); i++)
            {
                arrows[i] = new Button();

                board.Children.Add(arrows[i]);
                Grid.SetRow(arrows[i], 0);
                Grid.SetColumn(arrows[i], i);

                arrows[i].Click += Click;
            }
        }

        private void Click(object sender, RoutedEventArgs e)
        {
            var arrow = sender as Button;

            int col = Grid.GetColumn(arrow);

            int i = 5;

            while(i > 0)
            {
                if (ellipses[i, col].Name == "blank")
                {
                    ellipses[i, col].Name = curPlayer;
                    break;
                }
                i--;
            }

            if (i - 1 == 0)
                arrow.IsEnabled = false;

            if (IsWin(curPlayer))
                MessageShow("Player " + curPlayer + " wins!!!\n\nDo you want to play again?", "WIN");

            if (curPlayer == "red")
                curPlayer = "blue";
            else
                curPlayer = "red";

            if (!IsWin(curPlayer) && checkButtons())
                MessageShow("DRAW!!!\n\nDo you want to play again?", "Draw");

        }

        private bool IsWin(string color)
        {
            for (int i = 1; i < ellipses.GetLength(0); i++)
            {
                int horizontal = 0, vertical = 0;
                
                for (int j = 1; j <= ellipses.GetLength(1); j++)
                {
                    if (ellipses[i, j-1].Name == color)
                        horizontal++;

                    else if (ellipses[i, j - 1].Name != color && ellipses[i, j - 1].Name != "blank" && horizontal != 0)
                    {
                        break;
                    }
                    
                    if (ellipses[j, i - 1].Name == color)
                        vertical++;

                    else if (ellipses[j, i - 1].Name != color && ellipses[j, i - 1].Name != "blank" && vertical != 0)
                    {
                        break;
                    }
                }

                if (horizontal == 4 || vertical == 4)
                    return true;

            }

            if ((ellipses[1,0].Name == color &&
                ellipses[2, 1].Name == color &&
                ellipses[3, 2].Name == color &&
                ellipses[4, 3].Name == color)||( 
                ellipses[2, 1].Name == color &&
                ellipses[3, 2].Name == color &&
                ellipses[4, 3].Name == color &&
                ellipses[5, 4].Name == color)||(
                ellipses[1, 4].Name == color &&
                ellipses[2, 3].Name == color &&
                ellipses[3, 2].Name == color &&
                ellipses[4, 1].Name == color)||(
                ellipses[2, 3].Name == color &&
                ellipses[3, 2].Name == color &&
                ellipses[4, 1].Name == color &&
                ellipses[5, 0].Name == color)||(
                ellipses[2, 0].Name == color &&
                ellipses[3, 1].Name == color &&
                ellipses[4, 2].Name == color &&
                ellipses[5, 3].Name == color)||(
                ellipses[2, 4].Name == color &&
                ellipses[3, 3].Name == color &&
                ellipses[4, 2].Name == color &&
                ellipses[5, 1].Name == color)||(
                ellipses[1, 1].Name == color &&
                ellipses[2, 2].Name == color &&
                ellipses[3, 3].Name == color &&
                ellipses[4, 4].Name == color)||(
                ellipses[1, 3].Name == color &&
                ellipses[2, 2].Name == color &&
                ellipses[3, 1].Name == color &&
                ellipses[4, 0].Name == color))
                return true;

            return false;
        }

        private void MessageShow(string message, string caption)
        {
            MessageBoxResult temp = MessageBox.Show(message, caption, MessageBoxButton.YesNo);
            if (temp == MessageBoxResult.Yes)
            {
                ResetGame();
            }
            else
            {
                Environment.Exit(0);
            }
        }

        private void ResetGame()
        {
            foreach (var button in arrows)
                button.IsEnabled = true;

            for (int i = 1; i < ellipses.GetLength(0); i++)
                for (int j = 0; j < ellipses.GetLength(1); j++)
                    ellipses[i, j].Name = "blank";
        }

        private bool checkButtons()
        {
            foreach (var button in arrows)
                if(button.IsEnabled)
                    return false;

            return true;
        }
    }
}
