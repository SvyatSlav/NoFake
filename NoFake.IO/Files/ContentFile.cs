using System;
using System.Diagnostics.Contracts;
using NoFake.IO.Helpers;

namespace NoFake.IO.Files
{
    internal class ContentFile : NFile
    {
        private readonly byte[] _content;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="content">The content.</param>
        public ContentFile(NDirectory directory, string fileName, byte[] content) : base(directory, fileName)
        {
           
            Contract.Requires<ArgumentNullException>(content != null);
            _content = content;
        }

        public override void Create()
        {
           IOManager.CreateFileWithContent(FullPath, _content);
        }
    }
}