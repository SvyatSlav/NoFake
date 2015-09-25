using System.IO;
using NoFake.IO.Files;
using NUnit.Framework;


namespace NoFake.IO.Tests
{
    public class FileTest
    {
        [Test]
        public void CreateFakeFile_Path_EnsureExist()
        {
            const string fileName = @"1.jpg";

            var directory = new NDirectory();

            using (var file = new EmptyFile(directory, fileName))
            {
                file.Create();
                Assert.True(File.Exists(file.FullPath));
            }

            
        }
    }
}