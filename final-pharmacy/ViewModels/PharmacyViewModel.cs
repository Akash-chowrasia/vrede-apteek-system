using final_pharmacy.DbHandler.DbModel;
using final_pharmacy.DbHandler.DbServices;
using final_pharmacy.ViewModels.Base;
using System.Collections.Generic;
using System.Linq;

namespace final_pharmacy.ViewModels
{
    public class PharmacyViewModel : BaseViewModel
    {
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
        public PharmacyViewModel()
        {
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
    }
}
