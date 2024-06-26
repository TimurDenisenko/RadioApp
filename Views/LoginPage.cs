using RadioApp.Models;
using RadioApp.ViewModels;
using System.Text.RegularExpressions;

namespace RadioApp.Views;

public class LoginPage : ContentPage
{
    private readonly Button loginButton;
    private readonly Entry loginEntry, emailEntry, passwordEntry;
    private readonly Switch passVisible;
    private readonly AbsoluteLayout layout;
    private string code, name;
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
        App.DatabaseUser.Clear();
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
            Source = GeneralManager.ConvertToImageSource(Properties.Resources.userIcon),
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
        loginEntry = new Entry
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
        emailEntry = new Entry
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
        passwordEntry = new Entry
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
        passVisible = new Switch
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
        loginButton = new Button
        {
            BackgroundColor = Color.FromRgb(53, 47, 68),
            TextColor = Color.FromRgb(250, 240, 230),
            Text = "Sign in",
            WidthRequest = 270,
            HeightRequest = 50,
            FontFamily = "Interstate",
            FontSize = 23,
        };
        loginButton.Clicked += SignIn_Event;
        Button back = new Button
        {
            Text = "Back",
            FontFamily = "Interstate",
            FontSize = 17,
            BackgroundColor = Colors.Transparent,
            TextColor = Colors.Gray,
            WidthRequest = 100,
            HeightRequest = 50,
            IsVisible = false,
        };
        layout = new AbsoluteLayout
        {
            WidthRequest = 400,
            HeightRequest = 1800,
            Children = { userIcon, title, entryBox, loginEntry, passwordEntry, passVisible, forgetPass, loginButton, signUp, emailEntry, back },
        };
        back.Clicked += (s, e) =>
        {
            loginEntry.MaxLength = 15;
            loginEntry.Placeholder = "Login";
            loginButton.Text = "Sign in";
            passwordEntry.IsVisible = true;
            forgetPass.IsVisible = true;   
            passVisible.IsVisible = true;
            back.IsVisible = false;
            loginButton.Clicked -= SendCode_Event;
            loginButton.Clicked -= RestorePass_Event;
            loginButton.Clicked += SignIn_Event;
        };
        forgetPass.Clicked += (s, e) =>
        {
            loginEntry.MaxLength = 50;
            loginButton.Clicked -= SignIn_Event;
            loginButton.Clicked += SendCode_Event;
            loginEntry.Placeholder = "Email";
            loginButton.Text = "Send an email";
            passwordEntry.IsVisible = false;
            forgetPass.IsVisible = false;
            passVisible.IsVisible = false;
            back.IsVisible = true;
        };
        signUp.Clicked += (sender, args) =>
        {
            loginEntry.MaxLength = 15;
            loginButton.IsVisible = true;
            passVisible.IsVisible = true;
            passwordEntry.IsVisible = true;
            back.IsVisible = false;
            loginEntry.Placeholder = "Login";
            if (emailEntry.IsVisible) 
            {
                layout.SetLayoutBounds(passwordEntry, new Rect(-30, -10, layout.WidthRequest, layout.HeightRequest));
                layout.SetLayoutBounds(passVisible, new Rect(90, -10, layout.WidthRequest, layout.HeightRequest));
                signUp.Text = "Sign Up";
                loginButton.Text = "Sign in";
                emailEntry.IsVisible = false;
                forgetPass.IsVisible = true;
                loginButton.Clicked += SignIn_Event;
                loginButton.Clicked -= SendCode_Event;
                loginButton.Clicked -= RestorePass_Event;
                loginButton.Clicked -= SignUp_Event;
                loginButton.Clicked -= CheckAccCode_Event;
            }
            else
            {
                layout.SetLayoutBounds(passwordEntry, new Rect(-30, 30, layout.WidthRequest, layout.HeightRequest));
                layout.SetLayoutBounds(passVisible, new Rect(90, 30, layout.WidthRequest, layout.HeightRequest));
                signUp.Text = "Sign In";
                loginButton.Text = "Sign up";
                emailEntry.IsVisible = true;
                forgetPass.IsVisible = false;
                loginButton.Clicked -= SignIn_Event;
                loginButton.Clicked -= SendCode_Event;
                loginButton.Clicked -= RestorePass_Event;
                loginButton.Clicked -= CheckAccCode_Event;
                loginButton.Clicked += SignUp_Event;
            }
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
        layout.SetLayoutBounds(back, new Rect(-110, -190, layout.WidthRequest, layout.HeightRequest));
        Content = layout;
    }
    private async void SendCode_Event(object? sender, EventArgs e)
    {
        try
        {
            code = GeneralManager.SendCode(loginEntry.Text, false);
        }
        catch (Exception)
        {
            await DisplayAlert("Error", "Empty or incorrect email entered", "Cancel");
            return;
        }
        loginButton.Clicked -= SendCode_Event;
        loginEntry.Placeholder = "Code";
        loginButton.Text = "Check a code";
        loginButton.Clicked += RestorePass_Event;
    }

