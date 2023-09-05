using System.ComponentModel.DataAnnotations;

namespace kpi_feedback_from_scratch.Models.Domain.KPI_AssessorFeedback
{
    public class KPI_AssessorFeedback
    {
        [Key]
        public int Id { get; set; } 

        public int KPI_AssessorId { get; set; } 

        public int rating_type_id { get; set; }

        public string? rating { get; set; }

        public string? AreaOfStrength { get; set; }

        public string? Improvement { get; set; } 

    }
}
