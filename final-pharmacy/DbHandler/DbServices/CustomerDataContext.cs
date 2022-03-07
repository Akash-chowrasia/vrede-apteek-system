using final_pharmacy.DbHandler.DbModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace final_pharmacy.DbHandler.DbServices
{
    public class CustomerDataContext : DataBaseContext
    {
        public void AddCustomer(string name, string contact)
        {
            Customer obj = new Customer(name, contact);
            this.Customers.Add(obj);
            this.SaveChanges();
        }
        public void UpdateCustomer(string name, string contact, bool status)
        {
            var obj = this.Customers.Where(I => I.Name == name).FirstOrDefault();
            obj.Contact = contact;
            obj.Status = status;
            this.SaveChanges();
        }
    }
}
