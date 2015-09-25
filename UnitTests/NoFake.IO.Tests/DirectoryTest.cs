using System.IO;
using System.Threading.Tasks;
using NUnit.Framework;


namespace NoFake.IO.Tests
{
    public class DirectoryTest
    {
        private const string folderName = @"NoFake";

        [Test]
        public void Constructor_Path_DirectoryExist()
        {
            using (var t = new NDirectory(folderName))
            {
                Assert.That(Directory.Exists(t.FullPath), Is.True);
            }
        }

        [Test]
        public void ConstructorTestParallel_Path_DirectoryExist()
        {
            Parallel.For(0, 10, (i) =>
            {
                using (var t = new NDirectory(folderName))
                {
                    Assert.That(Directory.Exists(t.FullPath), Is.True);
                }
            });
        }

        [Test]
        public void EmptyFiles_Dispose_DoesnotThrow()
        {
            var d = new NDirectory(folderName);
            
            Assert.DoesNotThrow(d.Dispose);
        }
       

       
    }
}