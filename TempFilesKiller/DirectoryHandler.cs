using System;
using System.IO;

namespace TempFilesKiller
{
    internal class DirectoryHandler
    {
        private string _path { get; set; }

        /// <summary>
        /// Receives a path argument on which it will operate.
        /// </summary>
        /// <param name="path"></param>
        public DirectoryHandler(string path)
        {
            _path = path;
        }

        /// <summary>
        /// Tries to delete files and sub directories inside the path given to this class' constructor.
        /// </summary>
        public void TryToDeleteFilesAndSubDirectories()
        {
            TryDeleteFiles(Directory.GetFiles(_path));
            TryDeleteSubDirectories(Directory.GetDirectories(_path));

        }

        /// <summary>
        /// Tries to delete the top-level files inside the given directory.
        /// </summary>
        private void TryDeleteFiles(string[] files)
        {
            foreach (var file in files)
            {
                try
                {
                    File.Delete(file);
                    Utils.TreatSuccessMessage($"Successfully deleted file: {file}");
                }
                catch (Exception ex)
                {
                    Utils.TreatExceptionMessage($"Could not delete the file because of the following error: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Tries to delete the sub-directories inside the given directory.
        /// </summary>
        private void TryDeleteSubDirectories(string[] directories)
        {
            foreach (var directory in directories)
            {
                try
                {
                    Directory.Delete(directory, true);
                    Utils.TreatSuccessMessage($"Successfully deleted directory: {directory}");
                }
                catch (Exception ex)
                {
                    Utils.TreatExceptionMessage($"Could not delete the file because of the following error: {ex.Message}");
                }
            }
        }
    }
}