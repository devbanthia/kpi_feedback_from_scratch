using System.ComponentModel.DataAnnotations;

namespace kpi_feedback_from_scratch.Models.Domain.KPI_Assignment.Rating_type
{
    public class Rating_type
    {
        [Key]
        public int Id { get; set; } 
        public string Name { get; set; }
    }
}
