using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NoFake.IO.Extensions;
using NUnit.Framework;
namespace NoFake.IO.Extensions.Tests
{
    [TestFixture()]
    public class NDirectoryExtensionsTests
    {
        [Test()]
        public void AddWithContent_File_Exist()
        {
            var content = new byte[] {0, 0, 0};

            var d = DirectoryFactory.New();

           var file = d.Add<NFile>("1.txt", content);

            Assert.That(File.Exists(file.FullPath),Is.True);

            var readContent = File.ReadAllBytes(file.FullPath);

            Assert.That(readContent,Is.EqualTo(content));
        }
    }
}
