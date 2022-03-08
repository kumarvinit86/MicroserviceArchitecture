using EShopping.User.Core.Application;
using EShopping.User.Core.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EShopping.Common;

namespace EShopping.UserApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IdentityController : ControllerBase
    {
        UserService userService;
        public IdentityController(UserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public IActionResult Post(UserModel userModel)
        {
            var authenticatedUser = userService.GetUsers()
                .FirstOrDefault(x => x.EmailId == userModel.EmailId || x.UserName == userModel.UserName && x.Password == userModel.Password);
            IActionResult result = Unauthorized();
            if(authenticatedUser != null)
            {
               var token = GenerateJSONWebToken(userModel);
                if(token != string.Empty)
                {
                    result = Ok(new { token = token });
                }
            }
            return result;
        }

        [NonAction]
        private string GenerateJSONWebToken(UserModel userModel)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Constants.SecretKey));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

                var claims = new[] {
                                 new Claim(JwtRegisteredClaimNames.Sid, userModel.UserId.ToString()),
                                 new Claim(JwtRegisteredClaimNames.Email, userModel.EmailId),
                                 new Claim("UserId", userModel.UserId.ToString()),
                                 new Claim("MobileNumber", userModel.MobileNumber),
                                 new Claim("Username", userModel.UserName),
                                 new Claim("EmailId", userModel.EmailId),
                                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                                 };
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    SigningCredentials = credentials,
                    Expires = DateTime.Now.AddHours(12),
                    Issuer = Constants.Issuer,
                    Audience = Constants.Issuer
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
    }
}
