using final_pharmacy.Commands;
using final_pharmacy.DbHandler.DbModel;
using final_pharmacy.DbHandler.DbServices;
using final_pharmacy.Services.Navigations;
using final_pharmacy.ViewModels.Base;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace final_pharmacy.ViewModels
{
    public class AdminViewModel : BaseViewModel
    {
        private INavigator navigator;
        private ObservableCollection<User> users;
        public ObservableCollection<User> Users
        {
            get { return users; }
            set
            {
                users = value;
                OnPropertyChanged(nameof(Users));
            }
        }
        public AdminViewModel(INavigator navigator)
        {
            this.navigator = navigator;
            UserDataContext context = new UserDataContext();
            users = new ObservableCollection<User>();
            List<User> result = context.Users.ToList().FindAll(user => user.IsActive || !user.IsActive);
            foreach (User r in result)
            {
                users.Add(r);
            }
        }
        private User selectedUser;
        public User SelectedUser
        {
            get { return selectedUser; }
            set
            {
                selectedUser = value;
                OnPropertyChanged("SelectedUser");
            }
        }
        private ICommand _ExecuteUpdateCommand;
        public ICommand ExecuteUpdateCommand
        {
            get
            {
                if (_ExecuteUpdateCommand == null)
                {
                    _ExecuteUpdateCommand = new RelayCommand(SubmitExecute, CanSubmitExecute);
                }
                return _ExecuteUpdateCommand;
            }
        }

        private void SubmitExecute(object parameter)
        {
            navigator.CurrentViewModel = new UpdateAdminViewModel(navigator, SelectedUser);
        }

        private bool CanSubmitExecute(object parameter)
        {
            return true;
        }
    }
}
