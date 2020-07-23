using System;
using System.IO;

namespace TempFilesKiller
{
    internal class DirectoryHandler
    {
        private const string TEMP_FOLDER_PATH = "C:/Users/highl/AppData/Local/Temp";

        private string[] _arrayOfDirectoriesInsideTempDirectory { get; set; }
        private string[] _arrayOfFilenamesOutsideSubDirectories { get; set; }
        private DirectoryInfo _mainTempDirectory { get; set; }

        internal DirectoryHandler()
        {
            _arrayOfDirectoriesInsideTempDirectory = Directory.GetDirectories(TEMP_FOLDER_PATH);
            _arrayOfFilenamesOutsideSubDirectories = Directory.GetFiles(TEMP_FOLDER_PATH);
            _mainTempDirectory = new DirectoryInfo(TEMP_FOLDER_PATH);
        }

        internal void DeleteAllDirectoriesAndFiles()
        {
            FilesHandler.DeleteAllFilesInMainTempDirectory(_mainTempDirectory, _arrayOfFilenamesOutsideSubDirectories);

            if (_arrayOfDirectoriesInsideTempDirectory.Length > 0)
                TryToDeleteFilesAndSubDirectories();
            else
                Utils.TreatConditionalMessage("There are no directories in the temp folder to delete!");
        }

        private void TryToDeleteFilesAndSubDirectories()
        {
            for (int i = 0; i < _arrayOfDirectoriesInsideTempDirectory.Length; i++)
            {
                DirectoryInfo currentDirectory = new DirectoryInfo(_arrayOfDirectoriesInsideTempDirectory[i]);

                FilesHandler.TryToDeleteFilesInDirectory(currentDirectory);
                SubDirectoryHandler.CheckIfThereAreStillFilesLeftThenDeleteDirectory(currentDirectory, _arrayOfDirectoriesInsideTempDirectory[i]);
            }
        }
    }
}