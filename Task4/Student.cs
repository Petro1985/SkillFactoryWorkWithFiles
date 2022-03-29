namespace Task4
{
    [Serializable]
    public class Student
    {
        public Student(string name, string group, DateTime dateOfBirth)
        {
            Name = name;
            Group = group;
            DateOfBirth = dateOfBirth;
        }

        public string Name { get; set; }
        public string Group { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}