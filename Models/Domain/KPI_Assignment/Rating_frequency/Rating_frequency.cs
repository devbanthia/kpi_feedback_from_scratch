using System.ComponentModel.DataAnnotations;

namespace kpi_feedback_from_scratch.Models.Domain.KPI_Assignment.Rating_frequency
{
    public class Rating_frequency
    {
        [Key]
        public int Id { get; set; } 
        public string Name { get; set; }                
    }
}
