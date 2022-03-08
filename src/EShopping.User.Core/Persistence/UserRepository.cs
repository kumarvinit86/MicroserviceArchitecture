using EShopping.User.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.User.Core.Persistence
{
    public class UserRepository
    {
        public UserRepository()
        {
            Users.Add(new UserModel
            {
                UserName = "Admin",
                EmailId = "admin@test.com",
                MobileNumber ="9999666777",
                UserId=1,
                Password="admin"
            });
            Users.Add(new UserModel
            {
                UserName = "Vinit",
                EmailId = "vinit@test.com",
                MobileNumber = "9999666771",
                UserId = 2,
                Password = "admin"
            });
        }
        public static List<UserModel> Users { get; set; }
    }
}
