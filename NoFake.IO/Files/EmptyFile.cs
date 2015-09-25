using System.Runtime.CompilerServices;
using NoFake.IO.Helpers;


[assembly: InternalsVisibleTo("NoFake.IO.Tests")]

namespace NoFake.IO.Files
{
    internal class EmptyFile : NFile
    {
        internal EmptyFile(NDirectory directory, string fileName) : base(directory, fileName)
        {
        }


        public override void Create()
        {
            IOManager.CreateEmptyFile(FullPath);
        }
    }
}