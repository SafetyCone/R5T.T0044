using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using R5T.Magyar;
using R5T.Magyar.IO;

using R5T.T0044;


namespace System
{
    public static class IFileSystemOperatorExtensions
    {
        // Writes nothing to a file, clearing it.
        public static async Task ClearFile(this IFileSystemOperator _,
            string filePath)
        {
            await File.WriteAllTextAsync(
                filePath,
                String.Empty);
        }

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

        public static IEnumerable<string> FindFilesInDirectoryOrDirectParentDirectories(this IFileSystemOperator _,
            string directoryPath,
            string searchPattrn)
        {
            var childFilePaths = _.FindFilesInDirectory(
                directoryPath,
                searchPattrn);

            var directoryInfo = new DirectoryInfo(directoryPath);

            var parentFilePaths = directoryInfo.IsRoot()
                ? Enumerable.Empty<string>()
                : _.FindFilesInDirectoryOrDirectParentDirectories(
                    directoryInfo.Parent.FullName,
                    searchPattrn)
                ;

            var output = childFilePaths.Concat(parentFilePaths);
            return output;
        }

        public static IEnumerable<string> EnumerateAllChildDirectoryPaths(this IFileSystemOperator _,
            string directoryPath)
        {
            var output = DirectoryHelper.EnumerateAllChildDirectoryPaths(directoryPath);
            return output;
        }

        public static IEnumerable<string> EnumerateAllChildFilePaths(this IFileSystemOperator _,
            string directoryPath)
        {
            var output = DirectoryHelper.EnumerateAllChildFilePaths(directoryPath);
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

        public static IEnumerable<string> EnumerateAllDescendentFilePaths(this IFileSystemOperator _,
            string directoryPath,
            string searchPattern)
        {
            var output = _.EmptyIfDirectoryNotExistsOr(
                directoryPath,
                xDirectoryPath =>
                {
                    var output = DirectoryHelper.EnumerateDescendentFilePaths(
                        xDirectoryPath,
                        searchPattern);

                    return output;
                });

            return output;
        }

        public static IEnumerable<string> EmptyIfDirectoryNotExistsOr(this IFileSystemOperator _,
            string directoryPath,
            Func<string, IEnumerable<string>> directoryPathOperation)
        {
            var directoryExists = _.DirectoryExists(directoryPath);

            var output = directoryExists
                ? directoryPathOperation(directoryPath)
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

        public static bool IsDirectoryEmpty(this IFileSystemOperator _,
            string directoryPath)
        {
            var anyChildFilePaths = _.EnumerateAllChildFilePaths(directoryPath).Any();
            var anyChildDirectoryPaths = _.EnumerateAllChildDirectoryPaths(directoryPath).Any();

            var output = !(anyChildDirectoryPaths || anyChildFilePaths);
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

        public static void VerifyDirectoryExists(this IFileSystemOperator _,
            string directoryPath)
        {
            var directoryExists = _.DirectoryExists(directoryPath);
            if (!directoryExists)
            {
                throw new Exception($"Directory does not exist:\n{directoryPath}");
            }
        }

        public static void VerifyDirectoryDoesNotAlreadyExist(this IFileSystemOperator _,
            string directoryPath)
        {
            var directoryExists = _.DirectoryExists(directoryPath);
            if (directoryExists)
            {
                throw new Exception($"Directory already exists:\n{directoryPath}");
            }
        }

        public static void VerifyFileExists(this IFileSystemOperator _,
            string filePath)
        {
            var fileExists = _.FileExists(filePath);
            if (!fileExists)
            {
                throw new FileNotFoundException("File path does not exist.", filePath);
            }
        }

        public static void VerifyFilesExist(this IFileSystemOperator _,
            IEnumerable<string> filePaths)
        {
            _.VerifyFilesExist(filePaths.Now());
        }

        public static void VerifyFilesExist(this IFileSystemOperator _,
            params string[] filePaths)
        {
            var filePathsThatDontExist = filePaths
                .Distinct()
                .OrderAlphabetically()
                .Where(xFilePath => !_.FileExists(xFilePath))
                .Now();

            var anyFilePathsDontExist = filePathsThatDontExist.Any();
            if(anyFilePathsDontExist)
            {
                var message = $"File paths do not exist:\n{String.Join(Characters.NewLine, filePathsThatDontExist)}";

                throw new FileNotFoundException(message);
            }
        }
    }
}
