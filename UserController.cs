using Lab5.Hasher;
using Lab5.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace Lab5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly Context DBContext;

        public UserController(Context databaseContext)
        {
            DBContext = databaseContext;
        }

        [HttpGet("{id?}")]
        public IEnumerable<User> List(int? id)
        {
            if (id.HasValue)
                return DBContext.Users.Where(user => user.id == id);
            else
                return DBContext.Users;
        }

        [HttpPost("register")]
        public IActionResult RegisterUser(RegistrationData model)
        {
            if (ModelState.IsValid)
            {
                var hasher = new PasswordHasher(1000);
                var user = new User
                {
                    email = model.email,
                    password = hasher.Hash(model.password),
                    first_name = model.first_name,
                    last_name = model.last_name,
                };
                DBContext.Users.Add(user);
                DBContext.SaveChanges();

                return Ok(new Dictionary<string, dynamic> { { "Successfully registered with id", user.id } });
            }
            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        public IActionResult Token(LoginData loginDTO)
        {
            var user = GetUser(loginDTO.email, loginDTO.password);
            if (user == null)
                return BadRequest(new {error_text = "Wrong email or password. Try again" });
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                    issuer: Auth.ISSUER,
                    audience: Auth.AUDIENCE,
                    notBefore: now,
                    claims: user.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(Auth.LIFETIME)),
                    signingCredentials: new SigningCredentials(Auth.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var response = new
            {
                access_token = encodedJwt,
                user_id = user.Name
            };

            return Ok(response);
        }

        private ClaimsIdentity GetUser(string email, string password)
        {
            var hasher = new PasswordHasher(1000);

            var identity = DBContext.Users.FirstOrDefault(x => x.email == email);
            if (identity != null && hasher.Check(identity.password, password).verified)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, identity.id.ToString()),
            };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            return null;
        }
    }
}
