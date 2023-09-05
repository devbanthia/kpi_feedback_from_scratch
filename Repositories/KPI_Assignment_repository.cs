using kpi_feedback_from_scratch.data;
using kpi_feedback_from_scratch.Models.Domain.KPI_Assignment;
using kpi_feedback_from_scratch.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace kpi_feedback_from_scratch.Repositories
{
    public class KPI_Assignment_repository : IKPI_Assignment_repository
    {
        private kpi_feedback_dbcontext dbcontext;
        public KPI_Assignment_repository(kpi_feedback_dbcontext _dbcontext) 
        { 
            dbcontext = _dbcontext;
        }

        public List<KPI_Assignment_all_columns> get_all(List<KPI_Assignment> Kpi_assignment)
        {
            var query = from kpi_assignment in Kpi_assignment

                        join rating_type in dbcontext.rating_type on kpi_assignment.rating_type_id equals rating_type.Id
                        join rating_frequency in dbcontext.rating_frequency on kpi_assignment.rating_frequency_id equals rating_frequency.Id

                        join kpi_category in dbcontext.kpi_category on kpi_assignment.KPI_CategoryId equals kpi_category.Id
                        join kpi_subcategory in dbcontext.kpi_subcategory on kpi_assignment.KPI_SubcategoryId equals kpi_subcategory.Id 

                        join kpi in dbcontext.kpi on kpi_assignment.KPI_Id equals kpi.Id

                        join user in dbcontext.user on kpi_assignment.Employee_Id equals user.Id
                        join designation in dbcontext.designation on user.designationId equals designation.Id
                        join division in dbcontext.division on kpi_assignment.Employee_Division_Id equals division.Id

                        select new KPI_Assignment_all_columns
                        {
                            employee_name = user.Name,
                            division = division.Name,
                            designation = designation.Name,

                            KPI_Category = kpi_category.Category,
                            KPI_Subcategory = kpi_subcategory.Subcategory,
                            KPI_title = kpi.KPI_title,

                            rating_frequency_name = rating_frequency.Name,
                            rating_type_name = rating_type.Name,
                            Year = kpi_assignment.Year,

                            Employee_Id = user.Id,
                            KPI_Id = kpi.Id,
                            KPI_CategoryId = kpi_category.Id,

                            KPI_SubcategoryId = kpi_subcategory.Id,
                            rating_frequency_id = rating_frequency.Id,
                            rating_type_id = rating_type.Id


                        };

            return query.ToList();
        }

        public List<KPI_Assignment_View> create_KPI_Assignement_View_projection(List<KPI_Assignment_all_columns> Kpi_assignment)
        {
            var query = from kpi_assignment in Kpi_assignment
                        select new KPI_Assignment_View
                        {
                            employee_name = kpi_assignment.employee_name,
                            designation = kpi_assignment.designation,
                            division = kpi_assignment.division,
                            KPI_category = kpi_assignment.KPI_Category,
                            KPI_subcategory = kpi_assignment.KPI_Subcategory,
                            KPI_title = kpi_assignment.KPI_title,
                            rating_frequency = kpi_assignment.rating_frequency_name,
                            rating_type = kpi_assignment.rating_type_name
                        };
                return query.ToList() ;
        }

        public List<KPI_Assignment_View> get_by_filter(string? year, int? subcategory_id, int? category_id, int? kpi_id, int? employee_division_id, int? employee_id)
        {
            var query = dbcontext.kpi_assignment.ToList().AsQueryable();
            if(year != null)
            {
                query = query.Where(x => x.Year == year);   
            }

            if (subcategory_id != null)
            {
                query = query.Where(x => x.KPI_SubcategoryId== subcategory_id);
            }

            if (category_id != null)
            {
                query = query.Where(x => x.KPI_CategoryId == category_id);
            }

            if (employee_division_id != null)
            {
                query = query.Where(x => x.Employee_Division_Id == employee_division_id);
            }

            if (employee_id != null)
            {
                query = query.Where(x => x.Employee_Id == employee_id);
            }

            List<KPI_Assignment_all_columns> kpi_assignments_list = get_all(query.ToList());
            List<KPI_Assignment_View> kpi_assignment = create_KPI_Assignement_View_projection(kpi_assignments_list);

            return kpi_assignment.ToList();
        }

        public List<KPI_Assignment_View> get_by_filter(string? year, int[]? subcategory_id, int[]? category_id, int? kpi_id, int[]? employee_division_id, int? employee_id)
        {
            var query = (IQueryable<KPI_Assignment>)dbcontext.kpi_assignment;
            if (year != null)
            {
                query = query.Where(x => x.Year == year);
            }

            if (subcategory_id != null)
            {
                query = query.Where(x => subcategory_id.Contains(x.KPI_SubcategoryId) );
            }

            if (category_id != null)
            {
                query = query.Where(x => category_id.Contains(x.KPI_CategoryId));
            }

            if (employee_division_id != null)
            {
                query = query.Where(x => employee_division_id.Contains(x.Employee_Division_Id) );
            }

            if (employee_id != null)
            {
                query = query.Where(x => x.Employee_Id == employee_id);
            }

            List<KPI_Assignment_all_columns> kpi_assignments_list = get_all(query.ToList());
            List<KPI_Assignment_View> kpi_assignment = create_KPI_Assignement_View_projection(kpi_assignments_list);

            return kpi_assignment.ToList();
        }

        public KPI_Assignment get_kpi_assignment_by_id(int kpi_id, int employee_id)
        {
            return dbcontext.kpi_assignment.FirstOrDefault(x => (x.KPI_Id == kpi_id) && (x.Employee_Id == employee_id));
        }

        public KPI_Assignment get_kpi_assignment_by_id(int kpi_assignment_id)
        {
            return dbcontext.kpi_assignment.FirstOrDefault(x => x.Id == kpi_assignment_id);
        }



        public KPI_Assignment_View get_by_id(int id)
        {
           IQueryable<KPI_Assignment>  query = dbcontext.kpi_assignment.Where(x => x.Id == id);

            List<KPI_Assignment_all_columns> kpi_assignments_list = get_all(query.ToList());
            List<KPI_Assignment_View> kpi_assignment = create_KPI_Assignement_View_projection(kpi_assignments_list);

            return kpi_assignment.First();
        }

        public void add(KPI_Assignment assignment)
        {
            dbcontext.kpi_assignment.Add(assignment);
            dbcontext.SaveChanges();
        }

        public void update(KPI_Assignment assignment)
        {
            dbcontext.kpi_assignment.Update(assignment);
            dbcontext.SaveChanges();    
        }

        public void delete(KPI_Assignment assignment)
        {
            dbcontext.kpi_assignment.Remove(assignment);
            dbcontext.SaveChanges();
        }

        public bool kpi_assignment_exists(KPI_Assignment_Input kpi_assignment)
        {
            if (get_kpi_assignment_by_id(kpi_assignment.KPI_Id, kpi_assignment.Employee_Id) != null) 
            {
                return true;
            }
            return false;
        }


        public List<int>? get_distinct_id()
        {

            var query = dbcontext.kpi_assignment.Distinct().Select(x=> x.KPI_Id).ToList();
            query.Sort();

            return query;


        }

        public bool check_delete(int id)
        {
            if(dbcontext.kpi_assignment.Any(x => x.KPI_Id == id))
            {
                return true;
            }
            return false;
        }


    }
}
