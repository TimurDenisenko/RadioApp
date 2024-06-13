
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace RadioApp.ViewModels
{
    public class UserListViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<UserListViewModel> Users { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;
        public ICommand CreateUserCommand { get; protected set; }
        public ICommand DeleteUserCommand { get; protected set; }
        public ICommand SaveUserCommand { get; protected set; }
        public ICommand BackCommand { get; protected set; }
        public INavigation Navigation { get; set; }
        public UserListViewModel()
        {
            Users = new ObservableCollection<UserListViewModel>();
            DeleteUserCommand = new Command(DeleteUser);
            SaveUserCommand = new Command(SaveUser);
            BackCommand = new Command(Back);
        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        private void Back() => Navigation.PopAsync();
        private void SaveUser(object user)
        {
            if (user is not UserListViewModel userModel || userModel == null || !userModel.IsValid || Users.Contains(userModel)) return;
            Users.Add(userModel);
            Back();
        }
        private void DeleteUser(object user)
        {
            if (user is not UserListViewModel userModel || userModel == null) return;
            Users.Remove(userModel);
            Back();
        }

    }
}
