using System;

using R5T.Magyar.IO;

using R5T.T0044;


namespace System
{
    public static class IFileSystemOperatorExtensions
    {
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
    }
}
