using System.ComponentModel.DataAnnotations;

namespace final_pharmacy.DbHandler.DbModel
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }
        public string MedicineName { get; set; }
        public float MedicinePrice { get; set; }
        public int MedicineQuantity { get; set; }
        public string CustomerName { get; set; }
        public float TotalAmmount { get; set; }
        public Sale()
        {

        }
        public Sale(string medicinename, float medicineprice, int medicinequantity, string customername)
        {
            this.MedicineName = medicinename;
            this.MedicinePrice = medicineprice;
            this.MedicineQuantity = medicinequantity;
            this.CustomerName = customername;
            this.TotalAmmount = medicineprice * medicinequantity;
        }
    }
}
