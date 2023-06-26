using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebApi.Token
{
    public class JwtSecurityKey
    {
        public static SymmetricSecurityKey CreateToken(string key)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
        }
    }
}
