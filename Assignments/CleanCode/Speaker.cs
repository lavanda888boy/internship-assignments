namespace CleanCode
{
    internal class Speaker
    {
        private readonly List<string> _topics = new() { "Cobol", "Punch Cards", "Commodore", "VBScript" };
        private readonly List<string> _employers = new() { "Microsoft", "Google", "Fog Creek Software", "37Signals" };
        private readonly List<string> _domains = new() { "aol.com", "hotmail.com", "prodigy.com", "CompuServe.com" };

        private readonly int _experienceLimit = 10;
        private readonly int _certificationsLimit = 3;
        private readonly int _browserMajorVersion = 9;

        private readonly Dictionary<int, int> _feeRanges = new()
        {
            { 1, 500 },
            { 2, 250 },
            { 4, 100 },
            { 6, 50 },
            { 10, 0 }
        };

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int? Exp { get; set; }
        public bool HasBlog { get; set; }
        public string BlogURL { get; set; }
        public WebBrowser Browser { get; set; }
        public List<string> Certifications { get; set; }
        public string Employer { get; set; }
        public int RegistrationFee { get; set; }
        public List<Session> Sessions { get; set; }

        public void Register(IRepository repository)
        {
            ValidatePersonalInfo();

            if (!CheckCertifications())
            {
                throw new SpeakerDoesntMeetRequirementsException("Speaker doesn't meet our abitrary and capricious standards.");
            }

            if (!TryToApproveSessions())
            {
                throw new NoSessionsApprovedException("No sessions approved.");
            }

            RegistrationFee = CalculateRegistrationFee();
            repository.SaveSpeaker(this);
        }

        private void ValidatePersonalInfo()
        {
            if (string.IsNullOrWhiteSpace(FirstName)) throw new ArgumentNullException("First Name is required");
            if (string.IsNullOrWhiteSpace(LastName)) throw new ArgumentNullException("Last Name is required");
            if (string.IsNullOrWhiteSpace(Email)) throw new ArgumentNullException("Email is required");
            if (Sessions.Count == 0) throw new ArgumentException("Can't register speaker with no sessions to present.");
        }

        private bool CheckCertifications()
        {
            return (HasExperience() ||
                   HasBlog ||
                   HasEnoughCertifications() ||
                   IsEmployed()) ||
                   (HasEmailInDomains() && 
                   !(HasInternetExplorerBrowser() && 
                   HasRightBrowserVersion()));
        }

        private bool HasExperience()
        {
            return Exp > _experienceLimit;
        }

        private bool HasEnoughCertifications()
        {
            return Certifications.Count > _certificationsLimit;
        }

        private bool IsEmployed()
        {
            return _employers.Contains(Employer);
        }

        private bool HasEmailInDomains()
        {
            return _domains.Contains(Email.Split('@').Last());
        }

        private bool HasInternetExplorerBrowser()
        {
            return Browser.Name == WebBrowser.BrowserName.InternetExplorer;
        }

        private bool HasRightBrowserVersion()
        {
            return Browser.MajorVersion < _browserMajorVersion;
        }

        private bool TryToApproveSessions()
        {
            return Sessions.Any(session => _topics.Any(topic => CheckSessionTopicCorrespondence(session, topic)));
        }

        private bool CheckSessionTopicCorrespondence(Session session, string topic)
        {
            return session.Title.Contains(topic) || session.Description.Contains(topic);
        }

        private int CalculateRegistrationFee()
        {
            return _feeRanges.First(f => Exp <= f.Key).Value;
        }
    }
}
