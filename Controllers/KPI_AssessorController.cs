using kpi_feedback_from_scratch.Models.Domain.KPI_Assignment;
using kpi_feedback_from_scratch.Models.Domain.User;
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
    //[Authorize(Roles = "admin")]
    public class KPI_AssessorController : ControllerBase
    {
        private IKPI_Assessor_repository kpi_assessor_repository;
        private IUser_repository user_repository;
        private IKPI_Assignment_repository kpi_assignment_repository;
        public KPI_AssessorController(IKPI_Assessor_repository _kpi_assessor_repository, IUser_repository _user_repository, IKPI_Assignment_repository _kpi_assignment_repository)
        {
            kpi_assessor_repository = _kpi_assessor_repository;
            user_repository = _user_repository;
            kpi_assignment_repository = _kpi_assignment_repository;

        }

        [HttpGet]
        public IActionResult Get_KPI_Assessor_View([FromQuery]int[] assessor_id ) 
        {
            return Ok(kpi_assessor_repository.get_kpi_assessor_view(assessor_id));
        }

        [HttpPost]
        public IActionResult Assign_Assessor(KPI_Assessor_Input kpi_assessor)
        {
            User assessor = user_repository.get_user(kpi_assessor.kpi_assessor_id);
            KPI_Assignment kpi_assignment = kpi_assignment_repository.get_kpi_assignment_by_id(kpi_assessor.kpi_assignment_id);

            kpi_assessor_repository.add(kpi_assignment, assessor);

            return Ok("kpi assigned to assessor successfully!");

        }
    }
}
