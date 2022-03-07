using final_pharmacy.Commands;
using final_pharmacy.Services.Navigations;
using final_pharmacy.ViewModels.Base;
using System.Windows;
using System.Windows.Input;

namespace final_pharmacy.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public INavigator Navigator { get; set; } = new Navigator();

        private Visibility mMainWindowHide = Visibility.Visible;
        public Visibility MainWindowHide
        {
            get { return mMainWindowHide; }
            set
            {
                mMainWindowHide = value;
                OnPropertyChanged(nameof(MainWindowHide));
            }
        }
        private ICommand _MainWindowHideCommand;
        public ICommand MainWindowHideCommand
        {
            get
            {
                if (_MainWindowHideCommand == null)
                {
                    _MainWindowHideCommand = new RelayCommand(MainWindowHideCommandExecute, CanMainWindowHideCommandExecute);
                }
                return _MainWindowHideCommand;
            }
        }
        private void MainWindowHideCommandExecute(object parameter)
        {
            MainWindowHide = Visibility.Collapsed;
        }
        private bool CanMainWindowHideCommandExecute(object parameter)
        {
            return true;
        }
    }
}
