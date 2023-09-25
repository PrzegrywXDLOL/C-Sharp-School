using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

namespace TicTacToe_Windowed
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Style btnX, btnO, clear, win;
        Button[,] buttons = new Button[3,3];
        bool xo = true;
        int counter = 0;
        Player player1 = new Player();
        Player player2 = new Player();
        Random rnd = new Random();
        public MainWindow()
        {
            InitializeComponent();

            btnX = this.FindResource("btnXclick") as Style;
            btnO = this.FindResource("btnOclick") as Style;
            clear = this.FindResource("clear") as Style;
            win = this.FindResource("win") as Style;

            RandPlayer();

            for(int i = 0 ; i < buttons.GetLength(0); i++)
            {
                for(int j = 0 ; j < buttons.GetLength(1); j++)
                {
                    buttons[i, j] = new Button();
                    buttons[i, j].Content = "";
                    buttons[i, j].FontSize = 100;

                    board.Children.Add(buttons[i, j]);
                    Grid.SetRow(buttons[i, j], i);
                    Grid.SetColumn(buttons[i, j], j);

                    buttons[i, j].Click += new RoutedEventHandler(ClickForButton);
                }
            }

        }

        private void ClickForButton(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            if (button.Content.ToString() == "")
            {
                counter ++;
                if (xo)
                {
                    button.Content = "X";
                    button.Style = btnX;
                    if (CheckWin("X"))
                    {
                        if (player1.xo)
                            player1.score++;
                        else
                            player2.score++;
                        MessageShow(@"                   Player X wins!
        Do you want to play again?", "Victory!");
                    }
                    else if(counter == 9)
                        MessageShow(@"                      Draw!
        Do you want to play again?", "Draw!");
                    else
                        xo = false;
                }
                else
                {
                    button.Content = "O";
                    button.Style = btnO;
                    if (CheckWin("O"))
                    {
                        if (player1.xo)
                            player1.score++;
                        else
                            player2.score++;
                        MessageShow(@"                   Player O wins!
        Do you want to play again?", "Victory!");
                    }
                    else if (counter == 9)
                        MessageShow(@"                      Draw!
        Do you want to play again?", "Draw!");
                    else
                        xo = true;
                }
            }
        }
        
        private bool CheckWin(string xo)
        {
            for (int i = 0; i < buttons.GetLength(0); i++)
            {
                bool horizontal = true, vertical = true;
                for (int j = 0; j < buttons.GetLength(1); j++)
                {
                    if (buttons[i, j].Content.ToString() != xo)
                    {
                        horizontal = false;
                    }
                    if (buttons[j, i].Content.ToString() != xo)
                    {
                        vertical = false;
                    }
                }

                if (horizontal || vertical)
                {
                    if (horizontal)
                    {
                        buttons[i, 0].Style = win;
                        buttons[i, 1].Style = win;
                        buttons[i, 2].Style = win;
                    }else if (vertical)
                    {
                        buttons[0, i].Style = win;
                        buttons[1, i].Style = win;
                        buttons[2, i].Style = win;
                    }
                    return true;
                }
            }

            if ((buttons[0, 0].Content.ToString() == xo)
                && (buttons[1, 1].Content.ToString() == xo) 
                && buttons[2, 2].Content.ToString() == xo)
            {
                buttons[0, 0].Style = win;
                buttons[1, 1].Style = win;
                buttons[2, 2].Style = win;
                return true;
            }
            if ((buttons[0, 2].Content.ToString() == xo)
                && (buttons[1, 1].Content.ToString() == xo)
                && buttons[2, 0].Content.ToString() == xo)
            {
                buttons[0, 2].Style = win;
                buttons[1, 1].Style = win;
                buttons[2, 0].Style = win;
                return true;
            }

            return false;
        }

        private void ResetGame()
        {
            counter = 0;
            xo = true;
            foreach (var button in buttons) 
            {
                button.Content = "";
                button.Style = clear;
            }
            RandPlayer();
        }

        private void MessageShow(string message, string caption)
        {
            MessageBoxResult temp = MessageBox.Show(message,caption, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (temp == MessageBoxResult.Yes)
            {
                MessageBox.Show("Player 1: " + player1.score + @"
Player 2: " + player2.score);
                ResetGame();
            }
            else
            {
                MessageBox.Show("Player 1: " + player1.score + @"
Player 2: " + player2.score);
                Environment.Exit(0);
            }
        }
        
        private void RandPlayer()
        {
            if (rnd.Next(2) % 2 == 0)
            {
                player1.xo = true;
                MessageBox.Show("Player 1 starts");
            }
            else
            {
                player2.xo = true;
                MessageBox.Show("Player 2 starts");
            }
        }
    }
}
