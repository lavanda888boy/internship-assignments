namespace SOLID
{
    internal class NotificationMessage
    {
        private User _sender;
        public User Sender { get { return _sender; } set { _sender = value; } }

        private User _recipient;
        public User Recipient { get { return _recipient; } set { _recipient = value; } }

        private string _content;
        public string Content { get { return _content; } set { _content = value; } }

        public NotificationMessage(User sender, User recipient, string content)
        {
            _sender = sender;
            _recipient = recipient;
            _content = content;
        }
    }
}
