using JobApplicationAPIs.Controllers;
using JobApplicationAPIs.Data;
using JobApplicationAPIs.Data.DataModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JobApplicationAPIs.Middlewares
{
    
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomMiddleware> _logger;
        private readonly IConfiguration _configuration;

        public CustomMiddleware(RequestDelegate next, ILogger<CustomMiddleware> logger, IConfiguration configuration)
        {
            _next = next;
            _logger = logger;
            _configuration = configuration;
        }

        public Task Invoke(HttpContext httpContext, ApplicationDbContext _context)
        {
            _logger.LogInformation("In Middleware");
            var request = httpContext.Request;
            var headers = request.Headers;
            string route = request.Path.ToString().Split('/').Last();
            if (route != "Login" && route != "Register" && route != "Error")
            {
                var token = headers["Authorization"].FirstOrDefault()?.Split(' ').Last();
                if (token != null)
                {
                    try
                    {
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var tokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = _configuration["Jwt:Issuer"],
                            ValidAudience = _configuration["Jwt:Audience"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]))
                        };
                        string? userName = null;
                        try
                        {
                            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
                            userName = principal.Claims.FirstOrDefault(_ => _.Type == "UserName").Value;
                        }
                        catch (Exception ex)
                        {
                            var userData = _context.Users
                                .Where(x => x.AccessToken == token && x.RefreshToken != null)
                                .FirstOrDefault();
                            userName = userData.UserName;
                            if (userData != null)
                            {
                                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                                var claims = new List<Claim>();
                                claims.Add(new Claim("UserName", userData.UserName));
                                claims.Add(new Claim("Role", userData.Role));
                                switch (userData.Role)
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

                                token = new JwtSecurityTokenHandler().WriteToken(Sectoken);
                                httpContext.Response.Cookies.Append("Token", token);
                                httpContext.Request.Headers["Authorization"] = "Bearer " + token;
                            }
                        }


                        if (userName != null)
                        {
                            return _next(httpContext);
                        }
                        else
                        {
                            throw new UnauthorizedAccessException();
                        }
                    }
                    catch
                    {
                        throw;
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException();
                }
            }
            else
            {
                return _next(httpContext);
            }
        }
    }

    public static class CustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomMiddleware>();
        }
    }
}
