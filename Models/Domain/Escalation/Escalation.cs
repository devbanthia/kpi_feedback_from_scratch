using System.ComponentModel.DataAnnotations;

namespace kpi_feedback_from_scratch.Models.Domain.Escalation
{
    public class Escalation
    {
        [Key]
        public int Id { get; set; }
        public int assessorId { get; set; }
        public int escalations { get; set; }
    }
}
