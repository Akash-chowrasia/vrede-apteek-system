using final_pharmacy.Commands;
using final_pharmacy.DbHandler.DbModel;
using final_pharmacy.DbHandler.DbServices;
using final_pharmacy.Services;
using final_pharmacy.Services.Navigations;
using final_pharmacy.ViewModels.Base;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace final_pharmacy.ViewModels
{
    public class NewSaleViewModel : BaseViewModel
    {
        private INavigator navigator;
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
        private int quantityAvailable;
        public int QuantityAvailable
        {
            get { return quantityAvailable; }
            set
            {
                quantityAvailable = value;
                OnPropertyChanged("QuantityAvailable");
            }
        }
        private float totalAmmount;
        public float TotalAmmount
        {
            get { return totalAmmount; }
            set
            {
                totalAmmount = value;
                OnPropertyChanged("TotalAmmount");
            }
        }
        private int quantityPurchased;
        public int QuantityPurchased
        {
            get { return quantityPurchased; }
            set
            {
                quantityPurchased = value;
                TotalAmmount = value * MedicinePrice;
                OnPropertyChanged("QuantityPurchased");
            }
        }
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
        private ObservableCollection<Inventory> inventories;
        public ObservableCollection<Inventory> Inventories
        {
            get { return inventories; }
            set
            {
                inventories = value;
                OnPropertyChanged(nameof(Inventories));
            }
        }
        private Inventory selectedInventory;
        public Inventory SelectedInventory
        {
            get { return selectedInventory; }
            set
            {
                selectedInventory = value;
                MedicinePrice = value == null ? 0 : value.Price;
                QuantityAvailable = value == null ? 0 : value.Quantity;
                QuantityPurchased = 0;
                OnPropertyChanged("SelectedInventory");
            }
        }
        public NewSaleViewModel(INavigator navigator)
        {
            this.navigator = navigator;
            CustomerDataContext context = new CustomerDataContext();
            customers = new ObservableCollection<Customer>();
            List<Customer> result = context.Customers.ToList().FindAll(C => C.Status);
            foreach (Customer r in result)
            {
                customers.Add(r);
            }
            InventoryDataContext icontext = new InventoryDataContext();
            inventories = new ObservableCollection<Inventory>();
            List<Inventory> iresult = icontext.Inventories.ToList().FindAll(I => I.Name != null);
            foreach (Inventory r in iresult)
            {
                inventories.Add(r);
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
            Validators validate = new Validators();
            if (SelectedInventory == null)
            {
                MessageBox.Show("Select any Medicine to proceed");
            }
            else if (SelectedCustomer == null)
            {
                MessageBox.Show("Select any Customer to proceed");
            }
            else if (string.IsNullOrEmpty(QuantityPurchased.ToString()) || !validate.IsNumberOnly(QuantityPurchased.ToString()) || QuantityPurchased > QuantityAvailable || QuantityPurchased <= 0)
            {
                MessageBox.Show("Invalid Quantity to purchase");
            }
            else
            {
                using (SaleDataContext context = new SaleDataContext())
                {
                    context.AddSale(SelectedInventory.Name, SelectedInventory.Price, QuantityPurchased, SelectedCustomer.Name);
                }
                using (InventoryDataContext icontext = new InventoryDataContext())
                {
                    icontext.UpdateInventory(SelectedInventory.Name, SelectedInventory.Quantity - QuantityPurchased, SelectedInventory.Description);
                }
                navigator.CurrentViewModel = new SaleViewModel();
            }
        }

        private bool CanSubmitExecute(object parameter)
        {
            return true;
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
            QuantityPurchased = 0;
            QuantityAvailable = 0;
            MedicinePrice = 0;
            TotalAmmount = 0;
            SelectedCustomer = null;
            SelectedInventory = null;
        }
        private bool CanClearExecute(object parameter)
        {
            return true;
        }
    }
}
