using kpi_feedback_from_scratch.data;
using kpi_feedback_from_scratch.Models.Domain.KPI_AssessorFeedback;
using kpi_feedback_from_scratch.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace kpi_feedback_from_scratch.Repositories
{
    public interface IKPI_Assessor_Feedback_repository
    {




        public List<Assessor_Feedback_View> get_all(int? authenticated_assessor_id);
        

        public KPI_AssessorFeedback get_by_id(int id);
      
        public void add_feedback(KPI_AssessorFeedback feedback);
      

        public void update_feedback(KPI_AssessorFeedback feedback);
       

        public void delete_feedback(int feedback_id);
        

        public Feedback_view get_feedback_view(KPI_AssessorFeedback feedback);

        public void enter_feedback_date(KPI_AssessorFeedback kpi_assessor_feedback);

        public DateTime? get_latest_feedback_by_assessor_id(int kpi_assessor_id);

        public bool feedback_already_exists(KPI_AssessorFeedback feedback);




    }
}
