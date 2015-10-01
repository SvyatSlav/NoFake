using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using NoFake.IO.Exceptions;
using NoFake.IO.Helpers;

namespace NoFake.IO
{
    /// <summary>
    /// Directory
    /// </summary>
    public class NDirectory : IDisposable
    {
        private readonly string _rootFolder;


        /// <summary>
        /// Full path to Directory
        /// </summary>
        public string FullPath { get; private set; }

        public NDirectory Parent { get; private set; }

        /// <summary>
        /// Occurs when directory is delete.
        /// </summary>
        public event EventHandler DirectoryDeleted = delegate { };

        private List<NFile> _fileChildren = new List<NFile>();
        private List<NDirectory> _folderChildren = new List<NDirectory>();

        internal NDirectory(string folderName) : this()
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrEmpty(folderName), "FolderName is empty");
            Contract.Requires<ArgumentException>(!Path.IsPathRooted(folderName), "FolderName can't be a rooted path");

            FullPath = Path.Combine(_rootFolder, folderName);


            CreateDirectory(FullPath);
        }


        internal NDirectory()
        {
            //TODO в фабрику

            //Get disk letter from executing path
            var diskLetter = Helper.GetExecutingDiskLetter();

            var pathGuid = Guid.NewGuid().ToString();

            _rootFolder = Path.Combine(diskLetter, pathGuid);

            FullPath = _rootFolder;

            //Create directory
            CreateDirectory(FullPath);
        }

        internal NDirectory(NDirectory parentDirectory, string folderName)
        {
            Contract.Requires<ArgumentNullException>(parentDirectory != null);
            Contract.Requires<ArgumentNullException>(folderName != null);

            Parent = parentDirectory;
            FullPath = Path.Combine(parentDirectory.FullPath, folderName);

            CreateDirectory(FullPath);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Disposing();

            //Send message to lister for disposing them
            DirectoryDeleted(this, EventArgs.Empty);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposings this instance.
        /// </summary>
        protected virtual void Disposing()
        {
            if (_fileChildren != null)
            {
                _fileChildren.ForEach(f => f.Dispose());
                _fileChildren.Clear();
                _fileChildren = null;
            }

            if (_folderChildren != null)
            {
                _folderChildren.ForEach(f => f.Dispose());
                _folderChildren.Clear();
                _folderChildren = null;
            }


            //Delete rootFolder
            if (_rootFolder != null && Directory.Exists(_rootFolder))
            {
                IOManager.DeleteDirectory(_rootFolder, true);
            }
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="NDirectory"/> class.
        /// </summary>
        ~NDirectory()
        {
            Disposing();
        }

        private void CreateDirectory(string folderName)
        {
            try
            {
                if (Directory.Exists(FullPath))
                {
                    throw new Exception("directory  already exist");
                }

                Directory.CreateDirectory(FullPath);
            }
            catch (Exception ex)
            {
                throw new DirectoryTakeException(
                    String.Format("Directory '{0}' constructor exception: {1}", folderName, ex.Message), ex);
            }
        }

        internal void AddChild(NFile file)
        {
            Contract.Requires(file != null);
            _fileChildren.Add(file);
        }

        internal void AddChild(NDirectory file)
        {
            Contract.Requires(file != null);
            _folderChildren.Add(file);
        }

        public static NDirectory New()
        {
            return DirectoryFactory.New();
        }

        public static NDirectory New(string folderName)
        {
            return DirectoryFactory.New(folderName);
        }

    }
}