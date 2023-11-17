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

namespace WolfAndSheep
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly WolfAndSheepDbContext _dbContext;
        bool whoMove = true, wolfLose, sheepLose;
        Ellipse wolf;
        Ellipse sheep;
        List<Ellipse> moveList;
        public MainWindow()
        {
            _dbContext = new WolfAndSheepDbContext();
            moveList = new List<Ellipse>();

            _dbContext.Moves.RemoveRange(_dbContext.Moves);
            _dbContext.SaveChanges();

            InitializeComponent();

            for(int i = 0; i < 8; i++)
                for(int j = i % 2; j < 8; j+= 2)
                {
                    Rectangle r = new Rectangle()
                    {
                        Fill = Brushes.Black
                    };
                    board.Children.Add(r);
                    Grid.SetRow(r, i);
                    Grid.SetColumn(r, j);
                }

            for (int i = 0; i < 8; i += 2)
            {
                sheep = new Ellipse()
                {
                    Fill = Brushes.Aqua,
                    Width = 80,
                    Height = 80
                };
                sheep.MouseLeftButtonDown += SheepClick;
                board.Children.Add(sheep);
                Grid.SetRow(sheep, 0);
                Grid.SetColumn(sheep, i);
            }

            wolf = new Ellipse()
            {
                Fill = Brushes.Green,
                Width = 80,
                Height = 80
            };
            wolf.MouseLeftButtonDown += WolfClick;
            board.Children.Add(wolf);
            Grid.SetRow(wolf, 7);
            Grid.SetColumn(wolf, 7);
        }

        private void WolfClick(object sender, MouseButtonEventArgs e)
        {
            if (whoMove)
            {
                wolfLose = true;

                wolf = (Ellipse)sender;
                int row = Grid.GetRow(wolf);
                int col = Grid.GetColumn(wolf);
                
                GenerateMoveList(4);
                WolfGhost(row, col, false);
                             
            }

        }

        private void WolfGhost(int row, int col, bool check) 
        {
            bool up = false;
            row--;
            if (row >= 0) {
                up = true;
                col++;
                if (col <= 7) {
                    ShowMove(row, col, 0, check);

                }
                col -= 2;
                if (col >= 0)
                    ShowMove(row, col, 1, check);
            }
            
            if (!up) col--;
            row += 2;

            if (row <= 7) {
                if (col >= 0)
                    ShowMove(row, col, 2, check);
                col += 2;
                if (col <= 7)
                    ShowMove(row, col, 3, check);
            }
        }

        private void SheepClick(object sender, MouseButtonEventArgs e)
        {
            if (!whoMove)
            {
                sheep = (Ellipse)sender;
                int row = Grid.GetRow(sheep);
                int col = Grid.GetColumn(sheep);
                
                GenerateMoveList(2);
                SheepGhost(row, col, false);   
            }
        }

        private void SheepGhost(int row, int col, bool check) 
        {
            row++;
            if (row <= 7) {
                col++;
                if (col <= 7) {
                    ShowMove(row, col, 0, check);
                }
                col -= 2;
                if (col >= 0) {
                    ShowMove(row, col, 1, check);
                }

            }
        }
        private void GenerateMoveList(int count)
        {
            if(moveList.Count > 0)
            {
                foreach (var move in moveList)
                    board.Children.Remove(move);
                moveList.Clear();
            }
            for (int i = 0; i < count; i++)
            {
                Ellipse temp = new Ellipse()
                {
                    Fill = Brushes.LightGray,
                    Width = 80,
                    Height = 80
                };
                temp.MouseLeftButtonDown += TempClick;
                moveList.Add(temp);
            }
        }
        private void ShowMove(int row, int col, int count, bool check)
        {
            var IsEllipse = board.Children
                .Cast<UIElement>()
                .LastOrDefault(e => Grid.GetRow(e) == row 
                    && Grid.GetColumn(e) == col);

            if (IsEllipse.GetType() == typeof(Rectangle))
            {
                if (!check) 
                    {
                    board.Children.Add(moveList[count]);
                    Grid.SetRow(moveList[count], row);
                    Grid.SetColumn(moveList[count], col);
                }
                wolfLose = false;
                sheepLose = false;
            }
        }
        private void TempClick(object sender, MouseButtonEventArgs e)
        {
            var temp = sender as Ellipse;
            int row = Grid.GetRow(temp);
            int col = Grid.GetColumn(temp);

            GenerateMoveList(0);

            Ellipse move = new Ellipse();
            if (whoMove)
            {
                move = wolf;
                whoMove = false;
            }
            else
            {
                move = sheep;
                whoMove = true;
            }

            Move m = new Move() {
                BeforeRow = Grid.GetRow(move),
                BeforeCol = Grid.GetColumn(move),
                AfterRow = row,
                AfterCol = col
            };

            _dbContext.Moves.Add(m);
            _dbContext.SaveChanges();
                
            Grid.SetRow(move, row);
            Grid.SetColumn(move, col);

            IsLose();
            WolfWin();
        }

        private void Button_Click(object sender, RoutedEventArgs e) 
        {
            var move = _dbContext.Moves
                .OrderBy(x => x.Id)
                .LastOrDefault();
            if(move != null) 
            {
                var ellipse = board.Children
                    .Cast<UIElement>()
                    .LastOrDefault(x => Grid.GetRow(x) == move.AfterRow
                        && Grid.GetColumn(x) == move.AfterCol);
                
                Grid.SetRow(ellipse, move.BeforeRow);
                Grid.SetColumn(ellipse, move.BeforeCol);

                _dbContext.Remove(move);
                _dbContext.SaveChanges();

                if (whoMove)
                    whoMove = false;
                else
                    whoMove = true;
            }
        }

        private void IsLose()
        {
            sheepLose = true;
            int row, col;

            var list = board.Children
                .Cast<UIElement>()
                .ToList()
                .FindAll(e => e.GetType() == typeof(Ellipse));

            foreach(Ellipse e in list)
            {
                row = Grid.GetRow(e);
                col = Grid.GetColumn(e);

                if (e.Fill == Brushes.Aqua)
                    SheepGhost(row, col, true);
            }
            if (sheepLose)
                MessageBox.Show("Sheep lost");

            wolfLose = true;
            row = Grid.GetRow(wolf);
            col = Grid.GetColumn(wolf);
            WolfGhost(row, col, true);
            if (wolfLose)
                MessageBox.Show("Wolf lost");
        }
        private void WolfWin() 
        {
            bool win = true;

            int rowWolf = Grid.GetRow(wolf);

            var list = board.Children
                .Cast<UIElement>()
                .ToList()
                .FindAll(e => e.GetType() == typeof(Ellipse));

            foreach(Ellipse e in list) 
            {
                if(e.Fill == Brushes.Aqua) 
                    {
                    int row = Grid.GetRow(e);
                    if (row < rowWolf) 
                        {
                        win = false;
                        break;
                    }
                }
            }

            if (win)
                MessageBox.Show("Wolf Won");
        }
    }
}
