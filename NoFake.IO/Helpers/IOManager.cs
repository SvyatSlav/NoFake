using System;
using System.IO;
using System.Threading;

namespace NoFake.IO.Helpers
{
    internal static class IOManager
    {
        private const int MaxIteration = 4;

        public static void DeleteFile(string fullPath)
        {
            var canExit = false;

            var iteration = 1;

            while (!canExit)
            {
                try
                {
                    File.Delete(fullPath);

                    canExit = true;
                }
                catch (Exception)
                {
                    if (iteration == MaxIteration)
                    {
                        throw;
                    }

                    //Sleep 1 sec, then 2 sec and so on...
                    Thread.Sleep(iteration*1000);
                    ++iteration;
                }
            }
        }

        public static void DeleteDirectory(string fullPath, bool recursive = false)
        {
            var canExit = false;

            var iteration = 1;

            while (!canExit)
            {
                try
                {
                    Directory.Delete(fullPath, recursive);

                    canExit = true;
                }
                catch (Exception)
                {
                    if (iteration == MaxIteration)
                    {
                        throw;
                    }

                    //Sleep 1 sec, then 2 sec and so on...
                    Thread.Sleep(iteration*1000);
                    ++iteration;
                }
            }
        }

        public static void CreateEmptyFile(string fullPath)
        {
            using (File.Create(fullPath))
            {
            }
        }

        public static void CreateFileWithContent(string fullPath, byte[] content)
        {
            File.WriteAllBytes(fullPath, content);
        }
    }
}