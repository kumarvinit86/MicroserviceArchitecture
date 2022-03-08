using EShopping.User.Core.Persistence;
using EShopping.User.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.User.Core.Application
{
    public class UserService
    {

        public UserService()
        {

        }

        public int AddUser(UserModel userModel)
        {
            UserRepository.Users.Add(userModel);
            return 1;
        }

        public int RemoveUser(int UserId)
        {
            var user = UserRepository.Users.FirstOrDefault(x => x.UserId == UserId);
            UserRepository.Users.Remove(user);
            return 1;
        }

        public List<UserModel> GetUsers()
        {
            return UserRepository.Users.ToList();
        }
    }
}
