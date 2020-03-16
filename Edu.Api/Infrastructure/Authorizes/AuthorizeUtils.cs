using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Edu.Api.Infrastructure.Authorizes
{
    public class AuthorizeUtils
    {
        static string AudienceId = "099153c2625149bc8ecb3e85e03f0022";
        static string SecretKey = "IxrAjDoa2FqElO7IhrSrUJELhUckePEPVpaePlS_Xaw";
        static string IssUser = "A5FGDE64-E84D-485A-BE51-56E293D09A69";

        public static string Serialize<T>(T userToken, string loginType = "")
        {
            var keyByteArray = Base64UrlTextEncoder.Decode(SecretKey);
            var userDataStr = JsonSerializer.SerializeToString(userToken);
            var claimCollection = new List<Claim> {
                  new Claim(ClaimTypes.UserData, userDataStr),
             };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claimCollection),
                Issuer = IssUser,
                Audience = AudienceId,
                //Expires = DateTime.Now.AddMinutes(30),
                Expires = loginType == "app" ? DateTime.Now.AddMonths(1) : DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyByteArray), SecurityAlgorithms.HmacSha256)
            };
            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(securityToken);
        }

        public static bool Validate(string token)
        {
            try
            {
                var keyByteArray = Base64UrlTextEncoder.Decode(SecretKey);
                var validationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(keyByteArray),
                    ValidIssuer = IssUser,
                    ValidAudience = AudienceId
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                IdentityModelEventSource.ShowPII = true;
                SecurityToken validatedToken;
                var claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
                return claimsPrincipal.Identity.IsAuthenticated;
            }
            catch
            {
                return false;
            }

        }

        public static T GetCurUser<T>(string token)
        {
            try
            {
                var jsToken = new JwtSecurityToken(token);
                var userDataJson = jsToken.Claims.FirstOrDefault(t => t.Type == ClaimTypes.UserData);
                var userDatas = JsonSerializer.DeserializeFromString<T>(userDataJson.Value);
                return userDatas;
            }
            catch
            {
                throw new Exception("非法Token");
            }
        }
    }
}
