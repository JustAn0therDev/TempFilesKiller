using System;
using System.IO;

namespace TempFilesKiller
{
    class DirectoryHandler : IDisposable
    {
        private const string TEMP_DIRECTORY_PATH = "C:/Users/highl/AppData/Local/Temp";

        public string[] ArrayOfDirectories { get; set; }
        public string[] ArrayOfFilesOutSideSubDirectories { get; set; }
        public DirectoryInfo MainTempDirectory { get; set; }

        public void PopulateInitialValues()
        {
            ArrayOfDirectories = Directory.GetDirectories(TEMP_DIRECTORY_PATH);
            ArrayOfFilesOutSideSubDirectories = Directory.GetFiles(TEMP_DIRECTORY_PATH);
            MainTempDirectory = new DirectoryInfo(TEMP_DIRECTORY_PATH);
        }

        public void ShowAllDirectoriesAndFilesInTheTempFolder()
        {
            Console.WriteLine("List of Directories: ");
            for (int i = 0; i < ArrayOfDirectories.Length; i++)
            {
                Console.WriteLine(ArrayOfDirectories[i]);
            }

            Console.WriteLine("------");
            Console.WriteLine();

            Console.WriteLine("List of files outside of subdirectories: ");
            for (int i = 0; i < ArrayOfFilesOutSideSubDirectories.Length; i++)
            {
                Console.WriteLine(ArrayOfFilesOutSideSubDirectories[i]);
            }
        }

        public void DeleteAllDirectoriesAndFiles()
        {
            DeleteAllFilesInMainTempDirectory();

            Console.WriteLine("Accessing directories...");

            if (ArrayOfDirectories.Length > 0)
                for (int i = 0; i < ArrayOfDirectories.Length; i++)
                {
                    Console.WriteLine($"Accessing diretory: {ArrayOfDirectories[i]}...");
                    DirectoryInfo currentDirectory = new DirectoryInfo(ArrayOfDirectories[i]);

                    FileInfo[] filesInCurrentDirectory = currentDirectory.GetFiles();

                    if (filesInCurrentDirectory.Length > 0)
                    {
                        for (int j = 0; j < filesInCurrentDirectory.Length; j++)
                        {
                            try
                            {
                                filesInCurrentDirectory[j].Delete();
                            }
                            catch (Exception ex)
                            {
                                Utils.TreatConsoleMessageForException($"Couldn't delete the file because of the following error: {ex.Message}");
                            }
                        }
                    }

                    if (currentDirectory.GetFiles().Length == 0)
                    {
                        DeleteAllSubDirectoriesInDirectory(ArrayOfDirectories[i]);
                    }
                    else
                        Utils.TreatConditionalMessage($"Can't delete directory '{currentDirectory.Name}' because it's not empty!");

                    Console.WriteLine();
                }
            else
                Console.WriteLine("There are no directories in the temp folder for me to delete!");
        }

        public void DeleteAllFilesInMainTempDirectory()
        {
            FileInfo[] filesOutsideSubDirectories = MainTempDirectory.GetFiles();

            Console.WriteLine($"Deleting files outside the sub-directories...");
            if (ArrayOfFilesOutSideSubDirectories.Length > 0)
            {
                for (int i = 0; i < ArrayOfFilesOutSideSubDirectories.Length; i++)
                {
                    try
                    {
                        filesOutsideSubDirectories[i].Delete();
                        Utils.TreatSuccessMessage($"File '{filesOutsideSubDirectories[i].Name}' deleted!");
                    }
                    catch (Exception ex)
                    {
                        Utils.TreatConsoleMessageForException($"Couldn't delete the file because of the following error: {ex.Message}");
                    }
                }
            }
            else
                Utils.TreatConditionalMessage("There are no files in the temp folder for me to delete!");
        }

        private void DeleteAllSubDirectoriesInDirectory(string currentDirectoryName)
        {
            Console.WriteLine($"Deleting directory: {currentDirectoryName}");
            Directory.Delete(currentDirectoryName, true);
            Utils.TreatSuccessMessage($"Directory: '{currentDirectoryName} deleted!'");
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
