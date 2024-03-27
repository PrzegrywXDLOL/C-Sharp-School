namespace ToDo5f;

public partial class NewPage1 : ContentPage
{
	public NewPage1()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
		if (subjectEntry.Text != null && descroptionEditor.Text != null)
			ToDo.toDos.Add(
				new ToDo(subjectEntry.Text.ToString(), descroptionEditor.Text.ToString())
				);
		subjectEntry.Text = null;
		descroptionEditor.Text = null;
		await Navigation.PopAsync();
    }

    private async void SwipeGestureRecognizer_Swiped(object sender, SwipedEventArgs e)
    {
        await Navigation.PopAsync();
    }
}