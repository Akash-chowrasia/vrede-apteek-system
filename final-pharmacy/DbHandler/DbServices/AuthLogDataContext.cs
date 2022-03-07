using final_pharmacy.DbHandler.DbModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace final_pharmacy.DbHandler.DbServices
{
    public class AuthLogDataContext : DataBaseContext
    {
        public void LoginUser(string username)
        {
            AuthLog obj = new AuthLog(username);
            this.AuthLogs.Add(obj);
            this.SaveChanges();
        }
        public void LogoutUser()
        {
            var obj = this.AuthLogs.Where(A => A.isLogedIn).FirstOrDefault();
            obj.LogoutDate = DateTime.Now;
            obj.isLogedIn = false;
            this.SaveChanges();
        }
        public bool IsLogedIn()
        {
            var obj = this.AuthLogs.Where(A => A.isLogedIn).FirstOrDefault();
            if (obj == null)
            {
                return false;
            }
            else
            {
                if(DateTime.Compare(obj.SessionExpireDate, DateTime.Now) >= 0)
                {
                    return true;
                }
                else
                {
                    LogoutUser();
                    return false;
                }
            }
        }
        public string GetLogedInUser()
        {
            var obj = this.AuthLogs.Where(A => A.isLogedIn).FirstOrDefault();
            return obj.UserName;
        }
    }
}
