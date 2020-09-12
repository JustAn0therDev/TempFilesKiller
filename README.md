# TempFilesKiller
A temp directory and file deletion console application made with .NET Core.

## Notes
Another program was created to delete the content in the Windows 10 /Temp folder, but that [was made with Python 3](https://github.com/JustAn0therDev/python_temp_folder_killer). I had some trouble making it work because of some operating system access issues and decided to go with something that was "Microsoft native". It did work and, although more verbose, it's a lot faster.

If you want to use it, just change the directory which you want to empty in the Program class. It should work as long as you execute the published application with Admin privilege.

If there is a file or directory that can't be deleted, an `Exception` will be thrown and immediatly written to the console. The most common reasons are either a process was using something inside that directory or access to it was denied (that's why it's required to execute the application with admin privilege).
