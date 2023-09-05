using kpi_feedback_from_scratch.Models.Domain.KPI;
using kpi_feedback_from_scratch.Models.DTO;
using kpi_feedback_from_scratch.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace kpi_feedback_from_scratch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KPIController : ControllerBase
    {
        private IKPI_repository kpi_repository;
        private IKPI_Assignment_repository kpi_assignment_repository;
        public KPIController(IKPI_repository _kpi_repository, IKPI_Assignment_repository _kpi_assignment_repository)
        {
            kpi_repository = _kpi_repository;
            kpi_assignment_repository = _kpi_assignment_repository;
        }

        [HttpGet("/unassigned_kpi")]
        public IActionResult Get_Unassigned_KPIs([FromQuery]int? category_id, [FromQuery] int? subcategory_id)
        {
            List<KPI> all_kpis = kpi_repository.get_all_kpi(category_id, subcategory_id); 
            List<int> unique_kpi_assignment_id = kpi_assignment_repository.get_distinct_id();

            List<KPI> unassigned_kpi = new List<KPI>();

            int i = 0;
            int j = 0;

            while(j < unique_kpi_assignment_id.Count)
            {
                if (all_kpis[i].Id != unique_kpi_assignment_id[j] )
                {
                    unassigned_kpi.Add(all_kpis[i]);
                    i++;
                }
                else
                {
                    i++;
                    j++;
                }

            }
            while(i < all_kpis.Count)
            {
                unassigned_kpi.Add(all_kpis[i]);
                i++;
            }

            List<KPI_view> unassigned_kpi_view = kpi_repository.create_kpi_view_from_kpi(unassigned_kpi);
            return Ok(unassigned_kpi_view);



        }

        [HttpPost]
        public IActionResult Create(KPI_Input kpi)
        {
            KPI new_kpi = new KPI()
            {
                KPI_title = kpi.KPI_title,
                KPI_description = kpi.KPI_description,
                KPI_CategoryId = kpi.KPI_CategoryId,
                KPI_SubcategoryId = kpi.KPI_SubcategoryId



            };

            if(kpi_repository.kpi_title_exists(new_kpi))
            {

                return BadRequest("the kpi entered already exists!!");
            }

            else
            {

                kpi_repository.add(kpi);
                return Ok("kpi added successfully!");

            }
        }
        [HttpGet]
        public IActionResult get_all(int? category_id, int? subcategory_id, bool showhidden)
        {
            return Ok(kpi_repository.get_all(category_id, subcategory_id, showhidden));
        }

        [HttpDelete("/{id}")]
        public IActionResult Delete([FromQuery]int id) 
        { 
            if(kpi_assignment_repository.check_delete(id))
            {
                return BadRequest("the kpi is already linked with an assessee, so it can not be deleted!");
            }

            KPI kpi_to_be_deleted = kpi_repository.get_kpi_by_id(id);
            kpi_repository.delete(kpi_to_be_deleted);

            return Ok("hallelujah!");
 
        }

        [HttpPut("/{id}")]
        public IActionResult Update([FromQuery] int id, KPI_Input updated_kpi)
        {
            KPI kpi_to_be_updated = kpi_repository.get_kpi_by_id(id);

            kpi_to_be_updated.KPI_description = updated_kpi.KPI_description;
            kpi_to_be_updated.KPI_title = updated_kpi.KPI_title;
            kpi_to_be_updated.rating_frequency_id = updated_kpi.rating_frequency_id;
            kpi_to_be_updated.rating_type_id = updated_kpi.rating_type_id;

            if(kpi_repository.kpi_title_exists(kpi_to_be_updated))
            {
                return Ok("there is already an existing kpi with the details entered!!");
            }

            kpi_repository.update(kpi_to_be_updated);

            return Ok("the kpi was updated successfully!");

            
        }

    }
}
