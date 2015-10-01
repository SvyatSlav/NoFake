using System;
using System.IO;
using NoFake.IO.Extensions;
using NUnit.Framework;

namespace NoFake.IO.Tests
{
    /// <summary>
    /// Main test class shows library functional
    /// </summary>
    public class Tests
    {
        #region Directory

        [Test]
        public void TestEmptyFolderPathConstructor()
        {
            using (var dir = DirectoryFactory.New(String.Empty))
            {
                var directoryPath = dir.FullPath;

                Assert.That(Directory.Exists(directoryPath), Is.True);
            }

            using (var dir = DirectoryFactory.New())
            {
                var directoryPath = dir.FullPath;

                Assert.That(Directory.Exists(directoryPath), Is.True);
            }
        }

        [Test]
        public void TestEnsureDirectoryDeleted()
        {
            String path;
            using (var dir = DirectoryFactory.New())
            {
                path = dir.FullPath;
            }

            Assert.That(Directory.Exists(path), Is.False);
        }

        [Test]
        public void TestOneLevelFolder()
        {
            var path = "folder";

            using (var dir = DirectoryFactory.New(path))
            {
                var directoryPath = dir.FullPath;
                Assert.That(Directory.Exists(directoryPath), Is.True);

                Assert.That(directoryPath.EndsWith(path), Is.True);
            }
        }

        [Test]
        public void TestTwoLevelFolder()
        {
            var path = @"folder1\folder2";

            using (var dir = DirectoryFactory.New(path))
            {
                var directoryPath = dir.FullPath;
                Assert.That(Directory.Exists(directoryPath), Is.True);

                Assert.That(directoryPath.EndsWith(path), Is.True);
            }
        }

        [Test]
        public void TestThirdLevelFolder()
        {
            var path = @"folder1\Folder2\folder3";

            using (var dir = DirectoryFactory.New(path))
            {
                var directoryPath = dir.FullPath;
                Assert.That(Directory.Exists(directoryPath), Is.True);

                Assert.That(directoryPath.EndsWith(path), Is.True);
            }
        }


        [Test]
        public void TestFolderPathWithRoot()
        {
            var path = "folder";
            var root = @"C:\";
            var fullPath = root + path;

            using (var dir = DirectoryFactory.New(fullPath))
            {
                var directoryPath = dir.FullPath;
                Assert.That(Directory.Exists(directoryPath), Is.True);

                Assert.That(directoryPath.EndsWith(path), Is.True);
                Assert.That(directoryPath.Contains(fullPath), Is.False);
            }
        }

        [Test]
        [Ignore("NetworkPath not supported")]
        public void TestNetWorkPath()
        {
            var networkPath = @"\\127.0.0.1\Folder";
            var endPath = "Folder";
            using (var dir = DirectoryFactory.New(networkPath))
            {
                var directoryPath = dir.FullPath;
                Assert.That(Directory.Exists(directoryPath), Is.True);

                Assert.That(directoryPath.EndsWith(endPath), Is.True);
                Assert.That(directoryPath.Contains(networkPath), Is.False);
            }
        }

        [Test]
        public void TestFtpPath()
        {
            var ftpPath = @"ftp://127.0.0.1/Folder";
            var endPath = "Folder";
            using (var dir = DirectoryFactory.New(ftpPath))
            {
                var directoryPath = dir.FullPath;
                Assert.That(Directory.Exists(directoryPath), Is.True);

                Assert.That(directoryPath.EndsWith(endPath), Is.True);
                Assert.That(directoryPath.Contains(ftpPath), Is.False);
            }
        }

        [Test]
        public void TestAddFolder2Folder()
        {
            var folderName = @"folderChild";

            using (var d = DirectoryFactory.New())
            {
                d.Add<NDirectory>(folderName);
                Assert.That(Directory.Exists(Path.Combine(d.FullPath, folderName)));
            }

            //Or, factory Syntax
            using (var d = DirectoryFactory.New())
            {
                var subFolder = DirectoryFactory.Add(d, folderName);
                Assert.That(Directory.Exists(subFolder.FullPath));
            }
        }

        [Test]
        public void Fluent_CreateSimpleStructure()
        {
            using (var root = NDirectory.New())
            {
                root.Add("1").Add("2").Add("3");

                Assert.That(Directory.Exists(Path.Combine(root.FullPath,"1")));
                Assert.That(Directory.Exists(Path.Combine(root.FullPath, "2")));
                Assert.That(Directory.Exists(Path.Combine(root.FullPath, "3")));
            }
        }


        #endregion





        #region File

        [Test]
        public void TestFileConstructor()
        {
            var fileName = @"1.file";

            using (var file = FileFactory.New(fileName))
            {
                Assert.That(File.Exists(file.FullPath));
            }
        }

        [Test]
        public void TestFileWithFolder()
        {
            var fullPath = @"folder\1.file";

            using (var file = FileFactory.New(fullPath))
            {
                Assert.That(File.Exists(file.FullPath));
                Assert.That(file.FullPath.Contains(fullPath), Is.True);
            }
        }

        [Test]
        public void TestAddFileToFolder()
        {
            const string fileName = @"1.file";

            using (var d = DirectoryFactory.New())
            {
               var file = d.Add<NFile>(fileName);
                
                Assert.That(File.Exists(Path.Combine(d.FullPath, fileName)));
            }

            //Or, with Factory syntax

            using (var d = DirectoryFactory.New())
            {
                var file = FileFactory.Add(d, fileName);

                Assert.That(File.Exists(file.FullPath));
            }
        }

        #endregion
    }
}