using System.ComponentModel.DataAnnotations;

namespace kpi_feedback_from_scratch.Models.Domain.KPI_Assessor
{
    public class KPI_Assessor 
    {
        [Key]
        public int Id { get; set; } 
        public int KPI_AssignmentId { get; set; }

        public int AssessorId { get; set; }

        public string AssessorStatus { get; set; }

        public DateTime KPI_Assessment_Assign_Date { get; set; }

        public DateTime KPI_Assessment_Due_Date { get; set; }   


    }
}
