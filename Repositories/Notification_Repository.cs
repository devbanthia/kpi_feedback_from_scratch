using kpi_feedback_from_scratch.data;
using kpi_feedback_from_scratch.Models.Domain.Feedback_Notification;

namespace kpi_feedback_from_scratch.Repositories
{
    public class Notification_Repository : INotification_repository
    {
        private kpi_feedback_dbcontext dbcontext;
        public Notification_Repository(kpi_feedback_dbcontext _dbcontext) 
        {
            dbcontext = _dbcontext;
        }

        public void add(Feedback_Notification notification)
        {
            dbcontext.Add(notification);
            dbcontext.SaveChanges();
        }

        public void delete(Feedback_Notification notification)
        {
            dbcontext.Remove(notification);
            dbcontext.SaveChanges();
        }

        public List<Feedback_Notification> get_by_receiver(int receiverId)
        {
            return dbcontext.feedback_notification.Where(x => x.notification_to == receiverId).ToList();
        }

       

    }
}
