using final_pharmacy.Commands;
using final_pharmacy.DbHandler.DbServices;
using final_pharmacy.Services;
using final_pharmacy.Services.Navigations;
using final_pharmacy.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace final_pharmacy.ViewModels
{
    public class NewAdminViewModel : BaseViewModel
    {
        private INavigator navigator;
        public NewAdminViewModel(INavigator navigator)
        {
            this.navigator = navigator;
        }
        private string userName;
        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                OnPropertyChanged("UserName");
            }
        }
        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value.ToLower();
                OnPropertyChanged("Email");
            }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }
        private ICommand _SubmitCommand;
        public ICommand SubmitCommand
        {
            get
            {
                if (_SubmitCommand == null)
                {
                    _SubmitCommand = new RelayCommand(SubmitExecute, CanSubmitExecute);
                }
                return _SubmitCommand;
            }
        }

        private void SubmitExecute(object parameter)
        {
            using (UserDataContext context = new UserDataContext())
            {
                Validators validate = new Validators();
                if (string.IsNullOrEmpty(UserName) || !validate.IsAlphabetOnly(UserName))
                {
                    MessageBox.Show("use Alphabets only for username");
                }
                else if (string.IsNullOrEmpty(Email)  || !validate.IsValidEmail(Email))
                {
                    MessageBox.Show("Invalid email");
                }
                else if (string.IsNullOrEmpty(Password))// || !validate.IsValidPassword(Password))
                {
                    MessageBox.Show("Invalid password pattern");
                }
                else
                {
                    context.AddUser(UserName, Password, Email);
                    navigator.CurrentViewModel = new AdminViewModel(navigator);
                }
            }
        }

        private bool CanSubmitExecute(object parameter)
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Email))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private ICommand _ClearCommand;
        public ICommand ClearCommand
        {
            get
            {
                if (_ClearCommand == null)
                {
                    _ClearCommand = new RelayCommand(ClearExecute, CanClearExecute);
                }
                return _ClearCommand;
            }
        }
        private void ClearExecute(object parameter)
        {
            UserName = string.Empty;
            Password = string.Empty;
            Email = string.Empty;
        }
        private bool CanClearExecute(object parameter)
        {
            return true;
        }

        private ICommand _PasswordChangedCommand;
        public ICommand PasswordChangedCommand
        {
            get
            {
                if (_PasswordChangedCommand == null)
                {
                    _PasswordChangedCommand = new RelayCommand(PasswordChangedExecute, CanPasswordChangedExecute);
                }
                return _ClearCommand;
            }
        }
        private void PasswordChangedExecute(object parameter)
        {
            Password = ((PasswordBox)parameter).Password;
            MessageBox.Show(Password);
        }
        private bool CanPasswordChangedExecute(object parameter)
        {
            return true;
        }
    }
}
