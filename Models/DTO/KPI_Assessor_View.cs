namespace kpi_feedback_from_scratch.Models.DTO
{
    public class KPI_Assessor_View : KPI_Assignment_View
    {
        public string assessor_name { get; set; }
        public string assessor_division { get; set; }
        public string assessor_designation { get; set; }
        public DateTime KPI_Assessment_Due_Date { get; set; }

        public string status { get; set; }





    }
}
