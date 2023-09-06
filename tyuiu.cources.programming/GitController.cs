using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace tyuiu.cources.programming
{
    public class GitController
    {
        public void Load(string repoUrl, string rootDir)
        {
            if ((repoUrl.IndexOf("Tyuiu")) < 0)
            {
                throw new FileLoadException($"{repoUrl} - bad link");
            }
            var filename = (repoUrl.Substring(repoUrl.IndexOf("Tyuiu"))).Replace(".git", "");
            var localDir = $@"{rootDir}\{filename}";
            if (Directory.Exists(localDir))
            {
                throw new FileLoadException($"Directory {localDir} already exists!");
            }
            
            Repository.Clone(repoUrl, localDir);
            //Console.WriteLine($"Copying {repoUrl}");
            

        }

        }

        
    }