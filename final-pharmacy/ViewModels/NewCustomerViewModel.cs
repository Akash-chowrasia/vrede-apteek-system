using final_pharmacy.Commands;
using final_pharmacy.DbHandler.DbServices;
using final_pharmacy.Services;
using final_pharmacy.Services.Navigations;
using final_pharmacy.ViewModels.Base;
using System.Windows;
using System.Windows.Input;

namespace final_pharmacy.ViewModels
{
    public class NewCustomerViewModel : BaseViewModel
    {
        private INavigator navigator;
        public NewCustomerViewModel(INavigator navigator)
        {
            this.navigator = navigator;
        }
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
                if(string.IsNullOrEmpty(CustomerName) || !validate.IsAlphabetOnly(CustomerName))
                {
                    MessageBox.Show("name should contain alphabets only");
                }
                else if(string.IsNullOrEmpty(CustomerContact) || !validate.IsValidContact(CustomerContact))
                {
                    MessageBox.Show("Invalid Contact number");
                }
                else
                {
                    context.AddCustomer(CustomerName, CustomerContact);
                    navigator.CurrentViewModel = new CustomerViewModel(navigator);
                }
            }
        }

        private bool CanSubmitExecute(object parameter)
        {
            if (string.IsNullOrEmpty(CustomerName) || string.IsNullOrEmpty(CustomerContact))
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
            CustomerName = string.Empty;
            CustomerContact = string.Empty;
        }
        private bool CanClearExecute(object parameter)
        {
            return true;
        }
    }
}
