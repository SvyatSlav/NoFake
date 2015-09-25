using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using NoFake.IO.Helpers;

namespace NoFake.IO
{
    /// <summary>
    /// Provides support for creating <c>Directory</c> instance
    /// </summary>
    public static class DirectoryFactory
    {
        /// <summary>
        /// Get new single Directory
        /// </summary>
        /// <param name="folderPath">Path to destination folder. Could be a rooted, but in this way root part was take away. Could be empty or null -- return first-level directory with random path</param>
        /// <returns></returns>
        public static NDirectory New(string folderPath = null)
        {
            Contract.Ensures(Contract.Result<NDirectory>() != null);

            //Converted path to correct format

            if (String.IsNullOrEmpty(folderPath))
            {
                return new NDirectory();
            }

            var path = Helper.GetFormatedPath(folderPath);
            return new NDirectory(path);
        }

        /// <summary>
        /// Adds the specified parent directory.
        /// </summary>
        /// <param name="parentDirectory">The parent directory.</param>
        /// <param name="folderName">The folder name, only one-level path</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">@FolderName contains invalid directory separator char, example '\'</exception>
        public static NDirectory Add(NDirectory parentDirectory, string folderName)
        {
            Contract.Requires<ArgumentNullException>(parentDirectory != null);
            Contract.Requires<ArgumentNullException>(folderName != null);


            Helper.CheckForSeparatorChar(folderName);

            var dir = new NDirectory(parentDirectory, folderName);
            parentDirectory.AddChild(dir);
            return dir;
        }
    }
}