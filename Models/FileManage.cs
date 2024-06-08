
using VideoLibrary;

namespace RadioApp.Models
{
    public static class FileManage
    {
        public static YouTubeVideo? GetVideoUri(string url)
        {
            try
            {
               return YouTube.Default.GetVideo(url.Split("&si=")[0].Replace("music.", "www.").Replace(".com", ".com/embed").Replace("watch?v=", "") + ".mp4");
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static ImageSource ConvertToImageSource(byte[] data) => ImageSource.FromStream(() => new MemoryStream(data));
    }
}