using RadioApp.Models;

namespace RadioApp.Views
{
    public partial class App : Application
    {
        public const string DATABASE_NAME_RADIO = "radio.db";
        private static Repository? databaseRadio;
        public static Repository DatabaseRadio
        {
            get => databaseRadio ??= new Repository(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME_RADIO));
        }
        public const string DATABASE_NAME_USER = "user.db";
        private static Repository? databaseUser;
        public static Repository DatabaseUser
        {
            get => databaseUser ??= new Repository(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME_USER));
        }
        public App()
        {
            MainPage = new Shell
            {
                CurrentItem = new LoginPage()
            };
        }
    }
}