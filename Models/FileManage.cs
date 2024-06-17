
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
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
        public static (byte[], string) HashPassword(string password)
        {
            byte[] salt = new byte[16];
            //I don't want to complicate the pet project too much soo..
            byte[] globalSalt = [234, 21, 53, 231, 67, 21, 98, 74, 52, 125, 151, 182, 213, 149, 215, 43];
            RandomNumberGenerator.Create().GetBytes(salt);
            byte[] combinedSalt = new byte[salt.Length + globalSalt.Length];
            Buffer.BlockCopy(salt, 0, combinedSalt, 0, salt.Length);
            Buffer.BlockCopy(globalSalt, 0, combinedSalt, salt.Length, globalSalt.Length);
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: combinedSalt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 32));
            return (salt, hashed);
        }
    }
}