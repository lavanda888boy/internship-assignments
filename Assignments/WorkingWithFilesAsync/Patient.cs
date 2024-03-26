namespace WorkingWithFilesAsync
{
    internal class Patient
    {
        private readonly int _id;
        
        private string _name;
        public string Name { get { return _name; } }

        private string _surname;
        public string Surname {  get { return _surname; } }

        private string _gender;
        public string Gender { get { return _gender; } }

        public List<string> AssignedDoctors { get; set; }

        public List<string>? Illnesses { get; set; }

        public Patient(int id, string name, string surname, string gender, List<string> assignedDoctors, List<string> illnesses)
        {
            _id = id;
            _name = name;
            _surname = surname;
            _gender = gender;
            AssignedDoctors = assignedDoctors;
            Illnesses = illnesses;
        }
    }
}
