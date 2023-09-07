using kpi_feedback_from_scratch.Models.Domain.Feedback_Notification;
using kpi_feedback_from_scratch.Models.Domain.KPI;
using kpi_feedback_from_scratch.Models.Domain.KPI_Assessor;
using kpi_feedback_from_scratch.Models.Domain.KPI_AssessorFeedback;
using kpi_feedback_from_scratch.Models.Domain.KPI_Assignment;
using kpi_feedback_from_scratch.Models.Domain.KPI_Assignment.Rating_frequency;
using kpi_feedback_from_scratch.Models.Domain.KPI_Assignment.Rating_type;

using kpi_feedback_from_scratch.Models.Domain.User;
using Microsoft.EntityFrameworkCore;

namespace kpi_feedback_from_scratch.data
{
    public class kpi_feedback_dbcontext : DbContext
    {
        public kpi_feedback_dbcontext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }
        public DbSet<KPI> kpi { get; set; }
        public DbSet<KPI_Category> kpi_category { get; set; }
        public DbSet<KPI_subcategory> kpi_subcategory {get; set;}
        public DbSet<KPI_status> kpi_status { get; set;}
        public DbSet<KPI_Assessor> kpi_assessor { get; set; }
        public DbSet<KPI_AssessorFeedback> kpi_assessor_feedback { get; set; }
        public DbSet<KPI_Assignment> kpi_assignment { get; set; }
        public DbSet<Rating_type> rating_type { get; set; }
        public DbSet<Rating_frequency> rating_frequency { get; set; }
        public DbSet<User> user { get; set; }   
        public DbSet<Division> division { get; set; }
        public DbSet<Grade> grade { get; set; } 
        public DbSet<Designation> designation { get; set; }
        public DbSet<Feedback_Notification> feedback_notification { get; set;}







    }
}
