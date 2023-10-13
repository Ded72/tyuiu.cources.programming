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
        public bool Load(string repoUrl, string currentDate)
        {
            var filename = Path.GetFileNameWithoutExtension(repoUrl);
            var localDir = $@"{rootDir}\{currentDate}\{filename}";
            try
            {
                Repository.Clone(repoUrl, localDir);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }
            return Directory.Exists(localDir);
        }


    }


}