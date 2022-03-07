using final_pharmacy.DbHandler.DbModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace final_pharmacy.DbHandler.DbServices
{
    public class InventoryDataContext : DataBaseContext
    {
        public void AddInventory(string name, float price, int quantity, string description)
        {
            Inventory obj = new Inventory(name, price, quantity, description);
            this.Inventories.Add(obj);
            this.SaveChanges();
        }
        public void UpdateInventory(string name, int quantity, string description)
        {
            var obj = this.Inventories.Where(I => I.Name == name).FirstOrDefault();
            obj.Quantity = quantity;
            obj.Description = description;
            this.SaveChanges();
        }
    }
}
