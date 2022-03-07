using final_pharmacy.Commands;
using final_pharmacy.DbHandler.DbServices;
using final_pharmacy.Services;
using final_pharmacy.Services.Navigations;
using final_pharmacy.ViewModels.Base;
using System.Windows;
using System.Windows.Input;

namespace final_pharmacy.ViewModels
{
    public class ChangePasswordViewModel : BaseViewModel
    {
        private INavigator navigator;
        public ChangePasswordViewModel(INavigator navigator)
        {
            this.navigator = navigator;
        }
        private string oldPassword;
        public string OldPassword
        {
            get { return oldPassword; }
            set
            {
                oldPassword = value;
                OnPropertyChanged("OldPassword");
            }
        }
        private string newpPassword;
        public string NewPassword
        {
            get { return newpPassword; }
            set
            {
                newpPassword = value;
                OnPropertyChanged("NewPassword");
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
                AuthLogDataContext context1 = new AuthLogDataContext();
                string LogedInUserName = context1.GetLogedInUser();
                string password = context.GetPassword(LogedInUserName);
                Validators validate = new Validators();
                if (string.IsNullOrEmpty(NewPassword) || !validate.IsValidPassword(NewPassword))
                {
                    MessageBox.Show("Invalid password pattern");
                }
                else if (OldPassword != password)
                {
                    MessageBox.Show("Wrong old password");
                }
                else
                {
                    context.ChangePassword(LogedInUserName, NewPassword);
                    navigator.CurrentViewModel = new AdminViewModel(navigator);
                }
            }
        }

        private bool CanSubmitExecute(object parameter)
        {
            if (string.IsNullOrEmpty(OldPassword) || string.IsNullOrEmpty(NewPassword))
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
            OldPassword = string.Empty;
            NewPassword = string.Empty;
        }
        private bool CanClearExecute(object parameter)
        {
            return true;
        }
    }
}
