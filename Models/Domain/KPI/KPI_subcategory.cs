using System.ComponentModel.DataAnnotations;

namespace kpi_feedback_from_scratch.Models.Domain.KPI
{
    public class KPI_subcategory
    {
        [Key]
        public int Id { get; set; }
        public string Subcategory { get; set; }
    }
}
