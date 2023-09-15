using kpi_feedback_from_scratch.data;
using kpi_feedback_from_scratch.Models.Domain.KPI;
using kpi_feedback_from_scratch.Models.Domain.KPI_Assessor;
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
    public class KPI_AssignmentController : ControllerBase
    {
        private IKPI_Assessor_repository kpi_assessor_repository;
        private IKPI_Assignment_repository kpi_assignment_repository;
        private IKPI_repository kpi_repository;
        private IUser_repository user_repository;
        private kpi_feedback_dbcontext dbcontext;
        public KPI_AssignmentController(IKPI_Assessor_repository _kpi_assessor_repository, IKPI_Assignment_repository _kpi_assignment_repository, IKPI_repository _kpi_repository, kpi_feedback_dbcontext _dbcontext, IUser_repository _user_repository)
        {
            kpi_assessor_repository = _kpi_assessor_repository;
            kpi_assignment_repository = _kpi_assignment_repository;
            kpi_repository = _kpi_repository;
            dbcontext = _dbcontext;
            user_repository = _user_repository; 
        }



        [HttpPost]
        public IActionResult Create(List<KPI_Assignment_Input> kpi_assignments_input)
        {
            foreach(KPI_Assignment_Input kpi_assignment_input in kpi_assignments_input)
            { 

                if(kpi_assignment_repository.kpi_assignment_exists(kpi_assignment_input) == false)
                {
                    KPI_Assignment kpi_employee_pair = new KPI_Assignment();
                    KPI kpi = kpi_repository.get_kpi_by_id(kpi_assignment_input.KPI_Id);
                    User employee = user_repository.get_user(kpi_assignment_input.Employee_Id);

                    kpi_employee_pair.KPI_Id = kpi.Id;
                    kpi_employee_pair.KPI_CategoryId = kpi.KPI_CategoryId;
                    kpi_employee_pair.KPI_SubcategoryId = kpi.KPI_SubcategoryId;
                    kpi_employee_pair.rating_frequency_id = kpi_assignment_input.rating_frequency_id;
                    kpi_employee_pair.rating_type_id = kpi_assignment_input.rating_type_id;
                    kpi_employee_pair.Employee_Id = kpi_assignment_input.Employee_Id;
                    kpi_employee_pair.Employee_Division_Id = employee.divisionId;
                   
                    
                    kpi_assignment_repository.add(kpi_employee_pair);
                }
                else
                {
                    return BadRequest("the entered kpi-employee pair already exists!!");
                }

                return Ok("kpi assigned successfully");
 
            }

            return Ok();

        }

        [HttpGet]
        public IActionResult get_all(string? year, int? subcategory_id, int? category_id, int? kpi_id, int? employee_division_id, int? employee_id)
        {
            return Ok(kpi_assignment_repository.get_by_filter(year, subcategory_id, category_id, kpi_id, employee_division_id, employee_id));
        }


    }
}
