using System;
using System.Collections.Generic;
using System.IO;
using NoFake.IO.Extensions;

namespace NoFake.IO.Tests.FolderFileDisposing
{
    /// <summary>
    /// Simple transfer with 1-level folders and images. Transfer\Folder{0}\{1}.jpg
    /// </summary>
    public class SimpleTransfer : IDisposable
    {
        private readonly int _imageCount;
        private readonly string _imageSuffix;
        private NDirectory _rootFolder;


        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        /// <param name="transferName">Name of the transfer.</param>
        /// <param name="countFolder">The count folder.</param>
        /// <param name="imageCount">The image count.</param>
        /// <param name="imageSuffix">The image suffix.</param>
        public SimpleTransfer(string transferName, int countFolder, int imageCount = 3, string imageSuffix = null)
        {
            _imageCount = imageCount;
            _imageSuffix = imageSuffix;
            _rootFolder = DirectoryFactory.New(transferName);

            AddFolderWithImage(countFolder);
        }

        private void AddFolderWithImage(int countFolder)
        {
            for (int i = 0; i < countFolder; i++)
            {
                var folderName = "Folder" + i;

                var folder = _rootFolder.Add<NDirectory>(folderName);

                AddImageToFolder(folder, _imageCount, _imageSuffix);
            }
        }

        private void AddImageToFolder(NDirectory folder, int countImage, string imageSuffix)
        {
            var content = new byte[] {0};

            for (int i = 0; i < countImage; i++)
            {
                folder.Add<NFile>(String.Format("{1}{0}.jpg", i, imageSuffix ?? String.Empty), content);
            }
        }

        public string TranferPath
        {
            get
            {
                if (_rootFolder == null)
                {
                    return null;
                }
                return _rootFolder.FullPath;
            }
        }

        public string RootPath
        {
            get { return _rootFolder.With(f => Directory.GetParent(f.FullPath).FullName); }
        }

        #region Dispose

        public void Dispose()
        {
            Disposing();
            GC.SuppressFinalize(this);
        }

        private void Disposing()
        {
            _rootFolder.Dispose();
            _rootFolder = null;
        }

        ~SimpleTransfer()
        {
            Disposing();
        }

        #endregion
    }
}