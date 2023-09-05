using kpi_feedback_from_scratch.Models.Domain.User;
using kpi_feedback_from_scratch.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace kpi_feedback_from_scratch.Repositories
{
    public interface IUser_repository
    {
        public List<EmployeeView> get(int? id);


        public User get_user(int id);
       
    }
}
