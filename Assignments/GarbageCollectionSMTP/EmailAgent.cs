using System.Net.Mail;
using System.Net;
using System.Security;

namespace GarbageCollectionSMTP
{
    internal class EmailAgent
    {
        private readonly string _hostEmailAddress;
        private readonly string _smtpServerAddress;
        private readonly string _smtpServerPassword;

        public EmailAgent(string hostEmail, string serverAddress, string serverPassword)
        {
            _hostEmailAddress = hostEmail;
            _smtpServerAddress = serverAddress;
            _smtpServerPassword = serverPassword;
        }

        public void SendEmail(string recipientEmail)
        {
            if (recipientEmail is null)
            {
                throw new ArgumentNullException("Recipient email cannot be null");
            }

            string subject = "Thank you for subscribing!";
            string body = "Thank you for subscribing to our newsletter.";

            using (MailMessage message = new MailMessage(_hostEmailAddress, recipientEmail, subject, body))
            {
                using SmtpClient smtpClient = new SmtpClient(_smtpServerAddress)
                {
                    Port = 587,
                    Credentials = new NetworkCredential(_hostEmailAddress, _smtpServerPassword),
                    EnableSsl = true,
                };
                try
                {
                    smtpClient.Send(message);
                    Console.WriteLine("Email sent successfully.");
                }
                catch (SmtpException ex)
                {
                    Console.WriteLine($"Failed to send email. Error: {ex.Message}");
                }
                catch (SecurityException ex)
                {
                    Console.WriteLine($"Email operation requires security permissions. Error: {ex.Message}");
                }
            }
        }
    }
}
