using final_pharmacy.ViewModels.Base;
using System.Windows.Input;

namespace final_pharmacy.Services.Navigations
{
    public interface INavigator
    {
        BaseViewModel CurrentViewModel { get; set; }

        ICommand UpdateViewModelCommand { get; }
    }
}