    private async void RestorePass_Event(object? sender, EventArgs e)
    {
        try
        {
            if (code != loginEntry.Text)
            {
                await DisplayAlert("Error", "Incorrect code entered", "Cancel");
                return;
            }
            loginEntry.IsVisible = false;
            layout.SetLayoutBounds(passwordEntry, new Rect(-30, -10, layout.WidthRequest, layout.HeightRequest));
            App.DatabaseUser.SaveElement(((App.DatabaseUser.GetElements() as UserViewModel[]).Select(x => x.Email == loginEntry.Text) as UserViewModel));
        }
        catch (Exception)
        {
            await DisplayAlert("Error", "Fill in the code field", "Cancel");
        }
    }
    private async void SignUp_Event(object? sender, EventArgs e)
    {
        try
        {
            if (loginEntry.Text.Length < 4)
            {
                await DisplayAlert("Error", "Login is too short", "Cancel");
                return;
            }
            else if (!loginEntry.Text.All(char.IsAsciiLetter))
            {
                await DisplayAlert("Error", "The name must consist of letters only", "Cancel");
                return;
            }
            try
            {
                if ((App.DatabaseUser.GetElements() as UserViewModel[]).Select(x => x.Name).Contains(loginEntry.Text))
                {
                    await DisplayAlert("Error", "The user with this name is already registered", "Cancel");
                    return;
                }
            }
            catch (Exception) { }
            if (emailEntry.Text.Length < 10)
            {
                await DisplayAlert("Error", "Incorrect email entered or data missing", "Cancel");
                return;
            }
            else if (!Regex.IsMatch(emailEntry.Text, "(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|\"(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21\\x23-\\x5b\\x5d-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])*\")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\\[(?:(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9]))\\.){3}(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9])|[a-z0-9-]*[a-z0-9]:(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21-\\x5a\\x53-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])+)\\])"))
            {
                await DisplayAlert("Error", "Invalid email entered", "Cancel");
                return;
            }
            try
            {
                if ((App.DatabaseUser.GetElements() as UserViewModel[]).Select(x => x.Email).Contains(loginEntry.Text))
                {
                    await DisplayAlert("Error", "The user with this email is already registered", "Cancel");
                    return;
                }
            }
            catch (Exception) { }
            if (passwordEntry.Text.Length < 4)
            {
                await DisplayAlert("Error", "Too short password", "Cancel");
                return;
            }
            else if (!passwordEntry.Text.All(char.IsAsciiLetterOrDigit))
            {
                await DisplayAlert("Error", "The password must consist only of Latin letters or numbers", "Cancel");
                return;
            }
            try
            {
                code = GeneralManager.SendCode(emailEntry.Text, true);
            }
            catch (Exception)
            {
                await DisplayAlert("Error", "Failed to send registration code, check the email field", "Cancel");
                return;
            }
            passwordEntry.IsVisible = false;
            passVisible.IsVisible = false;
            emailEntry.IsVisible = false;
            loginButton.Clicked -= SignUp_Event;
            loginButton.Clicked += CheckAccCode_Event;
            loginEntry.Placeholder = "Code";
            name = loginEntry.Text;
            loginEntry.Text = string.Empty;
            loginButton.Text = "Check a code";
        }
        catch (Exception)
        {
            await DisplayAlert("Error", "Something went wrong, check that your data entered is correct and be sure to fill in all fields", "Cancel");
        }
    }

    private async void CheckAccCode_Event(object? sender, EventArgs e)
    {
        try
        {
            if (code != loginEntry.Text)
            {
                await DisplayAlert("Error", "Incorrect code entered", "Cancel");
                return;
            }
            App.DatabaseUser.SaveElement(new UserModel(name, emailEntry.Text, passwordEntry.Text));
            await Navigation.PushAsync(new RadioPage());
        }
        catch (Exception)
        {
            await DisplayAlert("Error", "Data input awaited", "Cancel");
        }
    }

    private async void SignIn_Event(object? sender, EventArgs e)
    {
        try
        {
            if (!GeneralManager.Verify(loginEntry.Text, passwordEntry.Text))
            {
                await DisplayAlert("Error", "Incorrect password or login entered", "Cancel");
                return;
            }
            await Navigation.PushAsync(new RadioPage());
        }
        catch (Exception)
        {
            await DisplayAlert("Error", "Data input awaited", "Cancel");
        }
    }
}