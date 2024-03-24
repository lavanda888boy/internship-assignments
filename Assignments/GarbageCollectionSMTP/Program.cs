using DotNetEnv;

namespace GarbageCollectionSMTP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Env.Load();

            Console.Write("Enter your email address: ");
            string? emailAddress = Console.ReadLine();

            EmailAgent ea = new EmailAgent(Env.GetString("HOST_EMAIL"), Env.GetString("SMTP_SERVER"), Env.GetString("APP_PASSWORD"));
            try
            {
                ea.SendEmail(emailAddress);
            }
            catch (ArgumentNullException ex) 
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}