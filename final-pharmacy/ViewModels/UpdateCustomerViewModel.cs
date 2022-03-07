using final_pharmacy.Commands;
using final_pharmacy.DbHandler.DbModel;
using final_pharmacy.DbHandler.DbServices;
using final_pharmacy.Services;
using final_pharmacy.Services.Navigations;
using final_pharmacy.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace final_pharmacy.ViewModels
{
    public class UpdateCustomerViewModel : BaseViewModel
    {
        private INavigator navigator;
        private string customerName;
        public string CustomerName
        {
            get { return customerName; }
            set
            {
                customerName = value;
                OnPropertyChanged("CustomerName");
            }
        }
        private string customerContact;
        public string CustomerContact
        {
            get { return customerContact; }
            set
            {
                customerContact = value;
                OnPropertyChanged("CustomerContact");
            }
        }
        private bool customerStatus;
        public bool CustomerStatus
        {
            get { return customerStatus; }
            set
            {
                customerStatus = value;
                OnPropertyChanged("CustomerStatus");
            }
        }
        public UpdateCustomerViewModel(INavigator navigator, Customer customer)
        {
            this.navigator = navigator;
            CustomerName = customer.Name;
            CustomerContact = customer.Contact;
            CustomerStatus = customer.Status;
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
            using (CustomerDataContext context = new CustomerDataContext())
            {
                Validators validate = new Validators();
                if (string.IsNullOrEmpty(CustomerContact) || !validate.IsValidContact(CustomerContact))
                {
                    MessageBox.Show("Invalid Contact number");
                }
                else
                {
                    context.UpdateCustomer(CustomerName, CustomerContact, CustomerStatus);
                    navigator.CurrentViewModel = new CustomerViewModel(navigator);
                }
            }
        }

        private bool CanSubmitExecute(object parameter)
        {
            return true;
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
            navigator.CurrentViewModel = new CustomerViewModel(navigator);
        }
        private bool CanCancelExecute(object parameter)
        {
            return true;
        }
    }
}
