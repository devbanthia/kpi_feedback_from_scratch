using System.ComponentModel.DataAnnotations;

namespace kpi_feedback_from_scratch.Models.Domain.KPI
{
    public class KPI
    {
        [Key]
        public int Id { get; set; }
        public int KPI_SubcategoryId { get; set; }
        public int KPI_CategoryId { get; set; }
        public string KPI_title { get; set; }
        public string KPI_description { get; set; }
        public int KPI_statusId { get; set; }
        public bool is_deleted { get; set; }

        public int rating_frequency_id { get; set; }

        public int rating_type_id { get; set; }
    }
}
