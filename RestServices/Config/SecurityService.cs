using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace REST.Config
{
    public class SecurityService
    {
        IConfiguration config;
        DataService service;
        public SecurityService(IConfiguration _config,DataService _service)
        {
            this.config = _config;
            this.service = _service;
        }
        private string GeterateJWT(string role)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Aud,config["Jwt:audience"]!),
                new Claim(JwtRegisteredClaimNames.Iss,config["Jwt:issuer"]!),
                new Claim(ClaimTypes.Role, role)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:key"]!));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var securityToken = new JwtSecurityToken(config["Jwt:issuer"]!, config["Jwt:audience"]!, claims, expires: DateTime.Now.AddMinutes(30),signingCredentials:cred);

            string token= new JwtSecurityTokenHandler().WriteToken(securityToken);
            return token;
        }
        public TokenAndRole? AuthenticateUserAndGetToken(AppUserCredentalsModel model)
        {
            string? token=null;
            string? role=service.FindUser(model);
            if (role != null)
            {
                token = GeterateJWT(role!);
                return new TokenAndRole(){Token=token,Role=role};
            }
            else
            {
                return null;  //means   "INVALID_UID_PWD";
            }
            
        }


    }

    public class TokenAndRole{
        public string? Token{get;set;}
        public string? Role{get;set;}

    }
    public class Policies
    {
        public const string Customer = "Customer";
        public const string Admin = "Admin";
        
        public static AuthorizationPolicy CustomerPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Customer).Build();
        }

        public static AuthorizationPolicy AdminPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Admin).Build();
        }
    }

    public class AppUserCredentalsModel
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
