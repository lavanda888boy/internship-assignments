namespace WorkingWithFilesAsync
{
    public class Patient
    {
        private readonly int _id;
        public int ID { get { return _id; } }
        
        private string _name;
        public string Name { get { return _name; } }

        private string _surname;
        public string Surname {  get { return _surname; } }

        private string _gender;
        public string Gender { get { return _gender; } }

        public List<string> AssignedDoctors { get; set; }

        public List<string>? Illnesses { get; set; }

        public Patient(string name, string surname, string gender, List<string> assignedDoctors, List<string> illnesses)
        {
            _id = IdGenerator.CurrentID;
            _name = name;
            _surname = surname;
            _gender = gender;
            AssignedDoctors = assignedDoctors;
            Illnesses = illnesses;
        }

        public Patient(int id, string name, string surname, string gender, List<string> assignedDoctors, List<string> illnesses)
            : this(name, surname, gender, assignedDoctors, illnesses)
        {
            _id = id;
        }

        public override string ToString()
        {
            var doctors = AssignedDoctors.Aggregate((curr, next) => curr + ", " + next);
            var illnesses = Illnesses.Aggregate((curr, next) => curr + ", " + next);
            return $"ID: {_id}\nName: {_name} Surname: {_surname}\nGender: {_gender}\nDoctors: {doctors}\nIllnesses: {illnesses}";
        }

        public override bool Equals(object otherPatient)
        {
            if (otherPatient == null || GetType() != otherPatient.GetType())
            {
                return false;
            }

            var other = (Patient) otherPatient;
            return ID == other.ID &&
                   Name == other.Name &&
                   Surname == other.Surname &&
                   Gender == other.Gender &&
                   AssignedDoctors.SequenceEqual(other.AssignedDoctors) &&
                   ((Illnesses == null && other.Illnesses == null) ||
                   (Illnesses != null && other.Illnesses != null && Illnesses.SequenceEqual(other.Illnesses)));
        }
    }
}
