using final_pharmacy.Commands;
using final_pharmacy.DbHandler.DbServices;
using final_pharmacy.Services;
using final_pharmacy.Views;
using System.Windows;
using System.Windows.Input;

namespace final_pharmacy.ViewModels
{
    public class NewUserViewModel : NotifyPropertyChanged
    {
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
        private Visibility mNewUserWindowHide = Visibility.Visible;
        public Visibility NewUserWindowHide
        {
            get { return mNewUserWindowHide; }
            set
            {
                mNewUserWindowHide = value;
                OnPropertyChanged(nameof(NewUserWindowHide));
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
                else if (string.IsNullOrEmpty(Email) || !validate.IsValidEmail(Email))
                {
                    MessageBox.Show("Invalid email");
                }
                else if (string.IsNullOrEmpty(Password) || !validate.IsValidPassword(Password))
                {
                    MessageBox.Show("Invalid password pattern");
                }
                else
                {
                    context.AddUser(UserName, Password, Email);
                    ClearAllFields();
                    NewUserWindowHide = Visibility.Collapsed;
                    Window window = new LoginView();
                    window.DataContext = new LoginViewModel();
                    window.Show();
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
        private void ClearAllFields()
        {
            UserName = string.Empty;
            Password = string.Empty;
            Email = string.Empty;
        }
    }
}
