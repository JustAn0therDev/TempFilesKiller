using System;
using System.IO;

namespace TempFilesKiller
{
    internal class DirectoryHandler
    {
        private const string TEMP_FOLDER_PATH = "C:/Users/highl/AppData/Local/Temp";

        private string[] _arrayOfDirectories { get; set; }
        private string[] _arrayOfFilenamesOutsideSubDirectories { get; set; }
        private DirectoryInfo _mainTempDirectory { get; set; }

        internal DirectoryHandler()
        {
            _arrayOfDirectories = Directory.GetDirectories(TEMP_FOLDER_PATH);
            _arrayOfFilenamesOutsideSubDirectories = Directory.GetFiles(TEMP_FOLDER_PATH);
            _mainTempDirectory = new DirectoryInfo(TEMP_FOLDER_PATH);
        }

        internal void DeleteAllDirectoriesAndFiles()
        {
            MainTempDirectoryHandler.DeleteAllFilesInMainTempDirectory(_mainTempDirectory, _arrayOfFilenamesOutsideSubDirectories);

            if (_arrayOfDirectories.Length > 0)
                TryToDeleteFilesAndSubDirectories();
            else
                Utils.TreatConditionalMessage("There are no directories in the temp folder to delete!");
        }

        private void TryToDeleteFilesAndSubDirectories()
        {
            for (int i = 0; i < _arrayOfDirectories.Length; i++)
            {
                Console.WriteLine($"Accessing diretory: {_arrayOfDirectories[i]}...");
                DirectoryInfo currentDirectory = new DirectoryInfo(_arrayOfDirectories[i]);
                TryToDeleteFilesInDirectory(currentDirectory);
                CheckIfThereAreStillFilesLeftInDirectory(currentDirectory, _arrayOfDirectories[i]);
                Console.WriteLine();
            }
        }

        private void TryToDeleteFilesInDirectory(DirectoryInfo currentDirectory)
        {
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
                        Utils.TreatExceptionMessage($"Couldn't delete the file because of the following error: {ex.Message}");
                    }
                }
            }
        }

        private void CheckIfThereAreStillFilesLeftInDirectory(DirectoryInfo currentDirectory, string directoryName)
        {
            if (currentDirectory.GetFiles().Length == 0)
                SubDirectoryHandler.DeleteAllSubDirectoriesInDirectory(directoryName);
            else
                Utils.TreatConditionalMessage($"Can't delete directory '{currentDirectory.Name}' because it's not empty!");
        }
    }
}