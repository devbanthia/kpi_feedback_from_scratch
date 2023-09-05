using kpi_feedback_from_scratch.data;
using kpi_feedback_from_scratch.Models.Domain.KPI_Assessor;
using kpi_feedback_from_scratch.Models.Domain.KPI_AssessorFeedback;
using kpi_feedback_from_scratch.Models.Domain.KPI_Assignment;
using kpi_feedback_from_scratch.Models.DTO;

namespace kpi_feedback_from_scratch.Repositories
{
    public class KPI_Assessor_Feedback_Repository : IKPI_Assessor_Feedback_repository
    {
        private kpi_feedback_dbcontext dbcontext;
        private IKPI_Assessor_repository kpi_assessor_repository;
        private IUser_repository user_repository;

        public KPI_Assessor_Feedback_Repository(kpi_feedback_dbcontext _dbcontext, IKPI_Assessor_repository _kpi_assessor_repository, IUser_repository _user_repository)
        {
            dbcontext = _dbcontext;
            kpi_assessor_repository = _kpi_assessor_repository;
            user_repository = _user_repository;
        } 
        

        public List<Assessor_Feedback_View> get_all()
        {
         
            var query = dbcontext.kpi_assessor_feedback.ToList().AsQueryable();

 
            List<Assessor_Feedback_View> assessor_feedback = new List<Assessor_Feedback_View>();

            foreach(var feedback in query)
            {
                //for every feedback, extract assessor view and the feedback
                int assessor_id = dbcontext.kpi_assessor.FirstOrDefault(x => x.Id == feedback.KPI_AssessorId).AssessorId;
                int[] assessor_id_array = {assessor_id };
                
                KPI_Assessor_View assessor_view = kpi_assessor_repository.get_kpi_assessor_view(assessor_id_array).First();
                Feedback_view feedback_view = get_feedback_view(feedback);

                Assessor_Feedback_View assessor_feedback_item = new Assessor_Feedback_View()
                {
                    employee_name = assessor_view.employee_name,
                    designation = assessor_view.designation,
                    division = assessor_view.division,
                    KPI_category = assessor_view.KPI_category,
                    KPI_subcategory = assessor_view.KPI_subcategory,
                    KPI_title = assessor_view.KPI_title,
                    assessor_name = assessor_view.employee_name,
                    assessor_designation = assessor_view.assessor_designation,
                    assessor_division = assessor_view.assessor_division,

                    rating_type = feedback_view.rating_type,
                    rating = feedback_view.rating,
                    AreaOfStrength = feedback_view.AreaOfStrength,
                    Improvement = feedback_view.Improvement
                };

                assessor_feedback.Add(assessor_feedback_item);

            }

            return assessor_feedback;    
        }

        public KPI_AssessorFeedback get_by_id(int id)
        {
            return dbcontext.kpi_assessor_feedback.FirstOrDefault(x => x.Id == id);
        }
        public void add_feedback(KPI_AssessorFeedback feedback)
        {
            dbcontext.kpi_assessor_feedback.Add(feedback);
            dbcontext.SaveChanges();
        }

        public void update_feedback(KPI_AssessorFeedback feedback)
        {
            dbcontext.kpi_assessor_feedback.Update(feedback);
            dbcontext.SaveChanges();
        }

        public void delete_feedback(int feedback_id)
        {
            KPI_AssessorFeedback feedback = dbcontext.kpi_assessor_feedback.Where(x => x.Id == feedback_id).FirstOrDefault();   
            dbcontext.kpi_assessor_feedback.Remove(feedback);
            dbcontext.SaveChanges();
        }

        public Feedback_view get_feedback_view(KPI_AssessorFeedback feedback)
        {
            Feedback_view feedback_view = new Feedback_view()
            {
                rating = feedback.rating,
                AreaOfStrength = feedback.AreaOfStrength,
                Improvement = feedback.Improvement,
                rating_type = dbcontext.rating_type.FirstOrDefault(x => x.Id == feedback.Id).Name

            };

            return feedback_view;
        }





    }
}
