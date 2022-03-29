using System.Text;

namespace Task4
{
    public static class Program
    {
        public static int Main()
        {
            var filename = "Students.dat";
            const int studentsCount = 5000;  //only for creation student's records (it isn't used in task solving)

            //trying to find out the way to desktop
            var pathToDesktop = Directory.GetDirectoryRoot(Environment.SystemDirectory);    
            pathToDesktop = pathToDesktop + "Users\\" + Environment.UserName + "\\Desktop\\";

            //CreatStudentsFile("students.dat", studentsCount);     //use only if you need to create student's file
            
            var stream = new FileStream("students.dat", FileMode.Open);
            var reader = new BinaryReader(stream, Encoding.Default, false);
            var students = new List<Student>();
            var numberOfStudents = reader.ReadInt32();          //The first in32 contains number of records 

            for (var i = 0; i < numberOfStudents; i++)              //read all the students to List<Student>
            {
                var student = new Student("","", new DateTime());
                student.Name = reader.ReadString();
                student.Group = reader.ReadString();
                student.DateOfBirth = DateTime.FromFileTime(reader.ReadInt64());
                students.Add(student);
            }
            reader.Dispose();
            stream.Dispose();

            var directory = new DirectoryInfo($"{pathToDesktop}Students");
            try
            {
                if (!directory.Exists) directory.Create();
            }
            catch (Exception e)
            {
                Console.WriteLine("By some reason, program couldn't find way to your desktop, sorry");
                return 1;
            }

            var groups = students.Aggregate(new Dictionary<string, List<Student>>(), (dictionary, student) =>
            {
                var group = student.Group;
                if (dictionary.ContainsKey(group))
                {
                    dictionary[group].Add(student);
                }
                else
                {
                    var newStudentList = new List<Student>();
                    newStudentList.Add(student);
                    dictionary.Add(student.Group, newStudentList);
                }
                return dictionary;
            });

            foreach (var group in groups)
            {
                var streamWriter = File.CreateText($"{pathToDesktop}Students\\{group.Key}.txt");
                foreach (var student in group.Value)
                {
                    streamWriter.WriteLine($"{student.Name}, {student.DateOfBirth.ToShortDateString()}");
                }
                streamWriter.Dispose();
            }
            return 0;
        }

        public static void CreatStudentsFile(string filename, int studentsCount)
        {
            string[] names = 
            {
                "Petr",
                "Andrew",
                "Tania",
                "Sveta",
                "Anastasiia",
                "Roman",
                "Nikita",
                "Vasia",
                "Anton",
                "Guk",
                "Sasha",
                "Someone",
                "Alligator",
                "Georgia",
                "Filip",
            };
            var r = new Random();
            var stream = new FileStream(filename, FileMode.Create);
            var writer = new BinaryWriter(stream, Encoding.Default, false);

            writer.Write(studentsCount);
            for (int i = 0; i < studentsCount; i++)
            {
                var student = new Student(
                    names[r.Next(0, 14)],
                    $"CDEV-{r.Next(1, 18)}",
                    DateTime.Now.AddYears(-20 + r.Next(0, 4)).AddDays(r.Next(-183, 183)).Date
                );
                
                writer.Write(student.Name);
                writer.Write(student.Group);
                writer.Write(student.DateOfBirth.ToFileTime());
            }
            
            writer.Dispose();
            stream.Dispose();
        }
    }
}

