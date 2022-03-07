using final_pharmacy.Commands;
using final_pharmacy.DbHandler.DbServices;
using final_pharmacy.Services;
using final_pharmacy.Views;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace final_pharmacy.ViewModels
{
    public class LoginViewModel : NotifyPropertyChanged
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
        private Visibility mLoginWindowHide = Visibility.Visible;
        public Visibility LoginWindowHide
        {
            get { return mLoginWindowHide; }
            set
            {
                mLoginWindowHide = value;
                OnPropertyChanged(nameof(LoginWindowHide));
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
            using(UserDataContext context = new UserDataContext())
            {
                context.InsureFirstAdmin();
                bool userfound = context.Users.Any(user => user.UserName == UserName && user.Password == Password && user.IsActive);
                if (userfound)
                {
                    if (context.Users.Any(user => user.UserName == "admin" && user.Password == "admin" && user.IsActive))
                    {
                        context.DeActivateUser("admin@gmail.com");
                        Window window = new NewUserView();
                        LoginWindowHide = Visibility.Collapsed;
                        window.DataContext = new NewUserViewModel();
                        window.Show();
                    }
                    else
                    {
                        using(AuthLogDataContext context1 = new AuthLogDataContext())
                        {
                            context1.LoginUser(UserName);
                        }
                        Window window = new MainView();
                        LoginWindowHide = Visibility.Collapsed;
                        window.DataContext = new MainViewModel();
                        window.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Wrong username or password / admin has been deactivated");
                }
            }
        }

        private bool CanSubmitExecute(object parameter)
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
