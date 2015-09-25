using System;

namespace NoFake.IO
{
    /// <summary>
    /// FileSystem common structure with directories and files
    /// </summary>
    [Obsolete]
    public class FileSystem : IDisposable
    {
        #region Dispose

        /// <summary>
        /// Dispose fileSystem
        /// </summary>
        public void Dispose()
        {
            Disposing();
            GC.SuppressFinalize(this);
        }

        private void Disposing()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~FileSystem()
        {
            Disposing();
        }

        #endregion
    }
}