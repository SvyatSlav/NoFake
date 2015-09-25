using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using NoFake.IO.Files;
using NoFake.IO.Helpers;

namespace NoFake.IO
{
    /// <summary>
    /// Provides support for creating <c>File</c> instance
    /// </summary>
    public static class FileFactory
    {
        /// <summary>
        /// Gets the empty file in random directory
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        public static NFile New(string filePath)
        {
            Contract.Requires<ArgumentNullException>(!String.IsNullOrEmpty(filePath), "Initial path is null or empty");
            Contract.Ensures(Contract.Result<NFile>() != null);

            //Get corrected format path
            var path = Helper.GetFormatedPath(filePath);


            String folderPath = null;
            //Check for Folder in path
            if (path.Contains(Path.DirectorySeparatorChar))
            {
                //File + Dir
                folderPath = Path.GetDirectoryName(path);
                path = Path.GetFileName(path);
            }

            var dir = DirectoryFactory.New(folderPath);
            var file = new EmptyFile(dir, path);
            file.Create();
            //Subscribe file deleting
            file.FileDeleted += (sender, args) => dir.Dispose();

            return file;
        }

        /// <summary>
        /// Add empty file to directory
        /// </summary>
        /// <param name="parentDirectory">The directory.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public static NFile Add(NDirectory parentDirectory, string fileName)
        {
            Contract.Requires<ArgumentNullException>(parentDirectory != null);
            Contract.Requires<ArgumentNullException>(fileName != null);
            Contract.Ensures(Contract.Result<NFile>() != null);

            Helper.CheckForSeparatorChar(fileName);

            var file = new EmptyFile(parentDirectory, fileName);

            CreateFile(parentDirectory, file);

            return file;
        }

        /// <summary>
        /// Adds file to the specified parent directory.
        /// </summary>
        /// <param name="parentDirectory">The parent directory.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="content">The binary content, ex: text, image, etc</param>
        /// <returns></returns>
        public static NFile Add(NDirectory parentDirectory, string fileName, byte[] content)
        {
            Contract.Requires<ArgumentNullException>(parentDirectory != null);
            Contract.Requires<ArgumentNullException>(fileName != null);
            Contract.Ensures(Contract.Result<NFile>() != null);

            Helper.CheckForSeparatorChar(fileName);

            var file = new ContentFile(parentDirectory, fileName, content);
            CreateFile(parentDirectory, file);

            return file;
        }

        private static void CreateFile(NDirectory parentDirectory, NFile file)
        {
            file.Create();
            parentDirectory.AddChild(file);
        }
    }
}