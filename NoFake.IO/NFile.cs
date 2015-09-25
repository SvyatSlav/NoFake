using System;
using System.Diagnostics.Contracts;
using System.IO;
using NoFake.IO.Exceptions;
using NoFake.IO.Helpers;

namespace NoFake.IO
{
    /// <summary>
    /// File
    /// </summary>
    public abstract class NFile : IDisposable
    {
        /// <summary>
        /// Full path to file
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// Name of the file
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        public string FileName { get; set; }

        /// <summary>
        /// Occurs when file is deletes.
        /// </summary>
        public event EventHandler FileDeleted = delegate { };

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Disposing();
            FileDeleted(this, EventArgs.Empty);
            GC.SuppressFinalize(this);
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="fileName">Name of the file.</param>
        protected internal NFile(NDirectory directory, string fileName)
        {
            Contract.Requires<ArgumentException>(directory != null, "NDirectory is null");
            Contract.Requires<ArgumentException>(!String.IsNullOrEmpty(fileName), "FileName is empty");
            Contract.Requires<ArgumentException>(!Path.IsPathRooted(fileName), "FileName can't be a rooted path");

            FileName = fileName;

            try
            {
                CheckBeforeCreate(directory);
            }
            catch (Exception ex)
            {
                ThrowConstructorException(ex);
            }
        }

        /// <summary>
        /// Creates file. Override in inheritors
        /// </summary>
        public abstract void Create();


        /// <summary>
        /// Throws the constructor exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <exception cref="FileTakeException"></exception>
        private void ThrowConstructorException(Exception ex)
        {
            Contract.Requires(ex != null);
            throw new FileTakeException(String.Format("NFile '{0}' constructor exception: {1}", FileName, ex.Message), ex);
        }

        private void CheckBeforeCreate(NDirectory directory)
        {
            Contract.Requires(directory != null);
            FullPath = Path.Combine(directory.FullPath, FileName);

            if (File.Exists(FullPath))
            {
                throw new Exception("file already exist");
            }
        }

        /// <summary>
        /// Disposings this instance.
        /// </summary>
        protected virtual void Disposing()
        {
            //Delete file
            if (FullPath != null && File.Exists(FullPath))
            {
                IOManager.DeleteFile(FullPath);
            }
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="NFile"/> class.
        /// </summary>
        ~NFile()
        {
            Disposing();
        }
    }
}