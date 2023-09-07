using kpi_feedback_from_scratch.data;
using kpi_feedback_from_scratch.Models.Domain;
using kpi_feedback_from_scratch.Models.Domain.KPI_Assessor;
using kpi_feedback_from_scratch.Models.Domain.KPI_Assignment;

using kpi_feedback_from_scratch.Models.Domain.User;
using kpi_feedback_from_scratch.Models.DTO;
using kpi_feedback_from_scratch.Models.enums;
using System.Linq;

namespace kpi_feedback_from_scratch.Repositories
{
    public class KPI_Assessor_repository : IKPI_Assessor_repository
    {
        kpi_feedback_dbcontext dbcontext;
        IKPI_Assignment_repository kpi_assignment_repository;
        IUser_repository user_repository;


        public KPI_Assessor_repository(kpi_feedback_dbcontext _dbcontext, IUser_repository _user_repository, IKPI_Assignment_repository _kpi_assignment_repository)
        {
            dbcontext = _dbcontext;
            kpi_assignment_repository = _kpi_assignment_repository;
            user_repository = _user_repository;

        }

        public void add(KPI_Assignment kpi_assignment, User assessor)
        {

            KPI_Assessor kpi_assessor = new KPI_Assessor()
            {
                KPI_AssignmentId = kpi_assignment.Id,
                AssessorId = assessor.Id,
                AssessorStatus = "active",
                KPI_Assessment_Assign_Date = DateTime.Now,
                KPI_Assessment_Due_Date = CalculateDueDate(kpi_assignment.Id)

            };

            dbcontext.kpi_assessor.Add( kpi_assessor );
            dbcontext.SaveChanges();
            
        }

        public List<KPI_Assessor> find_kpi_assessor(int[] assessor_id)
        {
            var query = dbcontext.kpi_assessor.ToList().AsQueryable(); 

            if (assessor_id.Length == 0)
            {
                return query.ToList();
            }

            query = query.Where(x => assessor_id.Contains(x.AssessorId));
            return query.ToList();
        }

        public KPI_Assessor find_kpi_assessor(int assessor_id)
        {
            return dbcontext.kpi_assessor.FirstOrDefault(x => x.AssessorId == assessor_id);
        }

        public void delete(KPI_Assessor kpi_assesor)
        {
            dbcontext.kpi_assessor.Remove( kpi_assesor );
            dbcontext.SaveChanges();
        }

        public bool kpi_assessor_exists(int kpi_assignment_id, int kpi_assessor_id)
        {
            if(dbcontext.kpi_assessor.Where(x => (x.AssessorId == kpi_assessor_id) &&  (x.KPI_AssignmentId == kpi_assignment_id)).ToList().Count >0)
            {
                return true;
            }
            return false;
        }

        public DateTime CalculateDueDate(int kpi_assignment_id)
        {
            KPI_Assignment kpi_assignment = kpi_assignment_repository.get_kpi_assignment_by_id(kpi_assignment_id);

            if (kpi_assignment.rating_frequency_id == 0)
            {
                return DateTime.Now.AddMonths(1);
            }

            else if (kpi_assignment.rating_frequency_id == 1)
            {
                return DateTime.Now.AddMonths(3);
            }

            else if (kpi_assignment.rating_frequency_id == 2)
            {
                return DateTime.Now.AddMonths(6);
            }

            else
            {
                return DateTime.Now.AddMonths(12);
            }

        }



        public List<KPI_Assessor_View> get_kpi_assessor_view(int[] assessor_id)

        {

            //filters on the basis of assessor name/id
            List<KPI_Assessor> kpi_assessor = find_kpi_assessor( assessor_id);

            List<KPI_Assessor_View> kpi_assessors_list = new List<KPI_Assessor_View>();

            foreach(var ka  in kpi_assessor)
            {
                KPI_Assignment_View kpi_assignment = kpi_assignment_repository.get_by_id( ka.KPI_AssignmentId);
                EmployeeView employee = user_repository.get(ka.AssessorId).First();

                KPI_Assessor_View kpi_assessor_view = new KPI_Assessor_View()
                {
                    employee_name = kpi_assignment.employee_name,
                    designation = kpi_assignment.designation,
                    division = kpi_assignment.division,
                    KPI_category = kpi_assignment.KPI_category,
                    KPI_subcategory = kpi_assignment.KPI_subcategory,
                    KPI_title = kpi_assignment.KPI_title,
                    assessor_name = employee.Name,
                    assessor_designation = employee.Level,
                    assessor_division = employee.Division,
                    KPI_Assessment_Due_Date = ka.KPI_Assessment_Due_Date
   
                };

                if (DateTime.Now > ka.KPI_Assessment_Due_Date)
                {
                    kpi_assessor_view.status = assessor_status.pending.ToString();
                }
                else
                {
                    kpi_assessor_view.status = assessor_status.due.ToString();
                }

                kpi_assessors_list.Add(kpi_assessor_view);
                   
            }

            return kpi_assessors_list;


        }


    }
}
