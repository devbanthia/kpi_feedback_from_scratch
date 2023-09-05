using kpi_feedback_from_scratch.data;
using kpi_feedback_from_scratch.Models.Domain.KPI_Assessor;
using kpi_feedback_from_scratch.Models.Domain.KPI_Assignment;
using kpi_feedback_from_scratch.Models.Domain.User;
using kpi_feedback_from_scratch.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace kpi_feedback_from_scratch.Repositories
{
    public interface IKPI_Assessor_repository
    {
      
       
        public void add(KPI_Assignment kpi_assignment, User assessor);

        public void delete(KPI_Assessor kpi_assesor);
       

        public bool kpi_assessor_exists(int kpi_assignment_id, int kpi_assessor_id);
       

        public List<KPI_Assessor_View> get_kpi_assessor_view(int[] assessor_id);

        public List<KPI_Assessor> find_kpi_assessor(int[] assessor_id);

        public KPI_Assessor find_kpi_assessor(int assessor_id);

    }
}
