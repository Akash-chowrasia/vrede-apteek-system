using final_pharmacy.DbHandler.DbServices;
using final_pharmacy.Services.Navigations;
using final_pharmacy.ViewModels;
using final_pharmacy.Views;
using System;
using System.Windows;
using System.Windows.Input;

namespace final_pharmacy.Commands
{
    public class UpdateViewModelCommand : ICommand
    {
        private INavigator navigator;
        public UpdateViewModelCommand(INavigator navigator)
        {
            this.navigator = navigator;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            if (parameter is ViewType)
            {
                ViewType viewtype = (ViewType)parameter;
                switch (viewtype)
                {
                    case ViewType.ViewPharmacy:
                        navigator.CurrentViewModel = new PharmacyViewModel();
                        break;
                    case ViewType.UpdatePharmacy:
                        navigator.CurrentViewModel = new UpdatePharmacyViewModel(navigator);
                        break;
                    case ViewType.ViewMedicine:
                        navigator.CurrentViewModel = new InventoryViewModel(navigator);
                        break;
                    case ViewType.NewMedicine:
                        navigator.CurrentViewModel = new NewInventoryViewModel(navigator);
                        break;
                    case ViewType.ViewCustomer:
                        navigator.CurrentViewModel = new CustomerViewModel(navigator);
                        break;
                    case ViewType.NewCustomer:
                        navigator.CurrentViewModel = new NewCustomerViewModel(navigator);
                        break;
                    case ViewType.ViewSales:
                        navigator.CurrentViewModel = new SaleViewModel();
                        break;
                    case ViewType.NewSale:
                        navigator.CurrentViewModel = new NewSaleViewModel(navigator);
                        break;
                    case ViewType.ViewAdmin:
                        navigator.CurrentViewModel = new AdminViewModel(navigator);
                        break;
                    case ViewType.NewAdmin:
                        navigator.CurrentViewModel = new NewAdminViewModel(navigator);
                        break;
                    case ViewType.ChangePassword:
                        navigator.CurrentViewModel = new ChangePasswordViewModel(navigator);
                        break;
                    case ViewType.Logout:
                        using (AuthLogDataContext context = new AuthLogDataContext())
                        {
                            context.LogoutUser();
                            Window window = new LoginView();
                            window.DataContext = new LoginViewModel();
                            window.Show();
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
