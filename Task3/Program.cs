namespace Task1
{
    static class Task3
    {
        static int Main(string[] arg)
        {
            const int timer = 1;        //this is the time that files are untouchable (minutes)
            
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

            var resultBefore = directory.CalculateSize();
            var sizeBefore = resultBefore.Size;
            var filesBefore = resultBefore.FilesCount;
            Console.WriteLine($"Size before cleaning: {sizeBefore} bytes");
            directory.CleanTheDirectory(timer);
            var resultAfter = directory.CalculateSize();
            var sizeAfter = resultAfter.Size;
            var filesAfter = resultAfter.FilesCount;
            Console.WriteLine($"Size after cleaning: {sizeAfter} bytes");
            Console.WriteLine($"Released: {sizeBefore - sizeAfter} bytes");
            Console.WriteLine($"Deleted {filesBefore - filesAfter} files");
            return 0;
        }

    }
}