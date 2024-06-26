
namespace RadioApp.Models
{
    public static class GeneralManager
    {
        public static ImageSource ConvertToImageSource(byte[] data) => ImageSource.FromStream(() => new MemoryStream(data));
    }
}