using SQLite;

namespace RadioApp.Models
{
    public class Repository
    {
        private SQLiteConnection database;
        public Repository(string databasePath)
        {
            database = new SQLiteConnection(databasePath);
        }
    }
}