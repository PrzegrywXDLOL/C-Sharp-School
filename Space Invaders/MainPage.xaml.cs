using Microsoft.Maui.Layouts;

namespace Arkanoid {
    public partial class MainPage : ContentPage {

        List<BoxV> boxes;
        Random random;
        double x = 0.5, X;
        int second = 0;
        bool t = true;

        [Obsolete]
        public MainPage() 
        {
            InitializeComponent();
            boxes = new List<BoxV>();
            random = new Random();

            Device.StartTimer(new TimeSpan(0, 0, 0, 0, 500), () => 
            {
                Device.BeginInvokeOnMainThread(() => 
                {
                    foreach(var box in boxes) 
                    {
                        box.y += 0.05;
                        AbsoluteLayout.SetLayoutBounds(box, new Rect(box.x, box.y, 40, 40));
                    }
                    foreach(var box in boxes.Where(box => box.y > 1))
                        board.Children.Remove(box);
                    boxes.RemoveAll(box => box.y > 1);

                    second += 1;

                    if (second % 4 == 0) 
                    {
                        X = random.Next(0, 10) / 10.0f;
                        BoxV box = new BoxV(Math.Round(X, 1), 0);
                        box.Color = Colors.Red;
                        boxes.Add(box);

                        board.Children.Add(box);
                        AbsoluteLayout.SetLayoutBounds(box, new Rect(box.x, box.y, 40, 40));
                        AbsoluteLayout.SetLayoutFlags(box, AbsoluteLayoutFlags.PositionProportional);
                    }
                });
                return t;
            });
        }
        private async Task StopGame()
        {
            t = false;
            bool answer = await DisplayAlert("You Lose", "Would You like to play again?", "Yes", "No");
        }
        private void Left_Swiped(object sender, SwipedEventArgs e) 
        {   
            x -= 0.1;
            x = Math.Round(x, 1);
            if (x < 0) 
                x = 0;

            AbsoluteLayout.SetLayoutBounds(box, new Rect(x, 1, 40, 40));
        }

        private void Right_Swiped(object sender, SwipedEventArgs e) 
        {
            x += 0.1;
            x = Math.Round(x, 1);
            if (x > 1)
                x = 1;

            AbsoluteLayout.SetLayoutBounds(box, new Rect(x, 1, 40, 40));
        }

        private void TapGesture(object sender, TappedEventArgs e) 
        {
            var box = boxes.FirstOrDefault(b => b.x == x);

            if (box != null) 
            {
                boxes.Remove(box);
                board.Children.Remove(box);
            }    
        }
    }

}
