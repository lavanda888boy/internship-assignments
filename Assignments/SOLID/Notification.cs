namespace SOLID
{
    internal abstract class Notification
    {
        private NotificationMessage _message;
        public NotificationMessage Message { get { return _message; } set { _message = value; } }

        public Notification(NotificationMessage message)
        {
            _message = message;
        }

        public abstract void Send();
    }
}
