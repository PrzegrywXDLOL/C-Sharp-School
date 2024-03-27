namespace ToDo5f;

public partial class NewPage3 : ContentPage
{
	public NewPage3()
	{
		InitializeComponent();
        myList.ItemsSource = ToDo.toDosFinish;
    }

    private void ImageButton_Clicked(object sender, EventArgs e)
    {
		var item = sender as ImageButton;
		var todo = item.CommandParameter as ToDo;

		ToDo.toDos.Add(new ToDo(todo.Subject, todo.Description));
		ToDo.toDosFinish.Remove(todo);
    }
}