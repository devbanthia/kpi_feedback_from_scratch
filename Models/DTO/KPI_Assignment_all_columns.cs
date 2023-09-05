namespace kpi_feedback_from_scratch.Models.DTO
{
    public class KPI_Assignment_all_columns 
    {


        public string? Year { get; set; }
        public int KPI_Id { get; set; }

        public int KPI_SubcategoryId { get; set; }
        public int KPI_CategoryId { get; set; }
        public string KPI_Subcategory { get; set; }


        public string KPI_Category { get; set; }
        public string KPI_title { get; set; }
        public int Employee_Id { get; set; }


        public string employee_name { get; set; }
        public string designation { get; set; }
        public string division { get; set; }


        public int rating_frequency_id { get; set; }
        public string rating_frequency_name { get; set; }
        public int rating_type_id { get; set; }


        public string rating_type_name { get;set; }
    }
}
