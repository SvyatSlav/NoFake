using System;
using System.IO;
using NoFake.IO.Tests.FolderFileDisposing;

namespace NoFake.IO.Tests
{
    public static class TestHelper
    {
        public static byte[] ReadBytes(string filePath)
        {
            if (filePath == null) throw new ArgumentNullException("filePath");

            return File.ReadAllBytes(filePath);
        }

        /// <summary>
        /// Аналог ?. из C#5 -- если объект не null, то выполняем лямбду из селектора. Если null то вернём default
        /// </summary>
        /// <typeparam name="T">Class T</typeparam>
        /// <typeparam name="U">Лябда</typeparam>
        /// <param name="callSite">The call site.</param>
        /// <param name="selector">The selector.</param>
        /// <returns></returns>
        public static U With<T, U>(this T callSite, Func<T, U> selector) where T : class
        {
            if (selector == null) throw new ArgumentNullException("selector");

            if (callSite == null)
            {
                return default(U);
            }
            
            return selector(callSite);
        }

        public static string GetCheckSumRoot(SimpleTransfer transfer, string transferName)
        {
            //Корень
            var root = transfer.RootPath;
       var checkSumsTransferName = transferName + "_checksums";

            //Корень к параллельной структуре
            var checkSumRoot = Path.Combine(root, checkSumsTransferName);
            return checkSumRoot;
        }
    }
}