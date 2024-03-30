namespace SOLID.entity
{
    internal class AdvancedNotification : Notification
    {
        public string CallToAction { get; set; }

        public AdvancedNotification(string content, string callToAction) : base(content)
        {
            CallToAction = callToAction;
        }

        public override string ToString()
        {
            return $"Date: {CreationDate};\nMessage: {Content};\nCall-to-action: {CallToAction}\n";
        }
    }
}
