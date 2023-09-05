using System.ComponentModel.DataAnnotations;

namespace kpi_feedback_from_scratch.Models.Domain.KPI_Assignment
{
    public class KPI_Assignment
    {
        [Key]
        public int Id { get; set; }
        public string? Year { get; set; }
        public int KPI_Id { get; set; }

        public int KPI_CategoryId { get; set; }

        public int KPI_SubcategoryId { get; set; }

        public int Employee_Id { get; set; }

        public int Employee_Division_Id { get; set; }

        public int rating_frequency_id { get; set; }

        public int rating_type_id { get; set; } 

    }
}