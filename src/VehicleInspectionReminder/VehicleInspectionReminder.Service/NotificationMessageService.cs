using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInspectionReminder.Data.Infrastructure;
using VehicleInspectionReminder.Data.Repository;
using VehicleInspectionReminder.Model;

namespace VehicleInspectionReminder.Service
{
	public class NotificationMessageService : INotificationMessageService
	{
		private readonly INotificationMessageRepository _messageRepository;
		private readonly IUnitOfWork _unitOfWork;

		public NotificationMessageService(IUnitOfWork unitOfWork, INotificationMessageRepository messageRepository)
		{
			_unitOfWork = unitOfWork;
			_messageRepository = messageRepository;
		}

		public void Add(NotificationMessage model)
		{
			_messageRepository.Insert(model);
			_unitOfWork.Commit();
		}

		public void AddList(IEnumerable<NotificationMessage> list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			foreach (var model in list)
			{
				_messageRepository.Insert(model);
			}
			_unitOfWork.Commit();
		}

		public void UpdateSendingStatus(int id)
		{
			var model = _messageRepository.GetById(id);
			model.IsSending = true;
			model.SendingTime = DateTime.Now;
			_messageRepository.Update(model);
			_unitOfWork.Commit();
		}

		public IEnumerable<NotificationMessage> LoadAllNoSeding()
		{
			return _messageRepository.GetManay(p => p.IsSending == false).ToList();
		}
	}

	public interface INotificationMessageService
	{
		void Add(NotificationMessage model);

		void AddList(IEnumerable<NotificationMessage> list);

		void UpdateSendingStatus(int id);

		IEnumerable<NotificationMessage> LoadAllNoSeding();
	}
}
