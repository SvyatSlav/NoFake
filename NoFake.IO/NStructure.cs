using System;

namespace NoFake.IO
{
    /// <summary>
    /// Common structure with directories and files
    /// </summary>
    [Obsolete]
    internal sealed class NStructure : IDisposable
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
        ~NStructure()
        {
            Disposing();
        }

        #endregion
    }
}