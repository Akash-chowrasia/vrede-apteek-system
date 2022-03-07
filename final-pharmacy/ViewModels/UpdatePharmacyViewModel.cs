using final_pharmacy.Commands;
using final_pharmacy.DbHandler.DbModel;
using final_pharmacy.DbHandler.DbServices;
using final_pharmacy.Services;
using final_pharmacy.Services.Navigations;
using final_pharmacy.ViewModels.Base;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace final_pharmacy.ViewModels
{
    public class UpdatePharmacyViewModel: BaseViewModel
    {
        private INavigator navigator;
        
        private string pharmacyName;
        public string PharmacyName
        {
            get { return pharmacyName; }
            set
            {
                pharmacyName = value;
                OnPropertyChanged("PharmacyName");
            }
        }
        private string pharmacyAddress;
        public string PharmacyAddress
        {
            get { return pharmacyAddress; }
            set
            {
                pharmacyAddress = value;
                OnPropertyChanged("PharmacyAddress");
            }
        }
        public UpdatePharmacyViewModel(INavigator navigator)
        {
            this.navigator = navigator;
            using (PharmacyDataContext context = new PharmacyDataContext())
            {
                context.EnsureTableCreated();
                List<Pharmacy> result = context.Pharmacy.ToList().FindAll(p => p.Name != "Not Available");
                foreach (Pharmacy r in result)
                {
                    PharmacyName = r.Name;
                    PharmacyAddress = r.Address;
                }
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
            using (PharmacyDataContext context = new PharmacyDataContext())
            {
                Validators validate = new Validators();
                if (!validate.IsAlphabetOnly(PharmacyName))
                {
                    MessageBox.Show("Pharmacy name should contain alphabet only");
                }
                else if (string.IsNullOrEmpty(PharmacyAddress))
                {
                    MessageBox.Show("Pharmacy address is required");
                }
                else
                {
                    context.UpdatePharmacy(PharmacyName, PharmacyAddress);
                    navigator.CurrentViewModel = new PharmacyViewModel();
                }
            }
        }

        private bool CanSubmitExecute(object parameter)
        {
            if (string.IsNullOrEmpty(PharmacyName) || string.IsNullOrEmpty(PharmacyAddress))
            {
                return false;
            }
            else
            {
                return true;
            }
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
            navigator.CurrentViewModel = new PharmacyViewModel();
        }
        private bool CanCancelExecute(object parameter)
        {
            return true;
        }
    }
}
