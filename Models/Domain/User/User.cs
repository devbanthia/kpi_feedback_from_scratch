using System.ComponentModel.DataAnnotations;

namespace kpi_feedback_from_scratch.Models.Domain.User
{
    public class User
    {
        [Key]
        public int Id { get; set; } 
        public string Name { get; set; }    
        public int designationId { get; set; }    
        public int divisionId { get; set; }
        public int? gradeId { get; set; }

        public string password { get; set; }


    }
}
