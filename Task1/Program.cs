

namespace WorkWithFiles
{
    static class Task1
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
            
            while (true)        //we do it all day long and even more
            {
                //take all files and directories in current dictionary (and in subdirectories as well)
                var allFilesAndDirectories = GetAllTheFiles(directory);       
                
                //getting only files that haven't changed for more than 'timer' minutes 
                var allFiles = allFilesAndDirectories.Files.Where(file =>
                    DateTime.Compare(DateTime.Now, file.LastAccessTime.AddMinutes(timer)) > 0).ToList();
                //getting only directories that haven't changed for more than 'timer' minutes 
                var allDirectories = allFilesAndDirectories.Directories.Where(dir =>
                    DateTime.Compare(DateTime.Now, dir.LastAccessTime.AddMinutes(timer)) > 0).ToList();
                
                foreach (var file in allFiles)      //just delete all the rest files
                {
                    try
                    {
                        file.Delete();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);       //if there is some exception, write the message in console
                    }
                }

                foreach (var dir in allDirectories)      //just delete all the rest directories
                {
                    try
                    {
                        dir.Delete();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);       //if there is some exception, write the message in console
                    }
                }
                Thread.Sleep(5000);     //wait 5 second before repeating
            }
        }

        private static (List<FileInfo> Files, List<DirectoryInfo> Directories) GetAllTheFiles(DirectoryInfo directory)
        {
            var files = directory.GetFiles().ToList();
            var directories = directory.GetDirectories();
            List<DirectoryInfo> dirResult = directories.ToList();

            foreach (var dir in directories)
            {
                var filesInDir = GetAllTheFiles(dir);
                foreach (var f in filesInDir.Files)
                {
                    files.Add(f);
                }
                foreach (var d in filesInDir.Directories)
                {
                    dirResult.Add(d);
                }
            }
            return (files, dirResult);
        }
    }
}