using kpi_feedback_from_scratch.data;
using kpi_feedback_from_scratch.Models.Domain.Feedback_Notification;
using kpi_feedback_from_scratch.Models.Domain.KPI_Assessor;
using kpi_feedback_from_scratch.Models.Domain.User;
using kpi_feedback_from_scratch.Repositories;

namespace kpi_feedback_from_scratch
{
    public class feedback_reminder_notification : BackgroundService
    {
        private IKPI_Assessor_Feedback_repository kpi_assessor_repository;
        private kpi_feedback_dbcontext dbcontext;
        private IUser_repository user_repository;
        private INotification_repository notification_repository;
        public feedback_reminder_notification(IKPI_Assessor_Feedback_repository _kpi_assessor_repository, IUser_repository _user_repository, kpi_feedback_dbcontext _dbcontext, INotification_repository _notification_repository)
        {
            kpi_assessor_repository = _kpi_assessor_repository;
            user_repository = _user_repository;
            notification_repository = _notification_repository;
            dbcontext = _dbcontext;
        }

        public bool overdue_by(int days, DateTime due_date)
        {
            TimeSpan tolerance = TimeSpan.FromHours(12);
            if ((DateTime.Now > due_date - tolerance)  && (DateTime.Now < due_date + tolerance))
            {
                return true;
            }
            return false;
        }

        public void reminder_to_self(KPI_Assessor kpi_assessor)
        {
            string reminder = "kpi feedback pending! please take action";
            Feedback_Notification notification = new Feedback_Notification()
            {
                notification_from = kpi_assessor.AssessorId,
                notification_to = kpi_assessor.AssessorId,
                message = reminder

            };
            notification_repository.add(notification);
        }

        public void escalate_to_HOD(KPI_Assessor kpi_assessor)
        {
            User assessor_employee = dbcontext.user.FirstOrDefault(x => x.Id == kpi_assessor.AssessorId);

            int assessor_division_id = dbcontext.division.FirstOrDefault(x => x.Id == assessor_employee.divisionId).Id;

            User department_HOD = dbcontext.user.FirstOrDefault(x => (x.designationId == 3) && (x.divisionId == assessor_division_id));

            string reminder = "kpi feedback pending for " + assessor_employee.Name + "! Please take urgent action";

            Feedback_Notification notification = new Feedback_Notification()
            {
                notification_from = kpi_assessor.AssessorId,
                notification_to = department_HOD.Id,
                message = reminder

            };
        }

        public void escalate_to_TM(KPI_Assessor kpi_assessor)
        {
            User assessor_employee = dbcontext.user.FirstOrDefault(x => x.Id == kpi_assessor.AssessorId);
            string assessor_division_name = dbcontext.division.FirstOrDefault(x => x.Id == assessor_employee.divisionId).Name;

            string reminder = "kpi feedback pending for " + assessor_employee.Name + "from " + assessor_division_name + "! Please take urgent action";
            List<User> top_management = dbcontext.user.Where(x => x.designationId == 4).ToList();


            foreach (User TM in top_management)
            {
                Feedback_Notification notification = new Feedback_Notification()
                {
                    notification_from = kpi_assessor.AssessorId,
                    message = reminder

                };

                notification.notification_to = TM.Id;
                notification_repository.add(notification);
            }
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(stoppingToken.IsCancellationRequested)
            {
                List<KPI_Assessor> kpi_assessors = dbcontext.kpi_assessor.ToList();
                foreach(KPI_Assessor kpi_assessor in kpi_assessors)
                {
                    DateTime due_date = kpi_assessor.KPI_Assessment_Due_Date;
                    User assessor = user_repository.get_user(kpi_assessor.AssessorId);

               
                    if(overdue_by(0, due_date) || overdue_by(3, due_date) || overdue_by(7, due_date))
                    {
                        reminder_to_self(kpi_assessor);
                    }

                    else if(overdue_by(10, due_date))
                    {
                        reminder_to_self(kpi_assessor);
                        if(assessor.designationId > 2)
                        {
                            escalate_to_TM(kpi_assessor);
                        }
                        else
                        {
                            escalate_to_HOD(kpi_assessor);
                        }
                        
                    }

                    else if(overdue_by(15, due_date))
                    {
                        reminder_to_self(kpi_assessor);
                        escalate_to_HOD(kpi_assessor);
                        escalate_to_TM(kpi_assessor);

                    }


                }
                await Task.Delay(TimeSpan.FromDays(1), stoppingToken);

            }
            
        }
    }
}
