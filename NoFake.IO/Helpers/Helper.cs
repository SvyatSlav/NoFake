using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

[assembly: InternalsVisibleTo("NoFake.IO.Tests")]

namespace NoFake.IO.Helpers
{
    internal static class Helper
    {
        internal static string GetExecutingDiskLetter()
        {
            Contract.Ensures(!String.IsNullOrEmpty(Contract.Result<string>()), "Disk letter is empty");

            var path = Environment.CurrentDirectory;
            var letter = Path.GetPathRoot(path);

            return letter;
        }

        /// <summary>
        /// Convert path to normal view, remove ending-,multiply- and back- slashes
        /// </summary>
        /// <param name="originalPath"></param>
        /// <returns></returns>
        internal static string GetNormalizePath(string originalPath)
        {
            Contract.Requires(!String.IsNullOrEmpty(originalPath));
            Contract.Ensures(Contract.Result<String>() != null);

            var path = originalPath.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

            if (Path.IsPathRooted(path))
            {
                return Path.GetFullPath(new Uri(path).LocalPath);
            }


            if (path.Contains(Path.AltDirectorySeparatorChar)) // '/'
            {
                path = path.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
            }

            if (path.Contains(Path.VolumeSeparatorChar)) // ':'
            {
                path = path.Replace(Path.VolumeSeparatorChar.ToString(), String.Empty);
            }

            string pathSeparator = Path.PathSeparator.ToString();
            if (path.Contains(pathSeparator))
            {
                path = path.Replace(pathSeparator, String.Empty);
            }

            if (path.Contains(Path.DirectorySeparatorChar)) // '\'
            {
                path = Regex.Replace(path, @"\\(?=(\\|[^\w]))", String.Empty);
            }

            return path;
        }

        /// <summary>
        /// Gets the correct format for path.
        /// </summary>
        /// <param name="originalPath">The original path.</param>
        /// <returns></returns>
        internal static string GetFormatedPath(string originalPath)
        {
            Contract.Requires(!String.IsNullOrEmpty(originalPath));
            Contract.Ensures(Contract.Result<String>() != null);

            var path = GetNormalizePath(originalPath);

            if (Path.IsPathRooted(path))
            {
                return path.Replace(Path.GetPathRoot(path), String.Empty);
            }

            return path;
        }


        /// <summary>
        /// Checks Item name for separator character.
        /// </summary>
        /// <param name="itemName">The name.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        internal static void CheckForSeparatorChar(string itemName)
        {
            bool isContaint = false;

            if (itemName.Contains(Path.AltDirectorySeparatorChar)) // '/'
            {
                isContaint = true;
            }

            if (itemName.Contains(Path.VolumeSeparatorChar)) // ':'
            {
                isContaint = true;
            }

            if (itemName.Contains(Path.PathSeparator.ToString())) // ';'
            {
                isContaint = true;
            }

            if (itemName.Contains(Path.DirectorySeparatorChar)) // '\'
            {
                isContaint = true;
            }

            if (isContaint)
            {
                throw new Exception(@"ItemName contains invalid separator chars, like '\',':' and etc");
            }
        }
    }
}