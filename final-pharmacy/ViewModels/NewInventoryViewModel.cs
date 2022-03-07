using final_pharmacy.Commands;
using final_pharmacy.DbHandler.DbServices;
using final_pharmacy.Services;
using final_pharmacy.Services.Navigations;
using final_pharmacy.ViewModels.Base;
using System.Windows;
using System.Windows.Input;

namespace final_pharmacy.ViewModels
{
    public class NewInventoryViewModel : BaseViewModel
    {
        private INavigator navigator;
        public NewInventoryViewModel(INavigator navigator)
        {
            this.navigator = navigator;
        }
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
                if (string.IsNullOrEmpty(MedicineName) || !validate.IsAlphabetOnly(MedicineName))
                {
                    MessageBox.Show("Medicine name should contain alphabets only");
                }
                else if (string.IsNullOrEmpty(MedicinePrice.ToString()) || !validate.IsNumberOnly(MedicinePrice.ToString()) || MedicinePrice <= 0)
                {
                    MessageBox.Show("Kindly enter a valid price ammount");
                }
                else if(string.IsNullOrEmpty(MedicineQuantity.ToString()) || !validate.IsNumberOnly(MedicineQuantity.ToString()) || MedicineQuantity <= 0)
                {
                    MessageBox.Show("Invalid quantity");
                }
                else
                {
                    context.AddInventory(MedicineName, MedicinePrice, MedicineQuantity, MedicineDescription);
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
            MedicineName = string.Empty;
            MedicinePrice = 0;
            MedicineQuantity = 0;
            MedicineDescription = string.Empty;
        }
        private bool CanClearExecute(object parameter)
        {
            return true;
        }
    }
}
