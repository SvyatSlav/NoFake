using System.IO;
using System.Linq;
using NUnit.Framework;

namespace NoFake.IO.Tests.FolderFileDisposing
{
    public class AddFileToFolderTests
    {
        private SimpleTransfer _transfer;

        [SetUp]
        public virtual void SetUp()
        {
            _transfer = new SimpleTransfer("Transfer", 2);
        }

        [Test]
        public void Add_6File_Exist()
        {
            HelperTests.GcCollect();

            var files = Directory.GetFiles(_transfer.TranferPath, "*", SearchOption.AllDirectories);

            Assert.That(files.Count(), Is.EqualTo(6));
        }

        //TODO AddSomeGCCollectSpecTest

        [Test]
        public void Add_Dispose_NoneFolderFile()
        {
            var rootPath = _transfer.RootPath;

            _transfer.Dispose();

            HelperTests.GcCollect();

            Assert.That(Directory.Exists(rootPath), Is.False);
        }
    }
}