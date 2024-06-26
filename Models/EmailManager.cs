using System.Net.Mail;
using System.Net;

namespace RadioApp.Models
{
    public static class EmailManager
    {
        private static readonly Random randomNumber = new Random();
        private static readonly MailMessage message = new MailMessage
        {
            From = new MailAddress("timur.denisenko.2006@gmail.com", "RadioApp"),
            IsBodyHtml = true
        };
        private static readonly SmtpClient smtp = new SmtpClient
        {
            Port = 587,
            Host = "smtp.gmail.com",
            EnableSsl = true,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(message.From.Address, "pzwv zdfw ntlj okpw"),
            DeliveryMethod = SmtpDeliveryMethod.Network
        };
        private static char[] _symList =
        {
            'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p', 'a', 's', 'd', 'f',
            'g', 'h', 'j', 'k', 'l', 'z', 'x', 'c', 'v', 'b', 'n', 'm',
            'Q', 'W', 'E', 'R', 'T', 'Y', 'U', 'I', 'O', 'P', 'A', 'S', 'D', 'F',
            'G', 'H', 'J', 'K', 'L', 'Z', 'X', 'C', 'V', 'B', 'N', 'M',
            '1', '2', '3', '4', '5', '6', '7', '8', '9', '0'
        };
        public static string SendCode(string email, bool forReg)
        {
            _symList = [.. _symList.OrderBy(x => randomNumber.Next())];
            message.To.Add(new MailAddress(email));
            string code = string.Join(string.Empty, _symList.Take(10));
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
            smtp.Send(message);
            return code;
        }
    }
}
