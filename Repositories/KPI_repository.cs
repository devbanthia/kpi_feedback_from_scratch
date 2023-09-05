using kpi_feedback_from_scratch.data;
using kpi_feedback_from_scratch.Models.Domain.KPI;
using kpi_feedback_from_scratch.Models.Domain.KPI_Assignment;
using kpi_feedback_from_scratch.Models.DTO;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Cmp;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace kpi_feedback_from_scratch.Repositories
{
    public class KPI_repository : IKPI_repository
    {
        kpi_feedback_dbcontext dbcontext;
        public KPI_repository(kpi_feedback_dbcontext _dbcontext)
        {
            dbcontext = _dbcontext;
        }

        public List<KPI_view> create_kpi_view_from_kpi(List<KPI> kpi_query)
        {
          

            var new_query = from kpi in kpi_query
                            join kpi_cat in dbcontext.kpi_category on kpi.KPI_CategoryId equals kpi_cat.Id
                            join kpi_subcat in dbcontext.kpi_subcategory on kpi.KPI_SubcategoryId equals kpi_subcat.Id
                        


                            select new KPI_view
                            {
                                KPI_Category = kpi_cat.Category,
                                KPI_Subcategory = kpi_subcat.Subcategory,
                                KPI_description = kpi.KPI_description,
                                KPI_title = kpi.KPI_title
                            };

            return new_query.ToList();
        }

        public List<KPI_view> get_all(int? category_id, int? subcategory_id, bool show_hidden)
        {
            var query = dbcontext.kpi.AsNoTracking();

            if (category_id != null)
            {
                query = dbcontext.kpi.Where(x => x.KPI_CategoryId == category_id);
            }

            if(subcategory_id != null)
            {
                query = query.Where(x => x.KPI_SubcategoryId == subcategory_id);
            }
            

            if (!show_hidden)
            {
                query = query.Where(x => x.is_deleted == false);
            }

          
            
            List<KPI_view> kpi_view = create_kpi_view_from_kpi(query.ToList());
            return kpi_view;

        }

        public List<KPI_view> get_all(int[] category_id, int[] subcategory_id, bool show_hidden)
        {
            var query = dbcontext.kpi.Where(x => category_id.Contains(x.KPI_CategoryId));
            query = query.Where(x => subcategory_id.Contains(x.KPI_SubcategoryId));

            if (!show_hidden)
            {
                query = query.Where(x => x.is_deleted == false);
            }

            List<KPI_view> kpi_view = create_kpi_view_from_kpi(query.ToList());
            return kpi_view;
        }

        public KPI_view? get_by_id(int id)
        {
            IQueryable<KPI> kpi = dbcontext.kpi.Where(x => x.Id == id);
            List<KPI_view> kpi_view = create_kpi_view_from_kpi(kpi.ToList());

            return kpi_view.FirstOrDefault();

        }

        public KPI get_kpi_by_id(int id)
        {
            return dbcontext.kpi.FirstOrDefault(x => x.Id == id);
        }

        public bool kpi_title_exists(KPI kpi)
        {
            var kpi_list = get_all(kpi.KPI_CategoryId, kpi.KPI_SubcategoryId, false);
         
            foreach(KPI_view _kpi in kpi_list)
            {
                if(_kpi.KPI_title == kpi.KPI_title)
                {
                    return true;
                }
            }

            return false;
        }

        public bool add(KPI_Input kpi)
        {
           
             KPI new_kpi = new KPI()
             {
                    KPI_title = kpi.KPI_title,
                    KPI_CategoryId = kpi.KPI_CategoryId,
                    KPI_SubcategoryId = kpi.KPI_SubcategoryId,
                    KPI_description = kpi.KPI_description,
                    rating_frequency_id = kpi.rating_frequency_id,
                    rating_type_id = kpi.rating_type_id,
                    KPI_statusId = 0,
                    is_deleted = false,

             };
             if(kpi_title_exists(new_kpi))
             {
                 return false;
             }

             dbcontext.kpi.Add(new_kpi);
             dbcontext.SaveChanges();

              return true;
         }

    

        public void update(KPI kpi)
        {
            dbcontext.kpi.Update(kpi);
            dbcontext.SaveChanges();
        }

        public void delete(KPI kpi)
        {
            kpi.is_deleted = true;

            dbcontext.kpi.Update(kpi);
            dbcontext.SaveChanges();
        }

        public List<KPI> get_kpi()
        {
            return dbcontext.kpi.ToList();
        }

        public List<KPI> get_all_kpi(int? category_id, int? subcategory_id)
        {
            var query = dbcontext.kpi.ToList().AsQueryable();

            if (category_id != null)
            {
                query = query.Where(x => x.KPI_CategoryId == category_id);
            }
            if (subcategory_id != null)
            {
                query = query.Where(x => x.KPI_SubcategoryId == subcategory_id);
            }

            return query.ToList();

        }
    }
}

