using System;
using System.IO;
using NoFake.IO.Exceptions;
using NUnit.Framework;

namespace NoFake.IO.Tests
{
    public class DirectoryFactoryTest
    {
       
        [TestCase(@"Folder1\\\Folder2\\")]
        [TestCase(@"Folder1\Folder2\1.jpg")]
        [TestCase(@"C:\Folder1\Folder2\1.jpg")]
        public void GetNew_Path_IDirectory(string path)
        {
            using (var tempDir = DirectoryFactory.New(path))
            {
                Assert.True(Directory.Exists(tempDir.FullPath));
            }
        }

        [Test]
        public void GetNew_OverflowPathLength_Error()
        {
            //Simulate overflowStringPath
            String path = null;
            for (int i = 0; i < 9; i++)
            {
                path += Guid.NewGuid().ToString();
            }

            Assert.Throws<DirectoryTakeException>(() => DirectoryFactory.New(path));
        }

        [Test]
        public void GetNew_EmptyPath_DirectoryExist()
        {
            using (var tempDir = DirectoryFactory.New())
            {
                Assert.True(Directory.Exists(tempDir.FullPath));
            }
        }

        [Test]
        public void AddFolder_SeparatorPath_Throw()
        {
            using (var d = DirectoryFactory.New())
            {
                Assert.Throws<Exception>(() => FileFactory.Add(d, @"1\1"));
            }
        }
    }
}