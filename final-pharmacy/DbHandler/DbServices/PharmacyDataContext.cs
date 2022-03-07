using final_pharmacy.DbHandler.DbModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace final_pharmacy.DbHandler.DbServices
{
    public class PharmacyDataContext : DataBaseContext
    {

        public void EnsureTableCreated()
        {
            if (!this.Pharmacy.Any(user => user.Name == "Not Available"))
            {
                Pharmacy obj = new Pharmacy("Not Available", "Not Available");
                this.Pharmacy.Add(obj);
                this.SaveChanges();
            }
        }
        public void UpdatePharmacy(string name, string address)
        {
            Pharmacy obj = new Pharmacy(name, address);
            this.Pharmacy.RemoveRange(this.Pharmacy);
            this.Pharmacy.Add(obj);
            this.SaveChanges();
        }
    }
}
