using RadioApp.Models;

namespace RadioApp.Views
{
    public partial class App : Application
    {
        public const string DATABASE_NAME = "radio.db";
        private static Repository? database;
        public static Repository Database
        {
            get => database ??= new Repository(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME));
        }
        public App()
        {
            MainPage = new Shell
            {
                CurrentItem = new RadioPage()
            };
        }
    }
}