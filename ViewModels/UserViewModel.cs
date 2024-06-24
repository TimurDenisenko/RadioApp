﻿
using RadioApp.Models;
using System.ComponentModel;

namespace RadioApp.ViewModels
{
    public class UserViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        UserListViewModel? ulvm;
        public UserModel User { get; set; }
        public UserViewModel()
        {
            User = new UserModel();
        }
        public UserViewModel(string name, string email, string password)
        {
            (byte[], string) pass = GeneralManager.HashPassword(password);
            User = new UserModel(name, email, pass.Item1, pass.Item2);
        }
        public UserListViewModel? UsersListViewModel
        {
            get => ulvm;
            set
            {
                if (ulvm == value) return;
                ulvm = value;
                OnPropertyChanged("UserListViewModel");
            }
        }
        public string? Name
        {
            get => User.Name;
            set
            {
                if (User.Name == value) return;
                User.Name = value;
                OnPropertyChanged("Name");
            }
        }
        public string? Email
        {
            get => User.Email;
            set
            {
                if (User.Email == value) return;
                User.Email = value;
                OnPropertyChanged("Email");
            }
        }
        public string? Salt
        {
            get => User.Salt;
            set
            {
                if (User.Salt == value) return;
                User.Salt = value;
                OnPropertyChanged("Salt");
            }
        }
        public string? Hash
        {
            get => User.Hash;
            set
            {
                if (User.Hash == value) return;
                User.Hash = value;
                OnPropertyChanged("Hash");
            }
        }
        public bool IsAdmin
        {
            get => User.IsAdmin;
            set
            {
                if (User.IsAdmin == value) return;
                User.IsAdmin = value;
                OnPropertyChanged("IsAdmin");
            }
        }
        public bool IsValid
        {
            get
            {
                return new string?[] { Name, Email, Salt, Hash }.Any(x => !string.IsNullOrEmpty(x?.Trim()));
            }
        }

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
