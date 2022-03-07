using final_pharmacy.Commands;
using final_pharmacy.DbHandler.DbModel;
using final_pharmacy.DbHandler.DbServices;
using final_pharmacy.Services;
using final_pharmacy.Services.Navigations;
using final_pharmacy.ViewModels.Base;
using System.Windows;
using System.Windows.Input;

namespace final_pharmacy.ViewModels
{
    public class UpdateInventoryViewModel:BaseViewModel
    {
        private INavigator navigator;
        private string medicineName;
        public string MedicineName
        {
            get { return medicineName; }
            set
            {
                medicineName = value;
                OnPropertyChanged("MedicineName");
            }
        }
        private float medicinePrice;
        public float MedicinePrice
        {
            get { return medicinePrice; }
            set
            {
                medicinePrice = value;
                OnPropertyChanged("MedicinePrice");
            }
        }
        private int medicineQuantity;
        public int MedicineQuantity
        {
            get { return medicineQuantity; }
            set
            {
                medicineQuantity = value;
                OnPropertyChanged("MedicineQuantity");
            }
        }
        private string medicineDescription;
        public string MedicineDescription
        {
            get { return medicineDescription; }
            set
            {
                medicineDescription = value;
                OnPropertyChanged("MedicineDescription");
            }
        }
        public UpdateInventoryViewModel(INavigator navigator, Inventory inventory)
        {
            this.navigator = navigator;
            MedicineName = inventory.Name;
            MedicinePrice = inventory.Price;
            MedicineQuantity = inventory.Quantity;
            MedicineDescription = inventory.Description;
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
            using (InventoryDataContext context = new InventoryDataContext())
            {
                Validators validate = new Validators();
                if(string.IsNullOrEmpty(MedicineQuantity.ToString()) || !validate.IsNumberOnly(MedicineQuantity.ToString()) || MedicineQuantity <= 0)
                {
                    MessageBox.Show("Invalid quantity");
                }
                else
                {
                    context.UpdateInventory(MedicineName, MedicineQuantity, MedicineDescription);
                    navigator.CurrentViewModel = new InventoryViewModel(navigator);
                }
            }
        }

        private bool CanSubmitExecute(object parameter)
        {
            if (string.IsNullOrEmpty(MedicineName) || string.IsNullOrEmpty(MedicineDescription))
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
            navigator.CurrentViewModel = new InventoryViewModel(navigator);
        }
        private bool CanCancelExecute(object parameter)
        {
            return true;
        }
    }
}
