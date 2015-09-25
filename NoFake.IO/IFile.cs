using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NoFake.IO
{
    /// <summary>
    /// 
    /// </summary>
    public interface IFile : IDisposable
    {
        /// <summary>
        /// Full path to file
        /// </summary>
        string FullPath { get; }


        /// <summary>
        /// Occurs when file is deletes.
        /// </summary>
        event EventHandler FileDeleted;
    }
}