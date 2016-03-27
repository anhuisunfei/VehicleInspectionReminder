using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Net.Mail;
using System.Web;
using Quartz;
using Quartz.Impl;
using VehicleInspectionReminder.Model;
using VehicleInspectionReminder.Service;
using Autofac;
using Autofac.Extras.Quartz;
using Autofac.Integration.Mvc;

namespace VehicleInspectionReminder.Web.Jobs
{
    public class JobScheduler
    {
        public static void Start()
        {
            var config = (NameValueCollection)ConfigurationManager.GetSection("quartz");
            ISchedulerFactory schedFact = new StdSchedulerFactory(config);
            var sched = schedFact.GetScheduler();
            var scoppe = AutofacDependencyResolver.Current.RequestLifetimeScope.Resolve<AutofacJobFactory>();
            sched.JobFactory = scoppe;
            sched.Start();
            ITrigger trigger = TriggerBuilder.Create()
           .WithIdentity("1JobTrigger")
             .StartNow()
           .WithSimpleSchedule(x => x
               .WithIntervalInSeconds(60 * 60)
               .RepeatForever()
           )
           .Build();
            IJobDetail job = JobBuilder.Create<EmailingJob>()
                                       .WithIdentity("job1")
                                       .Build();
            sched.ScheduleJob(job, trigger);
        }
    }



    public class EmailingJob : IJob
    {
        private readonly INotificationMessageService _messageService;
        private readonly IOwnerInfoService _ownereInfoService;
        private readonly IVehicleInfoService _vehicleInfoService;
        public EmailingJob(INotificationMessageService messageService, IOwnerInfoService ownerInfoService, IVehicleInfoService vehicleInfoService)
        {
            _messageService = messageService;
            _ownereInfoService = ownerInfoService;
            _vehicleInfoService = vehicleInfoService;
        }
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                var allCarList = _vehicleInfoService.GetAll();
                foreach (var item in allCarList)
                {
                    DateTime insectionTime = _vehicleInfoService.GetNextInspectionTime(item.VehicleTypeId, item.Plate, item.LastInspectionTime.HasValue ? 2 : 1, item.PurchaseDate);

                    int day = DateTime.Now.Subtract(insectionTime).Days;
                    if (day >= 0 && day <= 7)
                    {
                        _messageService.Add(new NotificationMessage
                        {
                            Body = string.Format("您的下一次车检时间为{0}，请按时车检!", insectionTime.ToString("yyyy-MM-dd")),
                            CheckTime = insectionTime,
                            IsSending = false,
                            Subject = "车检提醒",
                            VehicleInfoId = item.OwnerId,
                            Email = item.Owner.Email,
                            Status = RecordStatus.Acvitiy

                        });
                    }
                }

                var list = _messageService.LoadAllNoSeding();

                ICollection<EmailMessage> mails = new List<EmailMessage>();
                foreach (var item in list)
                {
                    mails.Add(new EmailMessage
                    {
                        MessageBody = item.Body,
                        ToUser = item.Email
                    });
                }
                SendEmail(mails);
            }
            catch (Exception ex)
            {
                Elmah.ErrorLog.GetDefault(HttpContext.Current).Log(new Elmah.Error(ex));
            }


        }

        public void SendEmail(ICollection<EmailMessage> mails)
        {
            try
            {
                if (mails == null || mails.Count == 0)
                {
                    return;
                }
                string fromUser = System.Configuration.ConfigurationManager.AppSettings["fromuser"];
                if (string.IsNullOrWhiteSpace(fromUser))
                    throw new ArgumentNullException("163  fromUser");
                string fromPwd = System.Configuration.ConfigurationManager.AppSettings["frompwd"];
                if (string.IsNullOrWhiteSpace(fromPwd))
                    throw new ArgumentNullException("163 fromPwd");
                SmtpClient client = new SmtpClient();
                client.Host = "smtp.163.com";//使用163的SMTP服务器发送邮件
                client.UseDefaultCredentials = true;
                client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                client.Credentials = new System.Net.NetworkCredential(fromUser.Split('@')[0], fromPwd);

                foreach (var mailItem in mails)
                {
                    MailMessage message = new MailMessage();
                    message.From = new MailAddress(fromUser);
                    message.To.Add(mailItem.ToUser);
                    message.Subject = "车检提醒";
                    message.Body = mailItem.MessageBody;
                    message.SubjectEncoding = System.Text.Encoding.UTF8;
                    message.BodyEncoding = System.Text.Encoding.UTF8;
                    message.Priority = System.Net.Mail.MailPriority.High;
                    message.IsBodyHtml = true;
                    client.Send(message);
                }

            }
            catch (Exception ex)
            {
                Elmah.ErrorLog.GetDefault(HttpContext.Current).Log(new Elmah.Error(ex));
                // throw ex;
            }
        }
    }

    public class EmailMessage
    {
        public string ToUser { get; set; }

        public string MessageBody { get; set; }
    }



}