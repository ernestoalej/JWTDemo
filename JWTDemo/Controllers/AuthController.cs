using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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

            //Create the token

            var token = new JwtSecurityToken(
                    issuer: "fredcom.com",
                    audience: "fredcom.com",
                    expires: DateTime.UtcNow.AddMinutes(1),
                    signingCredentials: signingCredentials
                );

            //Return the token
            var strToken = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(strToken);
        }
    }
}