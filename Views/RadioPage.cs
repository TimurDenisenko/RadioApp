using CommunityToolkit.Maui.Views;
using VideoLibrary;
using RadioApp.Models;

namespace RadioApp.Views;

public class RadioPage : ContentPage
{
    ImageButton likeButton;
    bool like;
    public RadioPage()
    {
        //челики добавляют музыку
        //страница с любимым
        // ? отсортировывать 
        // можно остановить музыку нажав на картинку
        // светлая тема
        // связь с телеграммом 
        YouTubeVideo? video = GeneralManager.GetVideoUri("https://music.youtube.com/watch?v=9mf4umTvEZM&si=lgHoHf-eaCIK8Sme");
        Label title = new Label
        {
            Text = video?.Title ?? "None",
            FontSize = 25,
            TextColor = Colors.White,
            HorizontalOptions = LayoutOptions.Center,
        };
        MediaElement mediaElement = new MediaElement
        {
            Source = video?.Uri ?? "",
            WidthRequest = 400,
            HeightRequest = 300,
            ShouldShowPlaybackControls = false,
            Margin = new Thickness(0, 20, 0, 20)
        };
        mediaElement.Loaded += (s, e) => mediaElement.Play();
        mediaElement.Play();
        likeButton = new ImageButton
        {
            Source = GeneralManager.ConvertToImageSource(Properties.Resources.dislike),
            HeightRequest = 80,
            WidthRequest = 80,
        };
        likeButton.Clicked += LikeButton_Clicked;
        ImageButton back = new ImageButton
        {
            Source = GeneralManager.ConvertToImageSource(Properties.Resources.back),
            HeightRequest = 80,
            WidthRequest = 80,
            Margin = new Thickness(0, 0, 50, 0)
        };
        ImageButton forward = new ImageButton
        {
            Source = GeneralManager.ConvertToImageSource(Properties.Resources.forward),
            HeightRequest = 80,
            WidthRequest = 80,
            Margin = new Thickness(50, 0, 0, 0)
        };
        HorizontalStackLayout titleLayout = new HorizontalStackLayout
        {
            Children = { title },
            WidthRequest = 400,
            Padding = new Thickness(140, 0, 0, 0)
        };
        AbsoluteLayout actionLayout = new AbsoluteLayout
        {
            Children = { back, likeButton, forward },
            WidthRequest = 400,
        };
        actionLayout.SetLayoutBounds(back, new Rect(-120, 0, 400, 50));
        actionLayout.SetLayoutBounds(likeButton, new Rect(0, 0, 400, 50));
        actionLayout.SetLayoutBounds(forward, new Rect(120, 0, 400, 50));
        Content = new StackLayout
        {
            Children =
            {
                titleLayout,
                mediaElement,
                actionLayout,
            },
        };
        BackgroundColor = Colors.Black;
    }

    private async void LikeButton_Clicked(object? sender, EventArgs e)
    {
        like = !like;
        for (; likeButton.Opacity > 0.15; likeButton.Opacity -= 0.15)
        {
            await Task.Delay(1);
        }
        if (like)
            likeButton.Source = GeneralManager.ConvertToImageSource(Properties.Resources.like);
        else
            likeButton.Source = GeneralManager.ConvertToImageSource(Properties.Resources.dislike);
        for (int i = 0; i < 15; i++)
        {
            likeButton.TranslationX = i % 2 == 0 ? -1 : 1;
            likeButton.Opacity += 0.06333333333;
            likeButton.HeightRequest += 1;
            likeButton.WidthRequest += 1;
            await Task.Delay(1);
        }
        for (int i = 0; i < 15; i++)
        {
            likeButton.TranslationX = i % 2 == 0 ? -1 : 1;
            likeButton.HeightRequest -= 1;
            likeButton.WidthRequest -= 1;
            await Task.Delay(1);
        }
        likeButton.TranslationX = 0;
    }
}