using SOLID.entity;
using SOLID.exception;
using SOLID.repository;
using SOLID.service;

namespace SOLID.infrastructure
{
    internal class NotificationManager : INotificationManager
    {
        private readonly IRepository<User> _userRepository;
        private readonly List<INotificationService> _notificationServices;

        public NotificationManager(IRepository<User> userRepository, List<INotificationService> notificationServices)
        {
            _userRepository = userRepository;
            _notificationServices = notificationServices;
        }

        public void Notify(int senderId, int recipientId, string message, NotificationType notificationType)
        {
            User sender = _userRepository.GetById(senderId);
            User recipient;

            try
            {
                var service = _notificationServices.FirstOrDefault(s => s.GetType() == GetServiceType(notificationType));
                if (service == null)
                {
                    throw new ServiceNotAvailableException("The chosen notification service is not available");
                }
                
                recipient = _userRepository.GetById(recipientId);
                
                if (message.Contains("<call-to-action>"))
                {
                    AdvancedNotification advancedNotification = new AdvancedNotification(message, "<call-to-action>");
                    service.SendNotification(sender, recipient, advancedNotification);
                }
                else
                {
                    Notification notification = new Notification(message);
                    service.SendNotification(sender, recipient, notification);
                }
            }
            catch (UserDoesNotExistException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("User cannot be notified");
            }
        }

        private Type? GetServiceType(NotificationType notificationType)
        {
            return notificationType switch
            {
                NotificationType.Email => typeof(EmailNotificationService),
                NotificationType.SMS => typeof(SMSNotificationService),
                NotificationType.Push => typeof(PushNotificationService),
                _ => null,
            };
        }
    }
}
