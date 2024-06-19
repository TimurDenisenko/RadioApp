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
            FontSize = 23,
            Placeholder = "Login",
            PlaceholderColor = Colors.Gray,
            TextColor = Colors.Black,
            MaxLength = 15,
        };
        Entry emailEntry = new Entry
        {
            WidthRequest = 270,
            HeightRequest = 50,
            FontFamily = "Interstate",
            FontSize = 23,
            Placeholder = "Email",
            PlaceholderColor = Colors.Gray,
            TextColor = Colors.Black,
            MaxLength = 50,
            IsVisible = false,
        };
        Entry passwordEntry = new Entry
        {
            WidthRequest = 212,
            HeightRequest = 50,
            FontFamily = "Interstate",
            FontSize = 23,
            Placeholder = "Password",
            PlaceholderColor = Colors.Gray,
            TextColor = Colors.Black,
            MaxLength = 20,
            IsPassword = true,
        };
        Switch passVisible = new Switch
        {
            WidthRequest = 100,
            HeightRequest = 50,
            OnColor = Color.FromRgb(53, 47, 68),
            ThumbColor = Color.FromRgb(53, 47, 68),
        };
        passVisible.Toggled += (sender, args) => passwordEntry.IsPassword = !passwordEntry.IsPassword;
        Button forgetPass = new Button
        {
            BackgroundColor = Colors.Transparent,
            TextColor = Colors.Gray,
            Text = "Forgot password?",
            WidthRequest = 175,
            HeightRequest = 50,
            FontFamily = "Interstate",
            FontSize = 17,
        };
        Button signUp = new Button
        {
            BackgroundColor = Colors.Transparent,
            TextColor = Color.FromRgb(53, 47, 68),
            Text = "Sign Up",
            WidthRequest = 175,
            HeightRequest = 50,
            FontFamily = "Interstate",
            FontSize = 23,
        };
        Button loginButton = new Button
        {
            BackgroundColor = Color.FromRgb(53, 47, 68),
            TextColor = Color.FromRgb(250, 240, 230),
            Text = "Sign in",
            WidthRequest = 270,
            HeightRequest = 50,
            FontFamily = "Interstate",
            FontSize = 23,
        };
        AbsoluteLayout layout = new AbsoluteLayout
        {
            WidthRequest = 400,
            HeightRequest = 1800,
            Children = { userIcon, title, entryBox, loginEntry, passwordEntry, passVisible, forgetPass, loginButton, signUp, emailEntry },
        };
        signUp.Clicked += (sender, args) =>
        {
            if (emailEntry.IsVisible)
            {
                layout.SetLayoutBounds(passwordEntry, new Rect(-30, -10, layout.WidthRequest, layout.HeightRequest));
                layout.SetLayoutBounds(passVisible, new Rect(90, -10, layout.WidthRequest, layout.HeightRequest));
                layout.SetLayoutBounds(forgetPass, new Rect(-65, 50, layout.WidthRequest, layout.HeightRequest));
                signUp.Text = "Sign Up";
                loginButton.Text = "Sign in";
            }
            else
            {
                layout.SetLayoutBounds(passwordEntry, new Rect(-30, 30, layout.WidthRequest, layout.HeightRequest));
                layout.SetLayoutBounds(passVisible, new Rect(90, 30, layout.WidthRequest, layout.HeightRequest));
                layout.SetLayoutBounds(forgetPass, new Rect(-65, 80, layout.WidthRequest, layout.HeightRequest));
                signUp.Text = "Sign In";
                loginButton.Text = "Sign up";
            }
            emailEntry.IsVisible = !emailEntry.IsVisible;
            forgetPass.IsVisible = !forgetPass.IsVisible;
        };
        layout.SetLayoutBounds(title, new Rect(2, -300, layout.WidthRequest, layout.HeightRequest));
        layout.SetLayoutBounds(entryBox, new Rect(0, -20, layout.WidthRequest, layout.HeightRequest));
        layout.SetLayoutBounds(userIcon, new Rect(0, -160, layout.WidthRequest, layout.HeightRequest));
        layout.SetLayoutBounds(loginEntry, new Rect(0, -90, layout.WidthRequest, layout.HeightRequest));
        layout.SetLayoutBounds(passwordEntry, new Rect(-30, -10, layout.WidthRequest, layout.HeightRequest));
        layout.SetLayoutBounds(emailEntry, new Rect(0, -30, layout.WidthRequest, layout.HeightRequest));
        layout.SetLayoutBounds(passVisible, new Rect(90, -10, layout.WidthRequest, layout.HeightRequest));
        layout.SetLayoutBounds(forgetPass, new Rect(-65, 50, layout.WidthRequest, layout.HeightRequest));
        layout.SetLayoutBounds(loginButton, new Rect(0, 130, layout.WidthRequest, layout.HeightRequest));
        layout.SetLayoutBounds(signUp, new Rect(0, 360, layout.WidthRequest, layout.HeightRequest));
        Content = layout;
    }
}