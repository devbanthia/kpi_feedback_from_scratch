namespace kpi_feedback_from_scratch.Models.DTO
{
    public class KPI_Input
    {
        public int KPI_SubcategoryId { get; set; }
        public int KPI_CategoryId { get; set; }
        public string KPI_title { get; set; }
        public string KPI_description { get; set; }

        public int rating_frequency_id { get; set; }

        public int rating_type_id { get; set; }


    }
}
