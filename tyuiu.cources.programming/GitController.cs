using LibGit2Sharp;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace tyuiu.cources.programming
{
    public class GitController

    {
        public readonly string rootDir;

        public GitController(string rootDir)
        {
            this.rootDir = rootDir;
        }

        private void deleteFolder(string folder)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(folder);
                DirectoryInfo[] diA = di.GetDirectories();
                FileInfo[] fi = di.GetFiles();
                foreach (FileInfo f in fi)
                {
                    f.Attributes = FileAttributes.Normal;
                    f.Delete();
                }
                foreach (DirectoryInfo df in diA)
                {
                    deleteFolder(df.FullName);
                }
                if (di.GetDirectories().Length == 0 && di.GetFiles().Length == 0) di.Delete();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка: " + ex.Message);
            }
        }
        public bool Load(string repoUrl)
        {
            try
            {
                var filename = Path.GetFileNameWithoutExtension(repoUrl);
                var localDir = $@"{rootDir}\{filename}";

                if (Directory.Exists(localDir))
                {
                    deleteFolder(localDir);
                }
                Repository.Clone(repoUrl, localDir);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


    }


}