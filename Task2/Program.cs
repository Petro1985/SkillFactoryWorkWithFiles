// See https://aka.ms/new-console-template for more information
using Task1;

namespace WorkWithFiles
{
    static class Task2
    {
        static int Main(string[] arg)
        {
            const int timer = 1; //this is the time that files are untouchable (minutes)
            string directoryPath;
            if (arg.Length > 0)
            {
                directoryPath = arg[0]; //we use only first parameter and it is supposed to be a path to a directory
            }
            else
            {
                Console.WriteLine("Please, set up the parameter (Task1.exe PathToDirectory)");
                Console.ReadLine();
                return 0;
            }

            var directory = new DirectoryInfo(directoryPath);
            if (!directory.Exists) //check folder if it exists
            {
                Console.Write("Incorrect path"); //if it doesn't, write message and quit program
                return 0;
            }
            
            var size = directory.CalculateSize();
            Console.WriteLine($"The size of all the files is {size} bytes");
            return 0;
        }
    }
 }