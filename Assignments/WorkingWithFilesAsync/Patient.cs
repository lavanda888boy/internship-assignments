namespace WorkingWithFilesAsync
{
    internal class Patient
    {
        private readonly int _id;
        public int ID { get { return _id; } }
        
        private string _name;
        public string Name { get { return _name; } }

        private string _surname;
        public string Surname {  get { return _surname; } }

        private string _gender;
        public string Gender { get { return _gender; } }

        private List<string> _assignedDoctors;
        public List<string> AssignedDoctors { get; set; }

        private List<string> _illnesses;
        public List<string>? Illnesses { get; set; }

        public Patient(string name, string surname, string gender, List<string> assignedDoctors, List<string> illnesses)
        {
            _id = IdGenerator.CurrentID;
            _name = name;
            _surname = surname;
            _gender = gender;
            _assignedDoctors = assignedDoctors;
            _illnesses = illnesses;
        }

        public override string ToString()
        {
            var doctors = _assignedDoctors.Aggregate((curr, next) => curr + ", " + next);
            var illnesses = _illnesses.Aggregate((curr, next) => curr + ", " + next);
            return $"ID: {_id}\nName: {_name} Surname: {_surname}\nGender: {_gender}\nDoctors: {doctors}\nIllnesses: {illnesses}";
        }
    }
}
