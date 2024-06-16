
using RadioApp.Models;
using System.ComponentModel;

namespace RadioApp.ViewModels
{
    public class MusicViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        MusicListViewModel? ulvm;
        public MusicModel Music { get; set; }
        public MusicViewModel()
        {
            Music = new MusicModel();
        }
        public MusicListViewModel? MusicsListViewModel
        {
            get => ulvm;
            set
            {
                if (ulvm == value) return;
                ulvm = value;
                OnPropertyChanged("MusicListViewModel");
            }
        }
        public string? Url
        {
            get => Music.Url;
            set
            {
                if (Music.Url == value) return;
                Music.Url = value;
                OnPropertyChanged("Url");
            }
        }
        public bool IsValid
        {
            get
            {
                return new string?[] { Url }.Any(x => !string.IsNullOrEmpty(x?.Trim()));
            }
        }

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
