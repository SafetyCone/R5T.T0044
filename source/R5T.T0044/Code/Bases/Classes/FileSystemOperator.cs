using System;


namespace R5T.T0044
{
    /// <summary>
    /// Empty implementation as base for extension methods.
    /// </summary>
    public class FileSystemOperator : IFileSystemOperator
    {
        #region Static

        public static FileSystemOperator Instance { get; } = new();

        #endregion
    }
}