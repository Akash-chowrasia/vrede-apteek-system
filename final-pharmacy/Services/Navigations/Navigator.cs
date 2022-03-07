using final_pharmacy.Commands;
using final_pharmacy.ViewModels.Base;
using System.Windows.Input;

namespace final_pharmacy.Services.Navigations
{
    public class Navigator : NotifyPropertyChanged, INavigator
    {
        private BaseViewModel mCurrentViewModel;
        public BaseViewModel CurrentViewModel
        {
            get { return mCurrentViewModel; }
            set
            {
                mCurrentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

        public ICommand UpdateViewModelCommand => new UpdateViewModelCommand(this);
    }
}
