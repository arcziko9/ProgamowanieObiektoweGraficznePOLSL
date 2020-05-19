using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniTotalCommander.Model.Commands
{
 internal class Copy : IOperation
    {
        public string Name { get; } = "Copy";

        public void Execute(string sourcePath, string destinationPath)
        {
            if (File.Exists(sourcePath))
            {
                var info = new FileInfo(sourcePath);

                File.Copy(sourcePath, destinationPath + Path.DirectorySeparatorChar + info.Name);
            }
            if (Directory.Exists(sourcePath))
            {
                var info = new DirectoryInfo(sourcePath);
                DirectoryCopy(sourcePath, destinationPath + Path.DirectorySeparatorChar + info.Name, true);
            }
        }

        public bool canExecute(string sourcePath, string destinationPath)
        {
            string sourceDir = sourcePath.Substring(0, sourcePath.LastIndexOf(Path.DirectorySeparatorChar));
            string sourceFile = sourcePath.Substring(sourcePath.LastIndexOf(Path.DirectorySeparatorChar));
            if (sourceDir == destinationPath) return false;
            return true;
        }

        private static void DirectoryCopy(string sourceDirectoryName, string destinationDirectoryName, bool copySubdirectories)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDirectoryName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirectoryName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            if (!Directory.Exists(destinationDirectoryName))
            {
                Directory.CreateDirectory(destinationDirectoryName);
            }

            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destinationDirectoryName, file.Name);
                file.CopyTo(temppath, false);
            }

            if (copySubdirectories)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destinationDirectoryName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubdirectories);
                }
            }
        }
    }
}
