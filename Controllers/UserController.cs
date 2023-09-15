using kpi_feedback_from_scratch.Models.Domain.User;
using kpi_feedback_from_scratch.Models.DTO;
using kpi_feedback_from_scratch.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace kpi_feedback_from_scratch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUser_repository user_repository;
        private IToken_repository token_repository;
        public UserController(IUser_repository _user_repository, IToken_repository _token_repository) 
        {
            user_repository = _user_repository;
            token_repository = _token_repository;
        }

        [HttpPost("/register")]
        public IActionResult Register([FromBody] UserRegister user)
        {
            if(user_repository.validate_user(user)== false)
            {
                return BadRequest("the user already exists!");
            }

            User new_user = new User()
            {
                Name = user.name,
                divisionId = user.divisionId,
                designationId = user.designationId,
                password = user.password

            };

            user_repository.add(new_user);
            return Ok(new_user);
        }

        [HttpPost("/login")]
        public IActionResult Login([FromBody] UserLogin user)
        {
            if(user_repository.login_validation(user)==false)
            {
                return BadRequest("you are not registered yet! please register first");
            }
            int user_id = user_repository.get_id_by_filter(user.name, user.divisionId, user.designationId);
            User new_user = new User()

            {
                Id = user_id,
                Name = user.name,
                divisionId = user.divisionId,
                designationId = user.designationId,
                password = user.password

            };

            string token = token_repository.GenerateToken(new_user);
            return Ok(token);



        }


    }
}
