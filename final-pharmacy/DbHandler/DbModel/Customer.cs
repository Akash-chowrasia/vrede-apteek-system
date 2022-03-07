using System.ComponentModel.DataAnnotations;

namespace final_pharmacy.DbHandler.DbModel
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public bool Status { get; set; }
        public Customer()
        {

        }
        public Customer(string name, string contact)
        {
            this.Name = name;
            this.Contact = contact;
            this.Status = true;
        }
    }
}
