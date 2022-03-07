using final_pharmacy.DbHandler.DbModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace final_pharmacy.DbHandler.DbServices
{
    public class UserDataContext: DataBaseContext
    {
        public void InsureFirstAdmin()
        {
            if (!this.Users.Any(user => user.UserName == "admin" && user.Password == "admin"))
            {
                User obj = new User("admin", "admin", "admin@gmail.com");
                this.Users.Add(obj);
                this.SaveChanges();
            }
            else if (this.Users.Any(user => user.UserName == "admin" && user.Password == "admin" && Users.Count() <= 1))
            {
                var obj = this.Users.Where(user => user.Email == "admin@gmail.com").FirstOrDefault();
                obj.IsActive = true;
                this.SaveChanges();
            }
        }
        public void AddUser(string username, string password, string email)
        {
            User obj = new User(username, password, email);
            this.Users.Add(obj);
            this.SaveChanges();
        }
        public void DeActivateUser(string email)
        {
            var obj = this.Users.Where(user => user.Email == email).FirstOrDefault();
            obj.IsActive = false;
            this.SaveChanges();
        }
        public string GetPassword(string username)
        {
            var obj = this.Users.Where(user => user.UserName == username).FirstOrDefault();
            return obj.Password;
        }
        public void ChangePassword(string username, string password)
        {
            var obj = this.Users.Where(user => user.UserName == username).FirstOrDefault();
            obj.Password = password;
            this.SaveChanges();
        }
        public void UpdateUser(string username, string email, bool isactive)
        {
            var obj = this.Users.Where(user => user.UserName == username).FirstOrDefault();
            obj.Email = email;
            obj.IsActive = isactive;
            this.SaveChanges();
        }
    }
}
