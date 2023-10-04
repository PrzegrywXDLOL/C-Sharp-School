using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Layout
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Button> buttons;
        Random random;
        int count;
        int[] tab;
        DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();

            Decide decide = new Decide();
            decide.ShowDialog();

            int.TryParse(decide.number.Text.ToString(), out int btn);

            buttons = new List<Button>();
            random = new Random();
            timer = new DispatcherTimer();

            CreateBtn(btn);
        }

        private void CreateBtn(int btn)
        {
            buttons.Clear();
            uniform.Children.Clear();

            for (int i = 0; i < btn; i++)
            {
                buttons.Add(new Button());
                buttons[i].Content = random.Next(10).ToString();
                buttons[i].FontSize = 50;

                buttons[i].Click += leftClick;
                buttons[i].MouseRightButtonDown += rightClick;

                uniform.Children.Add(buttons[i]);
            }
            progress.Value = SortPercent();
        }

        private void leftClick(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            button.Content = random.Next(10).ToString();
            progress.Value = SortPercent();
        }

        private void rightClick(object sender, MouseButtonEventArgs e)
        {
            foreach (var item in buttons) 
            {
                item.Content = random.Next(10).ToString();
            }
            progress.Value = SortPercent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            count = 0;
            text.Text = string.Empty;

            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += Timer_Tick;
            timer.Start();

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (IsSort())
            {
                timer.Stop();
                MessageBox.Show("Number of attempts: " + count);
                Decide decide1 = new Decide();
                decide1.ShowDialog();
                int.TryParse(decide1.number.Text.ToString(), out int btn);
                CreateBtn(btn);
            }
            else
            {
                count++;
                for (int i = 1; i < buttons.Count; i++)
                {
                    if (int.Parse(buttons[i - 1].Content.ToString()) > int.Parse(buttons[i].Content.ToString()))
                    {
                        var temp = buttons[i - 1].Content.ToString();
                        buttons[i - 1].Content = buttons[i].Content.ToString();
                        buttons[i].Content = temp;
                    } 
                }

                text.Text += count.ToString() + ". ";
                foreach (var item in buttons)
                    text.Text += item.Content.ToString();
                text.Text += " " + SortPercent() + "%" + "\n";
                scroll.ScrollToBottom();
                progress.Value = SortPercent();
            }
        }

        private bool IsSort()
        {
            bool sort = true;

            for(int i = 1; i < buttons.Count; i++)
                if (int.Parse(buttons[i].Content.ToString())
                    < int.Parse(buttons[i - 1].Content.ToString()))
                {
                    sort = false;
                    break;
                }

            return sort;
        }

        private int SortPercent()
        {
            int maxval = 0;
            float sum = 0, avg;
            
            foreach (var item in buttons)
                if (int.Parse(item.Content.ToString()) >= maxval)
                {
                    sum++;
                    maxval = int.Parse(item.Content.ToString());
                }
            
            avg = (sum / buttons.Count) * 100;

            return (int)avg;
        }
    }
}
