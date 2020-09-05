using System;
using System.IO;

namespace TempFilesKiller
{
    internal class DirectoryHandler
    {
        private const string TEMP_FOLDER_PATH = "C:/Users/highl/AppData/Local/Temp";
        private DirectoryInfo _mainTempDirectory { get; set; }

        internal DirectoryHandler()
        {
            _mainTempDirectory = new DirectoryInfo(TEMP_FOLDER_PATH);
        }

        /// <summary>
        /// Tries to delete all of the directories and files in the path provided to an instance of this class.
        /// </summary>
        internal void TryToDeleteAllDirectoriesAndFiles()
        {
            FilesHandler.TryToDeleteAllFilesInMainTempDirectory(_mainTempDirectory);

            if (_mainTempDirectory.GetDirectories().Length > 0)
                TryToDeleteFilesAndSubDirectories();
            else
                Utils.TreatConditionalMessage("There are no directories in the temp folder to delete!");
        }

        /// <summary>
        /// Tries to delete files and sub directories inside the directory.
        /// </summary>
        private void TryToDeleteFilesAndSubDirectories()
        {
            DirectoryInfo[] directoriesInsideMainTempDirectory = _mainTempDirectory.GetDirectories();

            for (int i = 0; i < directoriesInsideMainTempDirectory.Length; i++)
            {
                FilesHandler.TryToDeleteFilesInDirectory(directoriesInsideMainTempDirectory[i]);
                SubDirectoryHandler.CheckIfThereAreStillFilesLeftThenDeleteDirectory(directoriesInsideMainTempDirectory[i]);
            }
        }
    }
}