using System;
using System.ComponentModel.DataAnnotations;

namespace final_pharmacy.DbHandler.DbModel
{
    public class AuthLog
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public DateTime LoginDate { get; set; }
        public DateTime SessionExpireDate { get; set; }
        public DateTime LogoutDate { get; set; }
        public bool isLogedIn { get; set; }
        public AuthLog()
        {

        }
        public AuthLog(string userName)
        {
            this.UserName = userName;
            this.LoginDate = DateTime.Now;
            this.SessionExpireDate = this.LoginDate.AddDays(2);
            this.isLogedIn = true;
        }
    }
}
