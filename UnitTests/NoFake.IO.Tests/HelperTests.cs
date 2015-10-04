using System;
using NoFake.IO.Helpers;
using NUnit.Framework;

namespace NoFake.IO.Tests
{
    public class HelperTests
    {
        [Test]
        public void ExecutingDiskLetter_RightLetter()
        {
            //TODO User temp directory
            var path = Helper.GetExecutingDiskLetter();
            Assert.That(path, Is.EqualTo(@"C:\"));
            Assert.IsNotEmpty(path);
        }

        [TestCase(@"C:\Folder\\\InnerFolder\\", @"C:\Folder\InnerFolder")]
        [TestCase(@"C:\Folder\\\InnerFolder", @"C:\Folder\InnerFolder")]
        [TestCase(@"Folder\InnerFolder\", @"Folder\InnerFolder")]
        [TestCase(@"Folder\\\\InnerFolder\\\\\\", @"Folder\InnerFolder")]
        [TestCase(@"Folder////InnerFolder////", @"Folder\InnerFolder")]
        [TestCase(@"Folder\//\\InnerFolder\//\\\", @"Folder\InnerFolder")]
        [TestCase(@"Folder\\\InnerFolder\1.jpg", @"Folder\InnerFolder\1.jpg")]
        public void GetNormalizePath(string originalPath, string expectedPath)
        {
            Assert.AreEqual(expectedPath, Helper.GetNormalizePath(originalPath));
        }


        public static void GcCollect()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

    }
}