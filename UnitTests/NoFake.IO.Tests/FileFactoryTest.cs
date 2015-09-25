using System;
using System.Collections.Generic;
using System.IO;
using NoFake.IO.Extensions;
using NUnit.Framework;


namespace NoFake.IO.Tests
{
    public class FileFactoryTest
    {
        private readonly List<NFile> _images = new List<NFile>();

        [TestCase(@"1.jpg")]
        [TestCase(@"017e009b-b11a-4a10-80fc-5c9156c80fcd.txt")]
        [TestCase(@"017e009b-b11a-4a10-80fc-5c9156c80fcd.txt")]
        public void GetEmpty_FileName_FileExist(string fileName)
        {
            using (var f = FileFactory.New(fileName))
            {
                Assert.True(File.Exists(f.FullPath));
            }
        }

        [Test]
        public void AddFileToFolder()
        {
            using (var d = DirectoryFactory.New())
            using (var f = FileFactory.Add(d, "1"))
            {
                Assert.That(File.Exists(f.FullPath), Is.True);
            }
        }

        [Test]
        public void AddFile_SeparatorPath_Throw()
        {
            using (var d = DirectoryFactory.New())
            {
                Assert.Throws<Exception>(() => FileFactory.Add(d, @"1\1"));
            }
        }

        [Test]
        public void AddFileToFolder_GCCollect_FileExist()
        {
            var d = DirectoryFactory.New();
            var f = FileFactory.Add(d, "1");

            HelperTests.GcCollect();

            Assert.That(File.Exists(f.FullPath), Is.True);
        }


        [Test]
        public void AddFileWithExtension_Exist()
        {
            var dir = DirectoryFactory.New("Test");


            var fileA = dir.Add<NFile>("0001.jpg");

            var fileB = dir.Add<NFile>("0002.jpg");


            HelperTests.GcCollect();

            Assert.That(File.Exists(fileA.FullPath), Is.True);
            Assert.That(File.Exists(fileB.FullPath), Is.True);
        }

        [Test]
        public void AddFile_SaveInList_FileExist()
        {
            var dir = DirectoryFactory.New("Test");


            var fileA = dir.Add<NFile>("0001.jpg");
            var fileB = dir.Add<NFile>("0002.jpg");

            _images.Add(fileA);
            _images.Add(fileB);

            var pathA = fileA.FullPath;
            var pathB = fileB.FullPath;

            HelperTests.GcCollect();

            Assert.That(File.Exists(pathA), Is.True);
            Assert.That(File.Exists(pathB), Is.True);
        }

        [Test]
        public void AddFile_NotAssignToVar_FileExist()
        {
            var dir = DirectoryFactory.New("Test");
            const string itemName = "1.jpg";
            dir.Add<NFile>(itemName);

            HelperTests.GcCollect();

            Assert.That(File.Exists(Path.Combine(dir.FullPath, itemName)), Is.True);
        }

        [Test]
        public void AddFile_TwoLevelDirectory_FileExist()
        {
            var d = DirectoryFactory.New("root");
            var folder = d.Add<NDirectory>("folder");

            const string itemName = "1.jpg";
            folder.Add<NFile>(itemName);

            HelperTests.GcCollect();

            Assert.That(File.Exists(Path.Combine(folder.FullPath, itemName)), Is.True);
        }
    }
}