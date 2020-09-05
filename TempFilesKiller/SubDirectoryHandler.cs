using System;
using System.IO;

namespace TempFilesKiller
{
    internal static class SubDirectoryHandler
    {
        /// <summary>
        /// Check for existing files in a directory then deletes it if there is none.
        /// </summary>
        /// <param name="currentDirectory"></param>
        /// <param name="directoryName"></param>
        internal static void CheckIfThereAreStillFilesLeftThenDeleteDirectory(DirectoryInfo currentDirectory)
        {
            if (currentDirectory.GetFiles().Length > 0)
                Utils.TreatConditionalMessage($"Can't delete directory '{currentDirectory.Name}' because it's not empty!");
            else
                TryToDeleteAllSubDirectoriesInDirectory(currentDirectory.Name);
        }

        /// <summary>
        /// Tries to delete all subdirectories in a directory.
        /// </summary>
        /// <param name="currentDirectoryName"></param>
        internal static void TryToDeleteAllSubDirectoriesInDirectory(string currentDirectoryName)
        {
            try
            {
                Directory.Delete(currentDirectoryName, true);
                Utils.TreatSuccessMessage($"Directory: '{currentDirectoryName} deleted!'");
            }
            catch (Exception ex)
            {
                Utils.TreatExceptionMessage($"Couldn't delete the current directory because of the following error: {ex.Message}");
            }
        }
    }
}
