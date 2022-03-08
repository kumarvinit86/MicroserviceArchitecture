using EShopping.User.Core.Application;
using EShopping.User.Core.Model;
using Microsoft.AspNetCore.Mvc;

namespace EShopping.UserApi.Controllers
{
    /// <summary>
    /// The user controller.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        public UserController(UserService userService)
        {
            this.userService = userService;
        }

        UserService userService; 

        [HttpGet]
        public List<UserModel> Get()
        {
           return userService.GetUsers();
        }
        [HttpPost]
        public int Post(UserModel userModel)
        {
            return userService.AddUser(userModel);
        }

        [HttpDelete]
        public int Delete(int id)
        {
            return userService.RemoveUser(id);
        }

    }
}
