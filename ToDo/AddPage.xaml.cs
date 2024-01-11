namespace ToDo;

public partial class AddPage : ContentPage
{
	public AddPage()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
		ToDo.toDos.Add(new ToDo(subjectEntry.Text.ToString(), descriptionEditor.Text.ToString()));

		await Navigation.PopAsync();
    }

    private async void SwipeGestureRecognizer_Swiped(object sender, SwipedEventArgs e)
    {
        await Navigation.PopAsync();
    }
}