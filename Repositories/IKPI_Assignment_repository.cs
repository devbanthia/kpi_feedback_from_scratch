using kpi_feedback_from_scratch.Models.Domain.KPI_Assignment;
using kpi_feedback_from_scratch.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace kpi_feedback_from_scratch.Repositories
{
    public interface IKPI_Assignment_repository
    {

        public List<KPI_Assignment_all_columns> get_all(List<KPI_Assignment> Kpi_assignment);


        public KPI_Assignment get_kpi_assignment_by_id(int kpi_id, int employee_id);

        public List<KPI_Assignment_View> create_KPI_Assignement_View_projection(List<KPI_Assignment_all_columns> Kpi_assignment);


        public List<KPI_Assignment_View> get_by_filter(string? year, int? subcategory_id, int? category_id, int? kpi_id, int? employee_division_id, int? employee_id);

        public KPI_Assignment get_kpi_assignment_by_id(int kpi_assignment_id);

        public List<int>? get_distinct_id();

        public List<KPI_Assignment_View> get_by_filter(string? year, int[]? subcategory_id, int[]? category_id, int? kpi_id, int[]? employee_division_id, int? employee_id);

        public KPI_Assignment_View get_by_id(int id);

        public void add(KPI_Assignment assignment);

        public bool check_delete(int id);

        public void update(KPI_Assignment assignment);

        public void delete(KPI_Assignment assignment);


        public bool kpi_assignment_exists(KPI_Assignment_Input kpi_assignment);
   
         

    }
}
