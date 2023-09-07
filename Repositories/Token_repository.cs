using kpi_feedback_from_scratch.data;
using kpi_feedback_from_scratch.Models.Domain.User;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace kpi_feedback_from_scratch.Repositories
{
    public class Token_repository
    {
        private IConfiguration configuration;
        private kpi_feedback_dbcontext dbcontext;
        public Token_repository(IConfiguration _configuration, kpi_feedback_dbcontext _dbcontext)
        {
            configuration = _configuration;
            dbcontext = _dbcontext;
        }

        public string GenerateToken(User user )
        {
            var token_handler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

            var claims = new List<Claim>();

            string role;
            bool is_assessor = true;

            claims.Add(new Claim(ClaimTypes.Name, user.Id.ToString()));

            if (dbcontext.kpi_assessor.FirstOrDefault(x => x.AssessorId == user.Id) == null )
            {
                is_assessor = false;
            }
            
            if(user.divisionId == 6)
            {
                role = "admin";
            }

            else if(is_assessor)
            {
                role = "assessor";
            }
            else
            {
                role = "normal_user";
            }

            claims.Add(new Claim(ClaimTypes.Role, role));

            var token = new JwtSecurityToken(

            configuration["Jwt:Issuer"],
            configuration["Jwt:Audience"],
            claims,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)

            );

            return token_handler.WriteToken(token);



        }

    }
}
