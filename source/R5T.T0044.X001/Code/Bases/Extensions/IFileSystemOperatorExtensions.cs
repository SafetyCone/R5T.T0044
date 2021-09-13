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

        public static IEnumerable<string> FindFilesInDirectory(this IFileSystemOperator _,
            string directoryPath,
            string searchPattern)
        {
            var output = DirectoryHelper.EnumerateFilePaths(directoryPath, searchPattern);
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
