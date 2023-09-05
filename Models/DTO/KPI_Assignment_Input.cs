using kpi_feedback_from_scratch.Models.Domain.KPI_Assignment;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System.ComponentModel.DataAnnotations;

namespace kpi_feedback_from_scratch.Models.DTO
{
    public class KPI_Assignment_Input 
    {
        
        public string? Year { get; set; }
        public int KPI_Id { get; set; }
        public int Employee_Id { get; set; }
        public int rating_frequency_id { get; set; }

        public int rating_type_id { get; set; }
    }
}
