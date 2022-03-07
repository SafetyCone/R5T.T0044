using System;

using R5T.T0044;

using Instances = R5T.T0044.X002.Instances;


namespace System
{
    public static class IFileSystemOperatorExtensions
    {
        public static void EnsureDirectoryForFileExists(this IFileSystemOperator _,
            string filePath)
        {
            var directoryPathForFilePath = Instances.PathOperator.GetDirectoryPathOfFilePath(filePath);

            _.CreateDirectoryOkIfExists(directoryPathForFilePath);
        }
    }
}