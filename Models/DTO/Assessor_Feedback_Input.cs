namespace kpi_feedback_from_scratch.Models.DTO
{
    public class Assessor_Feedback_Input
    {
        public int KPI_AssessorId { get; set; }

        public int rating_type_id { get; set; }

        public string? rating { get; set; }

        public string? AreaOfStrength { get; set; }

        public string? Improvement { get; set; }
    }
}
