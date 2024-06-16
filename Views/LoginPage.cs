namespace RadioApp.Views;

public class LoginPage : ContentPage
{
	public LoginPage()
    {
        BackgroundColor = Colors.Black;
        Label title = new Label
		{
			Text = "My account",
            FontSize = 25,
            TextColor = Colors.White,
        };
        AbsoluteLayout layout = new AbsoluteLayout
        {
            WidthRequest = 400,
            HeightRequest = 1800,
            Children = { title },
        };
        layout.SetLayoutBounds(title, new Rect(150, 50, layout.WidthRequest, layout.HeightRequest));
        Content = layout;
    }
}