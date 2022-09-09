using API.Context;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories.Data
{
    public class UserRepository : GeneralRepository<User, int>
    {
        MyContext myContext;

        public UserRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        //LOGIN
        public User Login(string username, string password)
        {
            var data = myContext.Users.SingleOrDefault(x => x.Username == username);
            if (data == null)
                return null;
            if (data.Password != password)
                return null;
            return data;
        }


        //REGISTER
        public int Register(User user)
        {
            if (myContext.Users.Any(x => x.Username == user.Username))
                return -1;
            myContext.Users.Add(user);
            var result = myContext.SaveChanges();
            return result;
        }


        //CHANGE PASSWORD
        public int ChangePassword(int id, User user)
        {
            var data = myContext.Users.Find(id);
            if (data == null)
                return -1;
            if (!string.IsNullOrWhiteSpace(user.Password) && user.Password != data.Password)
            {
                data.Password = user.Password;
            }
            var result = myContext.SaveChanges();
            return result;
        }


        //FORGOT PASSWORD
        public int ForgotPassword(User user)
        {
            var data = myContext.Users.SingleOrDefault(x => x.Username == user.Username);
            if (data == null)
                return -1;
            if (!string.IsNullOrWhiteSpace(user.Password))
            {
                data.Password = user.Password;
            }
            var result = myContext.SaveChanges();
            return result;
        }
    }
}
