using HalfDegreeApi.Data;
using HalfDegreeApi.Models;
using HalfDegreeApi.Services;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace HalfDegreeApi.Repositories
{
    public class UserData : IUser
    {
        private  HalfDegreeApiDbContext _context;
        private readonly IConfiguration _configuration;

        public UserData(HalfDegreeApiDbContext halfDegreeApiDbContext, IConfiguration configuration)
        {
            _context = halfDegreeApiDbContext;
            _configuration = configuration;

        }

        public string LoginUser(string username, string password)
        {
            password=GetHash(password);
            var loginUser=_context.users!.Where(o=>o.Email == username && o.Password==password).FirstOrDefault();
            if (loginUser != null)
            {
                var role = _context.roles!.Where(r => r.Id == loginUser.RoleId).Select(n => n.Name).First();
                var AuthClaim = new List<Claim>
                {
                new Claim(ClaimTypes.NameIdentifier, loginUser.Name!),
                new Claim(ClaimTypes.Email, loginUser.Email!),
                new Claim(ClaimTypes.Role,role!),
                new Claim(ClaimTypes.Name,loginUser.Name!),
                };
                var token = GenrateToken(_configuration["JWT:AudienceWebAPI"], AuthClaim, _configuration["JWT:SecretKey"]);
                return token;
                //var handler = new JwtSecurityTokenHandler();
                //var decodedValue = handler.ReadJwtToken(token);

            }

            //var AuthClaim = new List<Claim>
            //{
            //    new Claim(ClaimTypes.NameIdentifier, username),
            //    new Claim(ClaimTypes.Email, "Sanjay.singh@gmail.com"),
            //    new Claim(ClaimTypes.Role,"Admin"),
            //    new Claim(ClaimTypes.Name,"sanjay singh"),
            //    new Claim(ClaimTypes.UserData,"ioijojnjnfjnw")
            //};
            //var token= GenrateToken("sanjay", AuthClaim, "this-jwt-secret-key");
            //var handler = new JwtSecurityTokenHandler();
            //var decodedValue = handler.ReadJwtToken(token);
            return "UnAuthentication";
        }

        public string LogoutUser(string username)
        {
            return string.Empty;

        }

        public bool RegisterUser(User user)
        {
            user.Password = GetHash(user.Password!);
            _context.users!.Add(user);
            _context.SaveChanges();

            return true;
            
        }
        private string GenrateToken(string Audience,List<Claim> claims,string key)
        {
            var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            var token = new JwtSecurityToken(
               issuer: Audience,
               audience: Audience,
               expires: DateTime.Now.AddMinutes(5),
               claims: claims,
               signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256)
               );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private  string GetHash(string text)
        {
            // SHA512 is disposable by inheritance.  
            var sha256 = SHA256.Create();
            
                // Send a sample text to hash.  
                 var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));
                // Get the hashed string.  
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            
        }
    }
}
