using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.User.Core.Model
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string EmailId { get; set; }
        public string MobileNumber { get; set; }
        public string Password { get; set; }

    }
}
