using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using VideoLibrary;
using System.Net.Mail;
using System.Net;
using RadioApp.Views;
using RadioApp.ViewModels;
using System.Linq;

namespace RadioApp.Models
{
    public static class GeneralManager
    {
        private static char[] _symList = 
        { 
            'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p', 'a', 's', 'd', 'f',
            'g', 'h', 'j', 'k', 'l', 'z', 'x', 'c', 'v', 'b', 'n', 'm',
            'Q', 'W', 'E', 'R', 'T', 'Y', 'U', 'I', 'O', 'P', 'A', 'S', 'D', 'F',
            'G', 'H', 'J', 'K', 'L', 'Z', 'X', 'C', 'V', 'B', 'N', 'M',
            '1', '2', '3', '4', '5', '6', '7', '8', '9', '0'
        };
        //I don't want to complicate the pet project too much soo..
        private static byte[] globalSalt = [234, 21, 53, 231, 67, 21, 98, 74, 52, 125, 151, 182, 213, 149, 215, 43];
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
            RandomNumberGenerator.Create().GetBytes(salt);
            byte[] combinedSalt = new byte[salt.Length + globalSalt.Length];
            Buffer.BlockCopy(salt, 0, combinedSalt, 0, salt.Length);
            Buffer.BlockCopy(globalSalt, 0, combinedSalt, salt.Length, globalSalt.Length);
            return (salt, Hash(password, combinedSalt));
        }
        public static string Hash(string password, byte[] combinedSalt) =>
            Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: combinedSalt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 32));
        public static bool Verify(string name, string password)
        {
            UserViewModel user = (App.DatabaseUser.GetElements() as UserViewModel[]).Select(x => x.Name == name) as UserViewModel;
            string verifyHash = Hash(password, user.Salt.ToCharArray().Select(Convert.ToByte).ToArray());
            return verifyHash == user.Hash;

        }
        public static string SendCode(string email, bool forReg)
        {
            Random randomNumber = new Random();
            _symList = [.. _symList.OrderBy(x => randomNumber.Next())];
            string code = string.Join(string.Empty, _symList.Take(10));
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress("timur.denisenko.2006@gmail.com", "RadioApp");
            message.To.Add(new MailAddress(email));
            message.IsBodyHtml = true;
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            if (forReg)
            {
                message.Subject = "Registration code";
                message.Body = $"<p>Hello!<br><br>This is the code for creating an account in the RadioApp application.<br>If this is not you, then ignore this message<br><br>Code: {code} ";
            }
            else
            {
                message.Subject = "Password recovery code";
                message.Body = $"<p>Hello!<br><br>This is the code to restore your account in the RadioApp application.<br>If this is not you, then ignore this message<br><br>Code: {code} ";
            }
            smtp.Credentials = new NetworkCredential(message.From.Address, "pzwv zdfw ntlj okpw");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);
            return code;
        }
    }
}