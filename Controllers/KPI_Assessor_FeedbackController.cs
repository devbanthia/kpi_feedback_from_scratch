using kpi_feedback_from_scratch.Models.Domain.KPI_AssessorFeedback;
using kpi_feedback_from_scratch.Models.DTO;
using kpi_feedback_from_scratch.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace kpi_feedback_from_scratch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "assessor, admin")]
    public class KPI_Assessor_FeedbackController : ControllerBase
    {
        private IKPI_Assessor_Feedback_repository feedback_repository;
        private IUser_repository user_repository;
        public KPI_Assessor_FeedbackController(IKPI_Assessor_Feedback_repository _feedback_repository, IUser_repository _user_repository)
        {
            feedback_repository = _feedback_repository;
            user_repository = _user_repository;
        }

       [HttpGet]
        public IActionResult Get()
        {
            int user_id = user_repository.get_authenticated_user_id(HttpContext);
            return Ok(feedback_repository.get_all(user_id));
        }

        [HttpPost]
        public IActionResult Add(Assessor_Feedback_Input feedback)
        {
            KPI_AssessorFeedback assessor_feedback = new KPI_AssessorFeedback()
            { 
                KPI_AssessorId = feedback.KPI_AssessorId,
                rating_type_id = feedback.rating_type_id,
                rating = feedback.rating,
                AreaOfStrength = feedback.AreaOfStrength,
                Improvement = feedback.Improvement,
            };

            feedback_repository.enter_feedback_date(assessor_feedback);
            
            if(feedback_repository.feedback_already_exists(assessor_feedback))
            {
                return BadRequest("a feedback already exists for the current assessment period!");
            }

            feedback_repository.add_feedback(assessor_feedback);

            return Ok("feedback added successfully!");

        }

        [HttpPut("{id}")]

        public IActionResult Update(int id, Assessor_Feedback_Input updated_feedback)
        {
            KPI_AssessorFeedback feedback = feedback_repository.get_by_id(id);

            feedback.rating_type_id = updated_feedback.rating_type_id;
            feedback.rating = updated_feedback.rating;
            feedback.AreaOfStrength = updated_feedback.AreaOfStrength;
            feedback.Improvement = updated_feedback.Improvement;

            feedback_repository.update_feedback(feedback);

            return Ok("updated successfully!");

        }

        [HttpDelete]

        public IActionResult Delete(int id)
        {

            feedback_repository.delete_feedback(id);
            return Ok("hallelujah!");
        }
      


    }
}
