using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace RadioApp.ViewModels
{
    public class MusicListViewModel
    {
        public ObservableCollection<MusicViewModel> Musics { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;
        public ICommand CreateMusicCommand { get; protected set; }
        public ICommand DeleteMusicCommand { get; protected set; }
        public ICommand SaveMusicCommand { get; protected set; }
        public ICommand BackCommand { get; protected set; }
        public INavigation Navigation { get; set; }
        public MusicListViewModel()
        {
            Musics = new ObservableCollection<MusicViewModel>();
            DeleteMusicCommand = new Command(DeleteMusic);
            SaveMusicCommand = new Command(SaveMusic);
            BackCommand = new Command(Back);
        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        private void Back() => Navigation.PopAsync();
        private void SaveMusic(object Music)
        {
            if (Music is not MusicViewModel MusicModel || MusicModel == null || !MusicModel.IsValid || Musics.Contains(MusicModel)) return;
            Musics.Add(MusicModel);
            Back();
        }
        private void DeleteMusic(object Music)
        {
            if (Music is not MusicViewModel MusicModel || MusicModel == null) return;
            Musics.Remove(MusicModel);
            Back();
        }
    }
}
