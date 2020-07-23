using System;
using System.IO;

namespace TempFilesKiller
{
    internal static class SubDirectoryHandler
    {
        internal static void CheckIfThereAreStillFilesLeftThenDeleteDirectory(DirectoryInfo currentDirectory, string directoryName)
        {
            if (currentDirectory.GetFiles().Length > 0)
                Utils.TreatConditionalMessage($"Can't delete directory '{currentDirectory.Name}' because it's not empty!");
            else
                DeleteAllSubDirectoriesInDirectory(directoryName);
        }

        internal static void DeleteAllSubDirectoriesInDirectory(string currentDirectoryName)
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
