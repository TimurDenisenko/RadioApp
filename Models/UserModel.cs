using SQLite;
using System.Text;

namespace RadioApp.Models
{
    [Table("Users")]
    public class UserModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Salt { get; set; }
        public string? Hash { get; set; }
        public bool IsAdmin { get; set; }
        public UserModel() { }
        public UserModel(string name, string email, string password)
        {
            (byte[], string) pass = GeneralManager.HashPassword(password);
            Name = name;
            Email = email;
            Salt = Encoding.ASCII.GetString(pass.Item1);
            Hash = pass.Item2;
            IsAdmin = false;
        }
    }
}
