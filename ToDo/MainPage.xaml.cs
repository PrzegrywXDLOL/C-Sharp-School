namespace ToDo
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();

            ToDo.toDos.Add(new ToDo("Zadanie 1", "Opis zadania 1"));
            ToDo.toDos.Add(new ToDo("Zadanie 2", "Opis zadania 2"));
            ToDo.toDos.Add(new ToDo("Zadanie 3", "Opis zadania 3"));

            MyList.ItemsSource = ToDo.toDos;
        }

        private void MyList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var todo = e.Item as ToDo;
            DisplayAlert("Details", $"{todo.Subject}\n{todo.Description}", "OK");
        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {
            var menuItem = (MenuItem)sender;
            var todo = menuItem.CommandParameter as ToDo;

            var index = ToDo.toDos.IndexOf(todo);
            if (index - 1 >= 0)
            {
                var up = ToDo.toDos[index - 1];
                ToDo.toDos[index] = up;
                ToDo.toDos[index-1] = todo;

                MyList.ItemsSource = null;
                MyList.ItemsSource = ToDo.toDos;
            }
        }

        private void MenuItem_Clicked_1(object sender, EventArgs e)
        {
            var menuItem = (MenuItem)sender;
            var todo = menuItem.CommandParameter as ToDo;

            var index = ToDo.toDos.IndexOf(todo);
            if (index <= ToDo.toDos.Count()-2)
            {
                var down = ToDo.toDos[index + 1];
                ToDo.toDos[index] = down;
                ToDo.toDos[index + 1] = todo;

                MyList.ItemsSource = null;
                MyList.ItemsSource = ToDo.toDos;
            }
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            var item = sender as ImageButton;
            var todo = item.CommandParameter as ToDo;

            ToDo.toDos.Remove(todo);
        }

        async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            await Navigation.PushAsync(new AddPage());
        }

        async void ImageButton_Clicked_1(object sender, EventArgs e)
        {
            var item = sender as ImageButton;
            var todo = item.CommandParameter as ToDo;

            await Navigation.PushAsync(new EditPage(todo));
        }

        private void MyList_Refreshing(object sender, EventArgs e)
        {
            MyList.ItemsSource = null;
            MyList.ItemsSource = ToDo.toDos;
        }
    }

}
