using System;

using R5T.Magyar;

using R5T.T0044;


namespace System
{
    public static class IFileSystemOperatorExtensions
    {
        /// <summary>
        /// Provides the output-directory executable file path equivalent for testing.
        /// In a testing context (MS test), the assumed executable file path might be very different than the test executable's file path.
        /// This causes problems when trying to find files that were copied to the output directory (for example, reference files used in testing).
        /// </summary>
        public static string GetExecutableFilePathEquivalentForTesting(this IFileSystemOperator _)
        {
            // As-of .NET 5.0, this seems to have gotten easier.

            var output = ExecutableFilePathHelper.GetEntryAssemblyValue();
            return output;
        }
    }
}