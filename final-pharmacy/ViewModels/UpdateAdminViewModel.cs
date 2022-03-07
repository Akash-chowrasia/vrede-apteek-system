using final_pharmacy.Commands;
using final_pharmacy.DbHandler.DbModel;
using final_pharmacy.DbHandler.DbServices;
using final_pharmacy.Services;
using final_pharmacy.Services.Navigations;
using final_pharmacy.ViewModels.Base;
using System.Windows;
using System.Windows.Input;

namespace final_pharmacy.ViewModels
{
    public class UpdateAdminViewModel : BaseViewModel
    {
        private INavigator navigator;
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
        private bool isActive;
        public bool IsActive
        {
            get { return isActive; }
            set
            {
                isActive = value;
                OnPropertyChanged("IsActive");
            }
        }
        public UpdateAdminViewModel(INavigator navigator, User user)
        {
            this.navigator = navigator;
            UserName = user.UserName;
            Email = user.Email;
            IsActive = user.IsActive;
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
                if (string.IsNullOrEmpty(Email) || !validate.IsValidEmail(Email))
                {
                    MessageBox.Show("Invalid email");
                }
                else
                {
                    context.UpdateUser(UserName, Email, IsActive);
                    navigator.CurrentViewModel = new AdminViewModel(navigator);
                }
            }
        }

        private bool CanSubmitExecute(object parameter)
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Email))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private ICommand _CancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                if (_CancelCommand == null)
                {
                    _CancelCommand = new RelayCommand(CancelExecute, CanCancelExecute);
                }
                return _CancelCommand;
            }
        }
        private void CancelExecute(object parameter)
        {
            navigator.CurrentViewModel = new AdminViewModel(navigator);
        }
        private bool CanCancelExecute(object parameter)
        {
            return true;
        }
    }
}
