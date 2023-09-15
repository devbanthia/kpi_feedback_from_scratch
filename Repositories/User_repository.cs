using kpi_feedback_from_scratch.data;
using kpi_feedback_from_scratch.Models.Domain.User;
using kpi_feedback_from_scratch.Models.DTO;
using System.Linq;
using System.Security.Claims;

namespace kpi_feedback_from_scratch.Repositories
{
    public class User_repository : IUser_repository
    {
        private kpi_feedback_dbcontext dbcontext;
        public User_repository(kpi_feedback_dbcontext _dbcontext)
        {
            dbcontext = _dbcontext;
        }

        public List<EmployeeView> get(int? id)
        {
            var query = dbcontext.user.AsQueryable();

            if(id != null)
            {
                query = query.Where(x => x.Id == id);
            }



            var new_query = from user in query
                        join division in dbcontext.division on user.divisionId equals division.Id
                        join designation in dbcontext.designation on user.designationId equals designation.Id
                        select new EmployeeView
                        {
                            Name = user.Name,
                            Division = division.Name,
                            Level = designation.Name
                        };

            List<EmployeeView> list_of_employees = new_query.ToList();

          

            return list_of_employees;
        }

        public User get_user(int id)
        {
            return dbcontext.user.FirstOrDefault(x => x.Id == id);
        }

        public int get_id_by_filter(string name, int divisionId, int designationId)
        {
            User user = dbcontext.user.FirstOrDefault(x => (x.Name==name) && (x.divisionId==divisionId) && (x.designationId==designationId));
            return user.Id;
        }

        public int get_authenticated_user_id(HttpContext context)
        {
            var claims = context.User.Claims;
            string id = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;

            int.TryParse(id, out int id_int);

            return id_int;  
             
        }

        public string get_authenticated_user_role(HttpContext context)
        {
            var claims = context.User.Claims;
            string user_role = claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;

            return user_role;
        }

        public bool validate_user(UserRegister user)
        {
            if (dbcontext.user.FirstOrDefault(x => (x.Name==user.name) && (x.divisionId == user.divisionId) && (x.designationId== user.designationId)) == null)
            {
                return true;
            }
            return false;

        }

        public bool login_validation(UserLogin user)
        {
            if (dbcontext.user.FirstOrDefault(x => (x.Name == user.name) && (x.divisionId == user.divisionId) && (x.designationId == user.designationId)) == null)
            {
                return false;
            }
            return true;

        }

        public void add(User user)
        {
            dbcontext.user.Add(user);
            dbcontext.SaveChanges();
        }

    }
}
