using System.ComponentModel.DataAnnotations;

namespace kpi_feedback_from_scratch.Models.Domain.User
{
    public class Division
    {
        [Key]
        public int Id { get; set; } 
        public string Name { get; set; }

    }
}
