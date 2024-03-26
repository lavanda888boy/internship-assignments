namespace WorkingWithFilesAsync
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PatientRepository pr  = new PatientRepository();
            Console.WriteLine("Welcome to the hospital management!\n");

            while (true)
            {
                Console.WriteLine("Choose an operation to perform:\n");
                Console.WriteLine("1 - display all patients' info");
                Console.WriteLine("2 - get patient by id");
                Console.WriteLine("3 - add new patient");
                Console.WriteLine("4 - update existing patient");
                Console.WriteLine("5 - delete existing patient\n");

                var choice = Console.ReadLine();
                if (choice is not null)
                {
                    int c = int.Parse(choice);

                    switch (c)
                    {
                        case 1:
                            GetAllPatients(pr);
                            break;
                        case 2:
                            GetPatientByID(pr);
                            break;
                        case 3:
                            AddPatient(pr);
                            break;
                        case 4:
                            UpdatePatient(pr);
                            break;
                        case 5:
                            DeletePatientByID(pr);
                            break;
                        default:
                            break;
                    }
                }

                Console.WriteLine();
            }
        }

        static void GetAllPatients(PatientRepository pr)
        {
            Console.WriteLine();

            var patients = pr.GetAll();
            if (patients.Count() == 0)
            {
                Console.WriteLine("There are currently no patients registered");
            } 
            else
            {
                foreach (var p in patients)
                {
                    Console.WriteLine(p.ToString());
                }
            }
        }

        static void GetPatientByID(PatientRepository pr)
        {
            Console.WriteLine("\nProcess of getting the patient started..");
            Console.Write("\nEnter a patient's id: ");
            
            var line = Console.ReadLine();
            if (line is null  ||  line == "")
            {
                Console.WriteLine("\nYou have to provide an ID for searching patient");
                return;
            } 

            int id = int.Parse(line);

            try
            {
                var patient = pr.GetById(id);
                Console.WriteLine(patient.ToString());
            }
            catch (PatientDoesNotExistException ex)
            {
                Console.WriteLine($"\n{ex.Message}\n");
            }
            finally
            {
                Console.WriteLine("\n..process of getting the patient finished");
            }
        }

        static void AddPatient(PatientRepository pr)
        {
            Console.WriteLine("\nProcess of adding the patient started..\n");

            Patient p;
            while (true)
            {
                Console.Write("Name: ");
                var name = Console.ReadLine();

                Console.Write("Surname: ");
                var surname = Console.ReadLine();

                Console.Write("Gender: ");
                var gender = Console.ReadLine();

                Console.Write("Assigned doctors: ");
                var l1 = Console.ReadLine();
                List<string> doctors = l1.Split(" ").ToList();

                Console.Write("Illnesses: ");
                var l2 = Console.ReadLine();
                List<string> illnesses = l2.Split(" ").ToList();
                
                if (name != "" && surname != "" && gender != "" && l1 != "")
                {
                    p = new Patient(1, name, surname, gender, doctors, illnesses);
                    break;
                }
                Console.WriteLine("\nFields cannot be null\n");
            }

            pr.Add(p);
            Console.WriteLine("\nPacient succesfully added");
            Console.WriteLine("\n..process of adding the patient finished");
        }

        static void UpdatePatient(PatientRepository pr)
        {
            Console.WriteLine("\nProcess of updating the patient started..");
            Console.Write("\nEnter a patient's id: ");

            var line = Console.ReadLine();
            if (line is null || line == "")
            {
                Console.WriteLine("\nYou have to provide an ID for updating patient");
                return;
            }

            int id = int.Parse(line);

            Patient p;
            while (true)
            {
                Console.Write("Name: ");
                var name = Console.ReadLine();

                Console.Write("Surname: ");
                var surname = Console.ReadLine();

                Console.Write("Gender: ");
                var gender = Console.ReadLine();

                Console.Write("Assigned doctors: ");
                var l1 = Console.ReadLine();
                List<string> doctors = l1.Split(" ").ToList();

                Console.Write("Illnesses: ");
                var l2 = Console.ReadLine();
                List<string> illnesses = l2.Split(" ").ToList();

                if (name != "" && surname != "" && gender != "" && l1 != "")
                {
                    p = new Patient(1, name, surname, gender, doctors, illnesses);
                    break;
                }
                Console.WriteLine("\nFields cannot be null\n");
            }

            try
            {
                pr.Update(id, p);
                Console.WriteLine("\nPacient succesfully updated");
            }
            catch (PatientDoesNotExistException ex)
            {
                Console.WriteLine($"\n{ex.Message}\n");
            } 
            finally
            {
                Console.WriteLine("\n..process of updating the patient finished");
            }
        }

        static void DeletePatientByID(PatientRepository pr)
        {
            Console.WriteLine("\nProcess of deleting the patient started..");
            Console.Write("\nEnter a patient's id: ");

            var line = Console.ReadLine();
            if (line is null || line == "")
            {
                Console.WriteLine("\nYou have to provide an ID for deleting patient");
                return;
            }

            int id = int.Parse(line);

            try
            {
                pr.DeleteById(id);
            }
            catch (PatientDoesNotExistException ex)
            {
                Console.WriteLine($"\n{ex.Message}\n");
            }
            finally
            {
                Console.WriteLine("..process of deleting the patient finished");
            }
        }
    }
}
