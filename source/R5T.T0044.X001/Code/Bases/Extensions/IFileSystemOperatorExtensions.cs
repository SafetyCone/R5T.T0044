using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using R5T.Magyar;
using R5T.Magyar.IO;

using R5T.T0044;


namespace System
{
    public static class IFileSystemOperatorExtensions
    {
        public static void CopyFile(this IFileSystemOperator _,
            string sourceFilePath,
            string destinationFilePath,
            bool overwrite = true)
        {
            File.Copy(sourceFilePath, destinationFilePath, overwrite);
        }

        public static void CreateDirectoryOkIfExists(this IFileSystemOperator _,
            string directoryPath)
        {
            DirectoryHelper.CreateDirectoryOkIfExists(directoryPath);   
        }

        /// <summary>
        /// Chooses <see cref="CreateDirectoryOkIfExists(IFileSystemOperator, string)"/> as the default.
        /// </summary>
        public static void CreateDirectory(this IFileSystemOperator _,
            string directoryPath)
        {
            _.CreateDirectoryOkIfExists(directoryPath);
        }

        public static void DeleteDirectoryOkIfNotExists(this IFileSystemOperator _,
            string directoryPath)
        {
            DirectoryHelper.DeleteDirectoryOkIfNotExists(directoryPath);
        }

        /// <summary>
        /// Chooses <see cref="DeleteDirectoryOkIfNotExists(IFileSystemOperator, string)"/> as the default.
        /// </summary>
        public static void DeleteDirectory(this IFileSystemOperator _,
            string directoryPath)
        {
            _.DeleteDirectoryOkIfNotExists(directoryPath);
        }

        public static void DeleteFileOkIfNotExists(this IFileSystemOperator _,
            string filePath)
        {
            FileHelper.DeleteOkIfNotExists(filePath);
        }

        /// <summary>
        /// Chooses <see cref="DeleteFileOkIfNotExists(IFileSystemOperator, string)"/> as the default.
        /// </summary>
        public static void DeleteFile(this IFileSystemOperator _,
            string filePath)
        {
            _.DeleteFileOkIfNotExists(filePath);
        }

        public static bool DirectoryExists(this IFileSystemOperator _,
            string directoryPath)
        {
            var output = Directory.Exists(directoryPath);
            return output;
        }

        public static bool FileExists(this IFileSystemOperator _,
            string filePath)
        {
            var output = File.Exists(filePath);
            return output;
        }

        public static IEnumerable<string> FindFilesInDirectory(this IFileSystemOperator _,
            string directoryPath,
            string searchPattern)
        {
            var output = DirectoryHelper.EnumerateChildFilePaths(directoryPath, searchPattern);
            return output;
        }

        public static IEnumerable<string> EnumerateAllChildDirectoryPaths(this IFileSystemOperator _,
            string directoryPath)
        {
            var output = DirectoryHelper.EnumerateAllChildDirectoryPaths(directoryPath);
            return output;
        }

        public static IEnumerable<string> EnumerateAllDescendentFilePaths(this IFileSystemOperator _,
            string directoryPath)
        {
            var output = DirectoryHelper.EnumerateAllDescendentFilePaths(directoryPath);
            return output;
        }

        public static IEnumerable<string> EnumerateAllDescendentFilePathsOrEmptyIfNotExists(this IFileSystemOperator _,
            string directoryPath)
        {
            var directoryExists = _.DirectoryExists(directoryPath);

            var output = directoryExists
                ? _.EnumerateAllDescendentFilePaths(directoryPath)
                : Enumerable.Empty<string>()
                ;

            return output;
        }

        public static string[] GetAllDescendentFilePathsOrEmptyIfNotExists(this IFileSystemOperator _,
            string directoryPath)
        {
            var output = _.EnumerateAllDescendentFilePathsOrEmptyIfNotExists(directoryPath)
                .ToArray();

            return output;
        }

        public static string FindFirstOrDefaultFileInDirectoryByFileExtension(this IFileSystemOperator _,
            string directoryPath,
            string fileExtension)
        {
            var searchPattern = SearchPatternHelper.AllFilesWithExtension(fileExtension);

            var output = _.FindFilesInDirectory(directoryPath, searchPattern)
                .FirstOrDefault();

            return output;
        }

        /// <summary>
        /// Selects <see cref="FindFirstOrDefaultFileInDirectoryByFileExtension(IFileSystemOperator, string, string)"/> as the default.
        /// </summary>
        public static string FindFileInDirectoryByFileExtension(this IFileSystemOperator _,
            string directoryPath,
            string fileExtension)
        {
            var output = _.FindFirstOrDefaultFileInDirectoryByFileExtension(directoryPath, fileExtension);
            return output;
        }

        public static DateTime GetFileModifiedDateTime(this IFileSystemOperator _,
            string filePath)
        {
            var output = File.GetLastWriteTime(filePath);
            return output;
        }

        public static WasFound<string> HasFileInDirectoryByFileExtensionFirst(this IFileSystemOperator _,
            string directoryPath,
            string fileExtension)
        {
            var filePathOrDefault = _.FindFirstOrDefaultFileInDirectoryByFileExtension(directoryPath, fileExtension);

            var output = WasFound.From(filePathOrDefault);
            return output;
        }

        /// <summary>
        /// Selects <see cref="HasFileInDirectoryByFileExtensionFirst(IFileSystemOperator, string, string)"/> as the default.
        /// </summary>
        public static WasFound<string> HasFileInDirectoryByFileExtension(this IFileSystemOperator _,
            string directoryPath,
            string fileExtension)
        {
            var output = _.HasFileInDirectoryByFileExtensionFirst(directoryPath, fileExtension);
            return output;
        }
    }
}
