using final_pharmacy.Commands;
using final_pharmacy.DbHandler.DbModel;
using final_pharmacy.DbHandler.DbServices;
using final_pharmacy.Services.Navigations;
using final_pharmacy.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace final_pharmacy.ViewModels
{
    public class CustomerViewModel : BaseViewModel
    {
        private INavigator navigator;
        private ObservableCollection<Customer> customers;
        public ObservableCollection<Customer> Customers
        {
            get { return customers; }
            set
            {
                customers = value;
                OnPropertyChanged(nameof(Customers));
            }
        }
        public CustomerViewModel(INavigator navigator)
        {
            this.navigator = navigator;
            CustomerDataContext context = new CustomerDataContext();
            customers = new ObservableCollection<Customer>();
            List<Customer> result = context.Customers.ToList().FindAll(C => C.Status || !C.Status);
            foreach (Customer r in result)
            {
                customers.Add(r);
            }
        }
        private Customer selectedCustomer;
        public Customer SelectedCustomer
        {
            get { return selectedCustomer; }
            set
            {
                selectedCustomer = value;
                OnPropertyChanged("SelectedCustomer");
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
            navigator.CurrentViewModel = new UpdateCustomerViewModel(navigator, SelectedCustomer);
        }

        private bool CanSubmitExecute(object parameter)
        {
            return true;
        }
    }
}
