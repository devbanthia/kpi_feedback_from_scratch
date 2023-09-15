using kpi_feedback_from_scratch.Models.Domain.User;
using kpi_feedback_from_scratch.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace kpi_feedback_from_scratch.Repositories
{
    public interface IUser_repository
    {
        public List<EmployeeView> get(int? id);


        public User get_user(int id);

        public int get_authenticated_user_id(HttpContext context);


        public string get_authenticated_user_role(HttpContext context);

        public bool validate_user(UserRegister user);

        public void add(User user);

        public bool login_validation(UserLogin user);

        public int get_id_by_filter(string name, int divisionId, int designationId);





    }
}
