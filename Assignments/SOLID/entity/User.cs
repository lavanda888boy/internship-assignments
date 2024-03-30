using SOLID.utility;

namespace SOLID.entity
{
    internal class User
    {
        public readonly int Id;
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string AppToken { get; set; }

        public User(string name, string email, string phoneNumber, string appToken)
        {
            Id = IdGenerator.CurrentID;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            AppToken = appToken;
        }
    }
}
