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

        private readonly List<(int, int)> _feeRanges = new()
        {
            (1, 500),
            (2, 250),
            (4, 100),
            (6, 50),
            (10, 0)
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
            try
            {
                ValidatePersonalInfo();

                if (CheckCertifications())
                {
                    if (TryToApproveSessions())
                    {
                        CalculateRegistrationFee();
                        repository.SaveSpeaker(this);
                    }
                    else
                    {
                        throw new NoSessionsApprovedException("No sessions approved.");
                    }
                }
                else
                {
                    throw new SpeakerDoesntMeetRequirementsException("Speaker doesn't meet our abitrary and capricious standards.");
                }
            }
            catch (ArgumentNullException ex)
            {
                throw new InvalidSpeakerPersonalInfoException(ex.Message);
            }
            catch (ArgumentException ex)
            {
                throw new InvalidSpeakerPersonalInfoException(ex.Message);
            }
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
            if (Exp > _experienceLimit || HasBlog || Certifications.Count > _certificationsLimit || _employers.Contains(Employer))
            {
                return true;
            }
            else
            {
                string emailDomain = Email.Split('@').Last();

                if (_domains.Contains(emailDomain) && (!(Browser.Name == WebBrowser.BrowserName.InternetExplorer && Browser.MajorVersion < _browserMajorVersion)))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        private bool TryToApproveSessions()
        {
            foreach (var session in Sessions)
            {
                if (_topics.Any(topic => session.Title.Contains(topic) || session.Description.Contains(topic)))
                {
                    return true;
                }

            }
            return false;
        }

        private void CalculateRegistrationFee()
        {
            foreach (var (experienceThreshold, fee) in _feeRanges)
            {
                if (Exp <= experienceThreshold)
                {
                    RegistrationFee = fee;
                    break;
                }
            }
        }
    }
}
