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
using System.Windows.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace Numerical_Tetris
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer;
        TextBlock box;
        int[,] numbers;
        Random random;
        public MainWindow()
        {
            InitializeComponent();
            random = new Random();
            numbers = new int[8, 4];
            timer = new DispatcherTimer();

            CreateBox();
            this.KeyDown += MainWindow_KeyDown;

            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key.ToString() == "Left".ToString())
            {
                int c = Grid.GetColumn(box);
                c--;
                if (c < 0)
                    c = 0;
                Grid.SetColumn(board, c);
            }
            if (e.Key.ToString() == "Right".ToString())
            {
                int c = Grid.GetColumn(box);
                c++;
                if (c > 3)
                    c = 3;
                Grid.SetColumn(board, c);
            }
        }
        private void CreateBox()
        {
            int r = random.Next(1, 5) * 2;
            int c = random.Next(4);
            box = new TextBlock
            {
                Text = r.ToString(),
                FontSize = 50,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                TextAlignment = TextAlignment.Center,
                Padding = new Thickness(5, 5, 5, 5),
                Width = 80,
                Height = 80,
                Background = new SolidColorBrush(Colors.Yellow)
            };
            board.Children.Add(box);
            Grid.SetColumn(box, c);
            Grid.SetRow(box, 0);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            int row = Grid.GetRow(box);
            int col = Grid.GetColumn(box);
            row++;
            if (numbers[row, col] == 0)
                if (row == 7)
                {
                    numbers[row, col] = int.Parse(box.Text.ToString());
                    Grid.SetRow(box, row);
                    CreateBox();
                }
                else
                {
                    Grid.SetRow(box, row);
                }
            else if (numbers[row, col] != int.Parse(box.Text.ToString()))
            {
                row--;
                numbers[row, col] = int.Parse(box.Text.ToString());
                CreateBox();
            }
            else
            {
                box.Text = (2 * numbers[row, col]).ToString();
                numbers[row, col] = int.Parse(box.Text.ToString());
                var tempp = board.Children
                        .Cast<UIElement>()
                        .First(el => Grid.GetRow(el) == row && Grid.GetColumn(el) == col);
                board.Children.Remove(tempp);
                Grid.SetRow(box, row);

                row++;
                while (row <= 7)
                    if (numbers[row - 1, col] == numbers[row, col])
                    {
                        box.Text = (2 * numbers[row, col]).ToString();
                        numbers[row, col] = int.Parse(box.Text.ToString());
                        Grid.SetRow(box, row);
                        numbers[row - 1, col] = 0;
                        var temp = board.Children
                                .Cast<UIElement>()
                                .First(el => Grid.GetRow(el) == row && Grid.GetColumn(el) == col);
                        board.Children.Remove(temp);
                        row++;
                    }
                    else
                    {
                        break;
                    }


                CreateBox();
            }
        }
    }
}
