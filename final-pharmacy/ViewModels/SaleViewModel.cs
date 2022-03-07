using final_pharmacy.DbHandler.DbModel;
using final_pharmacy.DbHandler.DbServices;
using final_pharmacy.ViewModels.Base;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace final_pharmacy.ViewModels
{
    public class SaleViewModel : BaseViewModel
    {
        private ObservableCollection<Sale> sales;
        public ObservableCollection<Sale> Sales
        {
            get { return sales; }
            set
            {
                sales = value;
                OnPropertyChanged(nameof(Sales));
            }
        }
        public SaleViewModel()
        {
            SaleDataContext context = new SaleDataContext();
            sales = new ObservableCollection<Sale>();
            List<Sale> result = context.Sales.ToList().FindAll(I => I.CustomerName != null);
            foreach (Sale r in result)
            {
                sales.Add(r);
            }
        }
    }
}
