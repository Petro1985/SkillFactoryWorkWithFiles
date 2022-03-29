namespace Task1;

public static class DirectoryInfoExt
{
    public static (List<FileInfo> Files, List<DirectoryInfo> Directories) GetAllTheFiles(this DirectoryInfo directory)
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

    /// <summary>
    /// Method for Task1
    /// </summary>
    /// <param name="directory"></param>
    /// <param name="timer"></param>
    public static void CleanTheDirectory(this DirectoryInfo directory, int timer)
    {
        //take all files and directories in current dictionary (and in subdirectories as well)
        var allFilesAndDirectories = directory.GetAllTheFiles();

        //getting only files that haven't changed for more than 'timer' minutes 
        var allFiles = allFilesAndDirectories.Files.Where(file =>
            DateTime.Compare(DateTime.Now, file.LastAccessTime.AddMinutes(timer)) > 0).ToList();
        //getting only directories that haven't changed for more than 'timer' minutes 
        var allDirectories = allFilesAndDirectories.Directories.Where(dir =>
            DateTime.Compare(DateTime.Now, dir.LastAccessTime.AddMinutes(timer)) > 0).Reverse().ToList();

        foreach (var file in allFiles) //just delete all the rest files
        {
            try
            {
                file.Delete();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message); //if there is some exception, write the message in console
            }
        }

        foreach (var dir in allDirectories) //just delete all the rest directories
        {
            try
            {
                dir.Delete();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message); //if there is some exception, write the message in console
            }
        }
    }

    /// <summary>
    /// Method for Task2
    /// </summary>
    /// <param name="directory"></param>
    public static (long Size, int FilesCount) CalculateSize(this DirectoryInfo directory) =>
        directory.GetAllTheFiles().Files.Aggregate((0L, 0), (i, file) => (i.Item1 + file.Length, i.Item2 + 1));
}