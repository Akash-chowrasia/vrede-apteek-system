using System.ComponentModel.DataAnnotations;

namespace final_pharmacy.DbHandler.DbModel
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public User()
        {

        }
        public User(string username, string password, string email)
        {
            this.UserName = username;
            this.Password = password;
            this.Email = email;
            this.IsActive = true;
        }
    }
}
