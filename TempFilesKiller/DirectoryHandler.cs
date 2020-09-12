using System;
using System.IO;
using System.Threading.Tasks;

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
        public async Task TryToDeleteFilesAndSubDirectories()
        {
            Task taskForFileDeletion = await Task.Factory.StartNew(() => TryDeleteFiles(Directory.GetFiles(_path)));
            Task taskForDirectoryDeletion = await Task.Factory.StartNew(() => TryDeleteFiles(Directory.GetDirectories(_path)));

            await Task.CompletedTask;
        }

        /// <summary>
        /// Tries to delete the top-level files inside the given directory.
        /// </summary>
        private Task TryDeleteFiles(string[] files)
        {
            Parallel.ForEach(files, new ParallelOptions { MaxDegreeOfParallelism = 3 }, file =>
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
            });

            return Task.CompletedTask;
        }

        /// <summary>
        /// Tries to delete the sub-directories inside the given directory.
        /// </summary>
        private Task TryDeleteSubDirectories(string[] directories)
        {
            Parallel.ForEach(directories, new ParallelOptions { MaxDegreeOfParallelism = 3 }, directory =>
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
            });

            return Task.CompletedTask;
        }
    }
}