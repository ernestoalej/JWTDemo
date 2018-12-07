using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace JWTDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        [HttpGet("token")]
        public ActionResult GetToken()
        {
            //Security Key

            string securityKey = "87bff4f2-578d-4785-95e4-12a7dfb37a27";


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

            //Signing credentials
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            //Add Claims
            var Claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Role, "Administrator"),
                new Claim(ClaimTypes.Role, "Reader"),
                new Claim("My_Custom_Claim", "My_Custom_Value")
            };

            //Create the token
            var token = new JwtSecurityToken(
                    issuer: "fredcom.com",    // who created and sign this token.
                    audience: "fredcom.com",  // who or what the token is intended for.
                    expires: DateTime.UtcNow.AddMinutes(15),
                    signingCredentials: signingCredentials,
                    claims: Claims
                );

            //Return the token
            var strToken = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(strToken);
        }
    }
}