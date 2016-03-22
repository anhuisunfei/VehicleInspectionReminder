using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Net.Mail;
using System.Web;
using Quartz;
using Quartz.Impl;

namespace VehicleInspectionReminder.Web.Jobs
{
	public class JobScheduler
	{
		public static void Start()
		{
			DateTimeOffset startTime = DateBuilder.FutureDate(1, IntervalUnit.Hour);
			IJobDetail job = JobBuilder.Create<EmailingJob>()
									   .WithIdentity("job1")
									   .Build();

			ISchedulerFactory sf = new StdSchedulerFactory();
			IScheduler sc = sf.GetScheduler();
			ITrigger trigger = TriggerBuilder.Create()
				.WithIdentity("triggerName", "groupName")
			.WithSimpleSchedule(t =>
			  t.WithIntervalInSeconds(60)
		   .RepeatForever())
		   .Build();
			sc.ScheduleJob(job, trigger);
			sc.Start();
		}
	}

	public class EmailingJob : IJob
	{
		public void Execute(IJobExecutionContext context)
		{
			ICollection<EmailMessage> mails = new List<EmailMessage>();
			//mails.Add(new EmailMessage()
			//{
			//	MessageBody = "Elmah Test",
			//	ToUser = "anhuisunfei@gmail.com"
			//});
			SendEmail(mails);
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
				string fromPwd = System.Configuration.ConfigurationManager.AppSettings["frompwd"];

				SmtpClient client = new SmtpClient();
				client.Host = "smtp.163.com";//使用163的SMTP服务器发送邮件
				client.UseDefaultCredentials = true;
				client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
				client.Credentials = new System.Net.NetworkCredential(fromUser.Split('@')[0], fromPwd);
				MailMessage Message = new MailMessage();
				Message.From = new MailAddress(fromUser);

				foreach (var mailItem in mails)
				{
					Message.To.Add(mailItem.ToUser);
					Message.Subject = "车检提醒";
					Message.Body = mailItem.MessageBody;
					Message.SubjectEncoding = System.Text.Encoding.UTF8;
					Message.BodyEncoding = System.Text.Encoding.UTF8;
					Message.Priority = System.Net.Mail.MailPriority.High;
					Message.IsBodyHtml = true;
					client.Send(Message);
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