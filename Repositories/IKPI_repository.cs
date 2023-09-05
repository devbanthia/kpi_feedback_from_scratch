using kpi_feedback_from_scratch.Models.Domain.KPI;
using kpi_feedback_from_scratch.Models.Domain.KPI_Assignment;
using kpi_feedback_from_scratch.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace kpi_feedback_from_scratch.Repositories
{
    public interface IKPI_repository
    {
        public List<KPI_view> create_kpi_view_from_kpi(List<KPI> kpi_query);
        public List<KPI_view> get_all(int? category_id, int? subcategory_id, bool show_hidden);
       
        public List<KPI_view> get_all(int[] category_id, int[] subcategory_id, bool show_hidden);
       
        public KPI_view? get_by_id(int id);

        public KPI get_kpi_by_id(int id);

        public bool kpi_title_exists(KPI kpi);
        

        public bool add(KPI_Input kpi);
        
           
        public void update(KPI kpi);
       

        public void delete(KPI kpi);

        public List<KPI> get_kpi();

        public List<KPI> get_all_kpi(int? category_id, int? subcategory_id);


    }
}
