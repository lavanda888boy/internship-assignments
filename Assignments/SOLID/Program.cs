using SOLID.entity;
using SOLID.exception;
using SOLID.infrastructure;
using SOLID.repository;
using SOLID.service;

namespace SOLID
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IRepository<User> userRepository = new UserRepository();
            List<User> users = new()
            {
                new User("Mike", "mike@mail.com", "078125369", "2525"),
                new User("Steve", "steve@mail.com", "064859785", "8998"),
                new User("Harvey", "harvey@mail.com", "061220002", "4102")
            };

            foreach (var user in users)
            {
                userRepository.Add(user);
            }

            List<INotificationService> notificationServices = new()
            {
                new EmailNotificationService(),
                new SMSNotificationService(),
                new PushNotificationService()
            };

            INotificationManager notificationManager = new NotificationManager(userRepository, notificationServices);
            
            int senderId = 2;
            int recipientId = 5;
            string message = "Hello, my friend"; 
            NotificationType notificationType = NotificationType.SMS;

            try
            {
                notificationManager.Notify(senderId, recipientId, message, notificationType);
            }
            catch (ServiceNotAvailableException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
