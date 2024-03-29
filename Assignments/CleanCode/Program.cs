namespace CleanCode
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Speaker s = new Speaker()
            {
                FirstName = "Elon",
                LastName = "Musk",
                Email = "elonx@mail.com",
                Employer = "Microsoft",
                Exp = 6,
                BlogURL = "https://utm.md/",
                Browser = new WebBrowser("IE", 8),
                Certifications = new List<string>(),
                Sessions = new List<Session>()
            };

            s.Sessions.Add(new Session("Cobol", "amazing session"));
            try
            {
                s.Register(new CompactRepository());
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (SpeakerDoesntMeetRequirementsException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (NoSessionsApprovedException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
