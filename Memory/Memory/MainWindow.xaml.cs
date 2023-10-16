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

namespace Memory
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<Coord> coordList;
        List<BTN> btnList;
        BTN[,] buttons;
        Random rnd;
        int clickCount = 0;
        public MainWindow()
        {
            InitializeComponent();
            coordList = new List<Coord>();
            buttons = new BTN[4, 4];
            rnd = new Random();
            btnList = new List<BTN>();

            for(int i = 0; i < 4; i++)
                for(int j = 0; j < 4; j++)
                    coordList.Add(new Coord(i, j));

            for(int i = 0;i < 2; i++)
                for (int j = 0;j < 8; j++)
                {
                    int index = rnd.Next(coordList.Count());
                    buttons[coordList[index].X, coordList[index].Y] = new BTN
                    {
                        value = j,
                        FontSize = 50,
                    };
                    buttons[coordList[index].X, coordList[index].Y].Click += BTN_Click;

                    board.Children.Add(buttons[coordList[index].X, coordList[index].Y]);

                    Grid.SetRow(buttons[coordList[index].X, coordList[index].Y], coordList[index].X);
                    Grid.SetColumn(buttons[coordList[index].X, coordList[index].Y], coordList[index].Y);

                    coordList.RemoveAt(index);
                }
        }

        private void BTN_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as BTN;
            int index = btnList.FindIndex(b => b == btn);

            if (IsWin())
            {
                MessageBox.Show("You won!!!");
            }

            if(index == -1)
            {
                if (clickCount < 2)
                {
                    clickCount++;
                    btn.Content = btn.value.ToString();
                    btnList.Add(btn);
                }
                else
                {
                    btnList.ForEach(b => {
                        if (b.IsEnabled)
                            b.Content = null;});
                    btnList.Clear();
                    clickCount = 0;
                }
                BtnTheSame();
            } 
        }

        private void BtnTheSame()
        {
            if(btnList.Count == 2)
                if (btnList[0].value == btnList[1].value)
                {
                    btnList[0].IsEnabled = false;
                    btnList[1].IsEnabled = false;
                }
        }

        private bool IsWin()
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (buttons[i, j].IsEnabled)
                        return false;
            return true;
        }

    }
}
