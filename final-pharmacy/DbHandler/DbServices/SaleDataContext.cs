using final_pharmacy.DbHandler.DbModel;
using Microsoft.EntityFrameworkCore;

namespace final_pharmacy.DbHandler.DbServices
{
    public class SaleDataContext : DataBaseContext
    {
        public void AddSale(string medicinename,float medicineprice, int medicinequantity, string customername)
        {
            Sale obj = new Sale(medicinename, medicineprice, medicinequantity, customername);
            this.Sales.Add(obj);
            this.SaveChanges();
        }
    }
}
