

namespace Task1
{
    static class Task1
    {
        static int Main(string[] arg)
        {
            const int timer = 30;        //this is the time that files are untouchable (minutes)
            
            string directoryPath;
            if (arg.Length > 0)
            {
                directoryPath = arg[0];         //we use only first parameter and it is supposed to be a path to a directory
            }
            else
            {
                Console.WriteLine("Please, set up the parameter (Task1.exe PathToDirectory)");
                Console.ReadLine();
                return 0;
            }
            
            var directory = new DirectoryInfo(directoryPath);

            if (!directory.Exists)      //check folder if it exists
            {
                Console.Write("Incorrect path");    //if it doesn't, write message and quit program
                return 0;
            }
            
            while (true)        //we do it all day long and even more
            {
                directory.CleanTheDirectory(timer);     //clean the directory from files that haven't used for {timer} minutes
                Thread.Sleep(5000);     //wait 5 second before repeating
            }
        }

    }
}

