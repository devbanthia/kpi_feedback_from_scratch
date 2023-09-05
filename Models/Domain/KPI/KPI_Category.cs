using System.ComponentModel.DataAnnotations;

namespace kpi_feedback_from_scratch.Models.Domain.KPI
{
    public class KPI_Category
    {
        [Key]
        public int Id { get; set; }
        public string Category { get; set; }
    }
}
