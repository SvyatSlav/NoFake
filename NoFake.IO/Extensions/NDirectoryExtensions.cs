using System;
using System.Diagnostics.Contracts;

namespace NoFake.IO.Extensions
{
    /// <summary>
    /// Extensions for NDirectory class
    /// </summary>
    public static class NDirectoryExtensions
    {
        /// <summary>
        /// Add item to parentFolder
        /// </summary>
        /// <typeparam name="TReturn">The type of the return.</typeparam>
        /// <param name="parent">ParentDirectory</param>
        /// <param name="itemName">Name of the item, file or folder</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Invalid type of item</exception>
        public static TReturn Add<TReturn>(this NDirectory parent, string itemName) where TReturn : class
        {
            Contract.Requires<ArgumentNullException>(parent != null);
            Contract.Requires<ArgumentNullException>(itemName != null);

            var type = typeof (TReturn);


            if (type == typeof (NDirectory))
            {
                return DirectoryFactory.Add(parent, itemName) as TReturn;
            }

            if (type == typeof (NFile))
            {
                return FileFactory.Add(parent, itemName) as TReturn;
            }


            throw new Exception("Invalid TReturn type");
        }

        /// <summary>
        /// Adds file to folder with content
        /// </summary>
        /// <typeparam name="TReturn">The type of the return.</typeparam>
        /// <param name="parent">The parent.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public static TReturn Add<TReturn>(this NDirectory parent, string fileName, byte[] content) where TReturn : NFile
        {
            Contract.Requires<ArgumentNullException>(parent != null);
            Contract.Requires<ArgumentNullException>(fileName != null);

            return FileFactory.Add(parent, fileName, content) as TReturn;
        }

        #region Fluent

        public static NDirectory Add(this NDirectory current, string directoryName)
        {
            if (current == null)
            {
                return null;
            }

            var newDir = DirectoryFactory.Add(current, directoryName);

            return current;
        }

        //TODO Method name?
        public static NDirectory AddAndIn(this NDirectory current, string directoryName)
        {
            if (current == null)
            {
                return null;
            }


            return DirectoryFactory.Add(current, directoryName);

        }

        public static NDirectory Up(this NDirectory current)
        {
            if (current == null)
            {
                return null;
            }

            var parent = current.Parent;

            if (parent == null)
            {
                return current;
            }

            return parent;

        }

        public static NDirectory New(string folderName)
        {
            return DirectoryFactory.New(folderName);
        }

        //TODO Add(NFile)

        //TODO Add(NFile [])

        //TODO Add(string[] folders)

        //TODO Add("1/1/1")

        #endregion
    }
}