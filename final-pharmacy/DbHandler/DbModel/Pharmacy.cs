using System.ComponentModel.DataAnnotations;

namespace final_pharmacy.DbHandler.DbModel
{
    public class Pharmacy
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public Pharmacy()
        {

        }
        public Pharmacy(string name, string address)
        {
            this.Name = name;
            this.Address = address;
        }
    }
}
