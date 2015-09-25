using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NoFake.IO
{
    /// <summary>
    /// Directory
    /// </summary>
    public interface IDirectory : IDisposable
    {
        /// <summary>
        /// Full path to Directory
        /// </summary>
        string FullPath { get; }
        
        /// <summary>
        /// Occurs when directory is delete.
        /// </summary>
        event EventHandler DirectoryDeleted;
    }
}