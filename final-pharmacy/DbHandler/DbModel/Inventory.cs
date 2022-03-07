using System.ComponentModel.DataAnnotations;

namespace final_pharmacy.DbHandler.DbModel
{
    public class Inventory
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public Inventory()
        {

        }
        public Inventory(string name, float price, int quantity, string description)
        {
            this.Name = name;
            this.Price = price;
            this.Quantity = quantity;
            this.Description = description;
        }
    }
}
