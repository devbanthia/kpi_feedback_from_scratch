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
        private IKPI_Assignment_repository kpi_assignment_repository;
        private IUser_repository user_repository;

        public KPI_Assessor_Feedback_Repository(kpi_feedback_dbcontext _dbcontext, IKPI_Assessor_repository _kpi_assessor_repository, IUser_repository _user_repository, IKPI_Assignment_repository _kpi_assignment_repository)
        {
            dbcontext = _dbcontext;
            kpi_assessor_repository = _kpi_assessor_repository;
            user_repository = _user_repository;
            kpi_assignment_repository = _kpi_assignment_repository;
        } 
        

        public List<Assessor_Feedback_View> get_all(int? authenticated_assessor_id)
        {
            int[] kpi_assessor_id;
            if (authenticated_assessor_id != null)
            {
                kpi_assessor_id = dbcontext.kpi_assessor.Where(x => x.AssessorId == authenticated_assessor_id).Select(x => x.Id).ToArray();
            }

            else
            {
                kpi_assessor_id = dbcontext.kpi_assessor.Select(x => x.Id).ToArray();
            }


            var query = dbcontext.kpi_assessor_feedback.ToList().AsQueryable();
            query = query.Where(x => kpi_assessor_id.Contains(x.KPI_AssessorId));

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
                    assessor_name = assessor_view.assessor_name,
                    assessor_designation = assessor_view.assessor_designation,
                    assessor_division = assessor_view.assessor_division,

                    rating_type = feedback_view.rating_type,
                    rating = feedback_view.rating,
                    AreaOfStrength = feedback_view.AreaOfStrength,
                    Improvement = feedback_view.Improvement,
                    Assessment_period = feedback_view.Assessment_Period
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
                rating_type = dbcontext.rating_type.FirstOrDefault(x => x.Id == feedback.Id).Name,

            };

            var list = dbcontext.kpi_assessor_feedback.Where(x => x.KPI_AssessorId == feedback.KPI_AssessorId).OrderByDescending(x => x.Next_Feedback_Date).Select(x => x.Next_Feedback_Date).ToList();

            int year1, year2, month1, month2;

            year2 = ((DateTime)list[0]).Year;
         
            month2 = ((DateTime)list[0]).Month;
            


            if (list.Count == 1)
            {
                year1 = dbcontext.kpi_assessor.FirstOrDefault(x => x.Id == feedback.KPI_AssessorId).KPI_Assessment_Assign_Date.Year;
                month1 = dbcontext.kpi_assessor.FirstOrDefault(x => x.Id == feedback.KPI_AssessorId).KPI_Assessment_Assign_Date.Month;
            }

            else
            {
                year1 = ((DateTime)list[1]).Year;
                month1 = ((DateTime)list[1]).Month;
            }


            string assessment_period = $"{year1}/{month1} to {year2}/{month2}";
            feedback_view.Assessment_Period = assessment_period;

            return feedback_view;
        }

        public DateTime? get_latest_feedback_by_assessor_id(int kpi_assessor_id)
        {
            var query = dbcontext.kpi_assessor_feedback.ToList();
            if(query.Count == 0)
            {
                return null;
            }
            query = query.Where(x => x.KPI_AssessorId ==  kpi_assessor_id).ToList();
            if(query.Count == 0)
            {
                return null;
            }

            return query.OrderByDescending(x => x.Next_Feedback_Date).FirstOrDefault().Next_Feedback_Date;  
        }

        public void enter_feedback_date(KPI_AssessorFeedback kpi_assessor_feedback)
        {
 
            KPI_Assessor kpi_assessor = kpi_assessor_repository.find_kpi_assessor(kpi_assessor_feedback.KPI_AssessorId);

            TimeSpan rating_period = kpi_assessor_repository.calculate_rating_period(kpi_assessor.KPI_AssignmentId);
           

            if (get_latest_feedback_by_assessor_id(kpi_assessor.Id) == null)
            {
                kpi_assessor_feedback.Next_Feedback_Date = kpi_assessor.KPI_Assessment_Assign_Date + rating_period;
            }

            else
            {
                kpi_assessor_feedback.Next_Feedback_Date = get_latest_feedback_by_assessor_id(kpi_assessor.Id) + rating_period;
            }

        }

        public bool feedback_already_exists(KPI_AssessorFeedback feedback)
        {
            DateTime? last_feedback_date = get_latest_feedback_by_assessor_id(feedback.KPI_AssessorId);
            KPI_Assessor kpi_assessor = kpi_assessor_repository.find_kpi_assessor(feedback.KPI_AssessorId);
            
            if(last_feedback_date != null)
            {
                DateTime due_date = (DateTime)(last_feedback_date + kpi_assessor_repository.calculate_rating_period(kpi_assessor.KPI_AssignmentId));
                if(DateTime.Now < due_date)
                {
                    return true;
                }
            }
            return false;
            
        }




    }
}
