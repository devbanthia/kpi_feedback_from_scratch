using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace kpi_feedback_from_scratch.Models.DTO
{
    public class KPI_Assignment_View
    {
        public string employee_name { get; set; }
        public string division { get; set; }
        public string designation { get; set; } 
        public string KPI_category { get; set; }    
        public string KPI_subcategory { get; set; } 

        public string KPI_title { get; set; }
        public string rating_type { get; set; }
        public string rating_frequency { get; set; }

    }
}
