using kpi_feedback_from_scratch.data;
using kpi_feedback_from_scratch.Models.Domain.User;
using kpi_feedback_from_scratch.Models.DTO;
using System.Linq;


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
      
    }
}
