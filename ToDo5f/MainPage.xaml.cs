namespace ToDo5f
{
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();

            ToDo.toDos.Add(new ToDo("zadanie1", "opis zadania 1"));
            ToDo.toDos.Add(new ToDo("zadanie2", "opis zadania 2"));
            ToDo.toDos.Add(new ToDo("zadanie3", "opis zadania 3"));

            myList.ItemsSource = ToDo.toDos;
        }

        private void myList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var todo = e.Item as ToDo;
            DisplayAlert("Details", $"{todo.Subject}\n{todo.Description}", "OK");
        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {
            var menuItem = (MenuItem)sender;
            var todo = menuItem.CommandParameter as ToDo;

            var index = ToDo.toDos.IndexOf(todo);
            if(index - 1 >= 0)
            { 
                var up = ToDo.toDos[index - 1];
                ToDo.toDos[index] = up;
                ToDo.toDos[index-1] = todo;

                myList.ItemsSource = null;
                myList.ItemsSource = ToDo.toDos;
            }
        }

        private void MenuItem_Clicked_1(object sender, EventArgs e)
        {
            var menuItem = (MenuItem)sender;
            var todo = menuItem.CommandParameter as ToDo;

            var index = ToDo.toDos.IndexOf(todo);
            if (index <= ToDo.toDos.Count()-2)
            {
                var up = ToDo.toDos[index + 1];
                ToDo.toDos[index] = up;
                ToDo.toDos[index + 1] = todo;

                myList.ItemsSource = null;
                myList.ItemsSource = ToDo.toDos;
            }
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            var item = sender as ImageButton;
            var todo = item.CommandParameter as ToDo;

            ToDo.toDosFinish.Add(new ToDo(todo.Subject, todo.Description));
            ToDo.toDos.Remove(todo);
        }

        async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            await Navigation.PushAsync(new NewPage1());
        }

        private async void ImageButton_Clicked_1(object sender, EventArgs e)
        {
            var item = sender as ImageButton;
            var todo = item.CommandParameter as ToDo;

            await Navigation.PushAsync(new NewPage2(todo));
        }

        private void myList_Refreshing(object sender, EventArgs e)
        {
            myList.ItemsSource = null;
            myList.ItemsSource = ToDo.toDos;
        }
    }

}
