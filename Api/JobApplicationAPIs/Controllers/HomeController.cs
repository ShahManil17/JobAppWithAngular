using JobApplicationAPIs.Data;
using JobApplicationAPIs.Data.DataModel;
using JobApplicationAPIs.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JobApplicationAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public HomeController(IConfiguration configuration, ApplicationDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginModel model)
        {
            var data = _context.Users
                        .Where(x => x.UserName == model.UserName && x.Password == model.Password)
                        .ToList();
            // Code of cherry pick main
            // Code of cherryTest
            // Simulate user authentication (replace with actual authentication logic)
            if (data.Any())
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>();
                claims.Add(new Claim("UserName", data[0].UserName));
                claims.Add(new Claim("Role", data[0].Role));
                switch (data[0].Role)
                {
                    case "Admin":
                        claims.Add(new Claim("ShowAll", "ShowAll"));
                        claims.Add(new Claim("Apply", "Apply"));
                        claims.Add(new Claim("Update", "Update"));
                        claims.Add(new Claim("Delete", "Delete"));
                        claims.Add(new Claim("Assign Roles", "Assign Roles"));
                        break;
                    case "Manager":
                        claims.Add(new Claim("Apply", "Apply"));
                        claims.Add(new Claim("Update", "Update"));
                        break;
                    case "User":
                        claims.Add(new Claim("Apply", "Apply"));
                        break;
                }

                var Sectoken = new JwtSecurityToken(
                  _configuration["Jwt:Issuer"],
                  _configuration["Jwt:Audience"],
                  claims: claims,
                  expires: DateTime.Now.AddMinutes(2),
                  signingCredentials: credentials
                );

                var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);
                Response.Cookies.Append("Token", token);

                var insertData = new Users
                {
                    Id = data[0].Id,
                    UserName = data[0].UserName,
                    Password = data[0].Password,
                    Role = data[0].Role,
                    RefreshToken = data[0].RefreshToken,
                    AccessToken = token
                };
                _context.Users.Update(insertData);
                _context.SaveChanges();

                return Ok(token);
            }

            return Unauthorized();
        }

        [HttpPost("Register")]
        public IActionResult Register([FromQuery] LoginModel model)
        {
            try
            {
                var dataModel = new Users
                {
                    UserName = model.UserName,
                    Password = model.Password,
                    Role = "User"
                };
                _context.Users.Add(dataModel);
                _context.SaveChanges();
                
                return Ok("Registration Succeded");
            }
            catch
            {
                return BadRequest("Opps, Something Went Wrong");
            }
        }

        [Authorize(Policy = "AssignRoles")]
        [HttpPost("AssignPermission")]
        public IActionResult AssignRole(int id, string role)
        {
            var userData = _context.Users
                .Where(x => x.Id == id)
                .ToList();
            if (userData.Any())
            {
                try
                {
                    var newData = new Users
                    {
                        Id = userData[0].Id,
                        UserName = userData[0].UserName,
                        Password = userData[0].Password,
                        Role = role
                    };
                    _context.Users.Update(newData);
                    _context.SaveChanges();
                    return Ok("Role Assigned Succesfully");
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                    return BadRequest("Opps, Something Went Wrong");
                }
            }
            else
            {
                return NotFound("User Not Found");
            }
        }

        [Authorize(Policy = "AssignRoles")]
        [HttpGet("ShowUsers")]
        public IEnumerable<Users> ShowUsers()
        {
            return _context.Users;
        }
    }
}
