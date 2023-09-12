using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tyuiu.cources.programming.interfaces;
using tyuiu.cources.programming.interfaces.Sprint1;
using static tyuiu.cources.programming.ScoreController;

namespace tyuiu.cources.programming
{
    public class CsvController
    {
        private readonly ScoreController scoreController;

        public GitController gitController { get; }

        private readonly AssemblyController assemblyController;
        private readonly TestingController testingController;

        public CsvController(ScoreController scoreController,
            GitController gitController,
            AssemblyController assemblyController,
            TestingController testingController)
        {
            this.scoreController = scoreController;
            this.gitController = gitController;
            this.assemblyController = assemblyController;
            this.testingController = testingController;
        }
        TaskData taskData = new TaskData();
        public List<object> Load(string pathCsvFile)
        {
            using (StreamWriter sw = new StreamWriter(@"C:\Temp\report.txt", false)) { }
            using (StreamReader sr = new StreamReader(pathCsvFile))
            {

                List<string> downloadedLinks = new List<string>();

                List<object> studentInetrfacesFromDll = new List<object>();
                string? line = sr.ReadLine();
                while (line != null)
                {

                    if (!line.Contains("Ответ"))
                    {
                        var parsedData = Parse(line);
                        taskData.Name = parsedData.Name;
                        taskData.SurName = parsedData.SurName;
                        taskData.Link = parsedData.Link;
                        taskData.Score = 0.0;
                        if (!downloadedLinks.Contains(taskData.Link))
                        {
                            if (gitController.Load(taskData.Link))
                            {
                                using (StreamWriter sw = new StreamWriter(@"C:\Temp\report.txt", true))
                                {
                                    sw.WriteLine(($"Ученик {taskData.StudentId}, ссылка валидна"));
                                }
                                taskData.Score = taskData.Score + 0.2;
                                downloadedLinks.Add(taskData.Link);
                                var filename = Path.GetFileNameWithoutExtension(taskData.Link);
                                var localDir = $@"{gitController.rootDir}\{filename}";
                                Build(localDir);
                                //studentInetrfacesFromDll = ExtractInterfaceFromDll(Build(localDir));


                            }

                        }
                    }
                    using (StreamWriter sw = new StreamWriter(@"C:\Temp\report.txt", true))
                    {
                        sw.WriteLine(($"Ученик {taskData.StudentId}, имеет {taskData.Score} баллов"));
                    }
                    line = sr.ReadLine();
                }

                return studentInetrfacesFromDll;
            }


        }

        private static bool RunProcess(string directory)
        {
            Process process = new Process();
            process.StartInfo.FileName = "dotnet";
            process.StartInfo.Arguments = "build";
            process.StartInfo.WorkingDirectory = directory;

            process.Start();
            process.WaitForExit();

            ;

            return process.ExitCode == 0;
        }

        public List<string> Build(string directory)
        {
            List<string> dllPaths = new List<string>();
            List <string> subLibDirectories = new List<string>();
            foreach(var subDir in Directory.GetDirectories(directory))
            {
                if (subDir.EndsWith(".Lib"))
                {
                     subLibDirectories.Add(subDir);
                }
            }
            if (subLibDirectories.Count > 0)
            {
                foreach (string subDirectory in subLibDirectories)
                {
                    if (RunProcess(subDirectory))
                    {
                        scoreController.SetCompileScore(0.2);

                        dllPaths.Add($@"{directory}\{Path.GetFileNameWithoutExtension(subDirectory)}.Lib\bin\Debug\{Path.GetFileNameWithoutExtension(subDirectory)}.Lib.dll");
                        using (StreamWriter sw = new StreamWriter(@"C:\Temp\report.txt", true))
                        {
                            sw.WriteLine(($"Уcпешная компиляция {subDirectory}"));
                        }

                    }
                    else
                    {
                        using (StreamWriter sw = new StreamWriter(@"C:\Temp\report.txt", true))
                        {
                            sw.WriteLine(($"Ошибка компиляции {subDirectory}"));
                        }
                        //throw new Exception($"Compilation error at {subDirectory}");
                    }
                }
                if (dllPaths.Count == subLibDirectories.Count)
                {
                    taskData.Score = taskData.Score + 0.2;
                }
            }
            

            

            return dllPaths;

        }


        public List<object> ExtractInterfaceFromDll(List<string> studentDllPaths)
        {
            List<object> interfacesFromDll = new List<object>();
            foreach (string dll in studentDllPaths)
            {
                if (!dll.Contains('5'))
                {
                    try
                    {
                        var intf = assemblyController.CreateInstanceFromFile(dll);
                        interfacesFromDll.Add(intf);

                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                    };
                }

            }

            return interfacesFromDll;
        }

        private TaskData Parse(string line)
        {
            var values = line.Split(',');

            return new TaskData()
            {
                Name = values[0],
                SurName = values[1],
                Link = values[8]
            };
        }


    }

    public class TaskData
    {
        public string Name = string.Empty;
        public string SurName = string.Empty;
        public string Link = string.Empty;
        public double Score = 0.0;
        public string StudentId { get { return Name + SurName; } }

    }
}
