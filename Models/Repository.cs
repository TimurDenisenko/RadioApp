using SQLite;

namespace RadioApp.Models
{
    public class Repository
    {
        private SQLiteConnection database;
        internal Repository(string databasePath, object model)
        {
            database = new SQLiteConnection(databasePath);
            if (model is UserModel)
                database.CreateTable<UserModel>();
            else if (model is MusicModel)
                database.CreateTable<MusicModel>();
        }
        public Array? GetElemenets<T>()
        {
            if (typeof(T) == typeof(UserModel))
                return database.Table<UserModel>().ToArray();
            else if (typeof(T) == typeof(MusicModel))
                return database.Table<MusicModel>().ToArray();
            return null;
        }
        public object? GetElemenet<T>(int id)
        {
            if (typeof(T) == typeof(UserModel))
                return database.Get<UserModel>(id);
            else if (typeof(T) == typeof(MusicModel))
                return database.Get<MusicModel>(id);
            return null;
        }
        public void DeleteElement<T>(int id)
        {
            if (typeof(T) == typeof(UserModel))
                database.Delete<UserModel>(id);
            else if (typeof(T) == typeof(MusicModel))
                database.Delete<MusicModel>(id);
        }
        public int SaveElement(object element)
        {
            if (element is UserModel userModel)
            {
                if (userModel.Id != 0)
                {
                    database.Update(userModel);
                    return userModel.Id;
                }
                return database.Insert(userModel);
            }    
            else if (element is MusicModel musicModel)
            {
                if (musicModel.Id != 0)
                {
                    database.Update(musicModel);
                    return musicModel.Id;
                }
                return database.Insert(musicModel);
                }
            return 0;
        }
    }
}