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
    public class InventoryViewModel : BaseViewModel
    {
        private INavigator navigator;
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
        public InventoryViewModel(INavigator navigator)
        {
            this.navigator = navigator;
            InventoryDataContext context = new InventoryDataContext();
            inventories = new ObservableCollection<Inventory>();
            List<Inventory> result = context.Inventories.ToList().FindAll(I => I.Name != null);
            foreach (Inventory r in result)
            {
                inventories.Add(r);
            }
        }
        private Inventory selectedInventory;
        public Inventory SelectedInventory
        {
            get { return selectedInventory; }
            set
            {
                selectedInventory = value;
                OnPropertyChanged("SelectedInventory");
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
            using (InventoryDataContext context = new InventoryDataContext())
            {
                navigator.CurrentViewModel = new UpdateInventoryViewModel(navigator, SelectedInventory);
            }
        }

        private bool CanSubmitExecute(object parameter)
        {
            return true;
        }
    }
}
