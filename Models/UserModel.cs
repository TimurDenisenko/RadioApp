using SQLite;

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
        public UserModel(string name, string email, byte[] salt, string hash)
        {
            Name = name;
            Email = email;
            Salt = salt.ToString();
            Hash = hash;
            IsAdmin = false;
        }
    }
}
