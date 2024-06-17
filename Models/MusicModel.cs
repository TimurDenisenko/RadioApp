using SQLite;

namespace RadioApp.Models
{
    [Table("Musics")]
    public class MusicModel
    {
        public int Id { get; set; }
        public string? Url { get; set; }
    }
}
