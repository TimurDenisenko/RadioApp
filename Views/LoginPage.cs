using RadioApp.Models;

namespace RadioApp.Views;

public class LoginPage : ContentPage
{
    public LoginPage()
    {
        Background = new LinearGradientBrush
        {
            GradientStops =
            {
                new GradientStop { Color = Color.FromRgb(53, 47, 68), Offset = 0f },
                new GradientStop { Color = Color.FromRgb(92, 84, 112), Offset = 0.35f },
                new GradientStop { Color = Color.FromRgb(185, 180, 199), Offset = 0.75f },
                new GradientStop { Color = Color.FromRgb(250, 240, 230), Offset = 1 },
            }
        };
        Label title = new Label
        {
            Text = "My Account",
            FontSize = 35,
            TextColor = Colors.White,
            HeightRequest = 50,
            WidthRequest = 200,
            FontFamily = "Interstate",
        };
        Image userIcon = new Image
        {
            Source = FileManage.ConvertToImageSource(Properties.Resources.userIcon),
            HeightRequest = 80,
            WidthRequest = 80,
            ZIndex = 1,
        };
        BoxView entryBox = new BoxView
        {
            WidthRequest = 300,
            HeightRequest = 400,
            BackgroundColor = Color.FromRgb(250, 240, 230),
            CornerRadius = 5,
            ZIndex = -1,
        };
        Entry loginEntry = new Entry
        {
            WidthRequest = 270,
            HeightRequest = 50,
            FontFamily = "Interstate",
            FontSize = 30,
            Placeholder = "Login",
            PlaceholderColor = Colors.Gray,
            TextColor = Colors.Black,
            ZIndex = 2,
            BackgroundColor = Colors.Blue
        };
        AbsoluteLayout layout = new AbsoluteLayout
        {
            WidthRequest = 400,
            HeightRequest = 1800,
            Children = { userIcon, title, entryBox, loginEntry },
        };
        layout.SetLayoutBounds(title, new Rect(2, -300, layout.WidthRequest, layout.HeightRequest));
        layout.SetLayoutBounds(entryBox, new Rect(0, -20, layout.WidthRequest, layout.HeightRequest));
        layout.SetLayoutBounds(userIcon, new Rect(0, -160, layout.WidthRequest, layout.HeightRequest));
        layout.SetLayoutBounds(loginEntry, new Rect(0, -160, layout.WidthRequest, layout.HeightRequest));
        Content = layout;
    }
}