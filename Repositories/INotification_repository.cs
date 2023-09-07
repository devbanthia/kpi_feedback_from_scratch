using kpi_feedback_from_scratch.Models.Domain.Feedback_Notification;
using Microsoft.EntityFrameworkCore;

namespace kpi_feedback_from_scratch.Repositories
{
    public interface INotification_repository
    {
        public void add(Feedback_Notification notification);


        public void delete(Feedback_Notification notification);


        public List<Feedback_Notification> get_by_receiver(int receiverId);
  
    }
}
