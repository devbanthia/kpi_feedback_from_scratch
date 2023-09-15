namespace kpi_feedback_from_scratch.Models.DTO
{
    public class Assessor_Feedback_View : KPI_Assessor_View
    {
        public string rating_type { get; set; }

        public string? rating { get; set; }

        public string? AreaOfStrength { get; set; }

        public string? Improvement { get; set; }

        public string? Assessment_period { get; set; }
    }

}
