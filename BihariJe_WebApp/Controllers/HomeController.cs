using BihariJe_WebApp.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using BihariJe_WebApp.ApiRepositories;
using Microsoft.VisualBasic;

namespace BihariJe_WebApp.Controllers
{
    public class HomeController : Controller
    {
        readonly Uri baseAddress = new Uri("https://localhost:7242/api/");
        readonly HttpClient httpClient = new HttpClient();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(new AdminSite().GetProducts());
        }

        [Authorize(Roles ="Admin")]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult LoginUser(string Emailid,string password)
        {
           // string email = "san@gmail.com";
            string role;
           // string  password="san@Singh7900";
            HttpResponseMessage response = httpClient.GetAsync(baseAddress + $"Users/LoginUser?Emailid={Emailid}&password={password}").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                if (data != "")
                {
                    // store token in cookie
                   //Response.Cookies.Append(
                   //    Constants.XAccessToken,new CookieOptions
                   //    {
                   //        HttpOnly=true,
                   //        SameSite=SameSiteMode.Strict,
                   //    }
                   //    );
                    //
                    HttpContext.Session.SetString("JwtToken",data);
                   // HttpContext.Session.GetString("JwtToken");
                    
                    var handler = new JwtSecurityTokenHandler();
                    var token = handler.ReadToken(data);
                    var jsontoken = token as JwtSecurityToken;
                     role = jsontoken!.Claims.FirstOrDefault(c => c.Type.Equals("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", StringComparison.InvariantCultureIgnoreCase))!.Value;
                    var name= jsontoken.Claims.FirstOrDefault(c => c.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", StringComparison.InvariantCultureIgnoreCase))!.Value;
                    var cemail = jsontoken.Claims.FirstOrDefault(c => c.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress", StringComparison.InvariantCultureIgnoreCase))!.Value;
                    //var useremail= jsontoken.Claims.FirstOrDefault(c => c.Type.ToString() == "Email")!.Value;
                    AddClaims(name, role,cemail);

                    ViewData["status"] = "Success";
                    if(role == "Admin")
                    {
                       //return RedirectToAction("odata", "ImageMappers", new { area = "" });
                        return Json(new { result = "Redirect", url = Url.Action("odata", "ImageMappers") });

                    }

                }
                else
                {
                    ViewData["status"] = "Failed";
                    return BadRequest();

                }

            }

            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Login()
        {
            return View();
        }
        public async Task<IActionResult> RegisterUser(User user)
        {
            return Ok();
        }
        private void AddClaims(string name, string role,string email)
        {
            List<Claim> climList = new List<Claim>();
            climList.Add(new Claim(ClaimTypes.Name, name));
            climList.Add(new Claim(ClaimTypes.Role, role));
            climList.Add(new Claim(ClaimTypes.Email, email));
            ClaimsIdentity claims = new ClaimsIdentity(climList, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claims);
            HttpContext.SignInAsync(claimsPrincipal);


        }
    }

}