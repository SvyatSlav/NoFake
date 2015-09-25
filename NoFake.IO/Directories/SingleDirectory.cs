using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using NoFake.IO.Exceptions;
using NoFake.IO.Helpers;

namespace NoFake.IO.Directories
{
    internal class SingleDirectory : IDirectory
    {
        public string FullPath { get; private set; }
      
        private readonly string _rootFolder;
       


        public event EventHandler DirectoryDeleted = delegate { };

        internal SingleDirectory(string folderName) : this()
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrEmpty(folderName), "FolderName is empty");
            Contract.Requires<ArgumentException>(!Path.IsPathRooted(folderName), "FolderName can't be a rooted path");
            
            FullPath = Path.Combine(_rootFolder, folderName);


            CreateDirectory(FullPath);
        }


        internal SingleDirectory()
        {
            //Get disk letter from executing path
            var diskLetter = Helper.GetExecutingDiskLetter();

            var pathGuid = Guid.NewGuid().ToString();

            _rootFolder = Path.Combine(diskLetter, pathGuid);

            //Create directory
            Directory.CreateDirectory(_rootFolder);

            FullPath = _rootFolder;
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
                    String.Format("SingleDirectory '{0}' constructor exception: {1}", folderName, ex.Message), ex);
            }
        }

      
        #region Dispose

        public void Dispose()
        {
            //Send message to lister for disposing them
            DirectoryDeleted(this, new EventArgs());

            Disposing();
            GC.SuppressFinalize(this);
        }

        private void Disposing()
        {
            //Delete rootFolder
            if (_rootFolder != null && Directory.Exists(_rootFolder))
            {
                IOManager.DeleteDirectory(_rootFolder, true);
            }
            
        }

        ~SingleDirectory()
        {
            Disposing();
        }

        #endregion
    }
}