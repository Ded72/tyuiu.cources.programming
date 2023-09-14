using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using tyuiu.cources.programming.interfaces;


namespace tyuiu.cources.programming
{
    public class CsvController
    {

        public GitController gitController { get; }

        private readonly AssemblyController assemblyController;
        private readonly TestingController testingController;

        public CsvController(
            GitController gitController,
            AssemblyController assemblyController,
            TestingController testingController)
        {
            this.gitController = gitController;
            this.assemblyController = assemblyController;
            this.testingController = testingController;
        }
 
        public string Load(string pathCsvFile)
        {
            string currentDate = DateTime.Now.ToString().Replace(" ", "-").Replace(":", ".");
            List<string> studentPathsToDlls = new List<string>();
            object studentInetrfaceFromDll;
            TaskData taskData = new TaskData();
            if (!Directory.Exists(@$"{gitController.rootDir}\{currentDate}"))
            {
                Directory.CreateDirectory(@$"{gitController.rootDir}\{currentDate}");
            }
            string studentResultFile = @$"{gitController.rootDir}\{currentDate}\educon.txt";
            using (StreamWriter sw = new StreamWriter(studentResultFile, false)) { }
            using (StreamReader sr = new StreamReader(pathCsvFile))
            {

                List<string> downloadedLinks = new List<string>();
                string? line = sr.ReadLine();
                while (line != null)
                {
                    if (!line.Contains("Ответ"))
                    {
                        var parsedData = Parse(line);
                        taskData.Name = parsedData.Name;
                        taskData.SurName = parsedData.SurName;
                        taskData.Date = parsedData.Date;
                        taskData.Task = parsedData.Task;
                        taskData.Link = parsedData.Link;
                        taskData.Score = 0.0;
                        if (!downloadedLinks.Contains(taskData.Link))
                        {
                            downloadedLinks.Add(taskData.Link);
                            if (gitController.Load(taskData.Link, currentDate))
                            {
                                taskData.Score = 0.2;
                                var filename = Path.GetFileNameWithoutExtension(taskData.Link);
                                var localDir = $@"{gitController.rootDir}\{currentDate}\{filename}";
                                List<string> notCompiledDirectories = GetNotCompiledLibDirs(Directory.GetDirectories(localDir));

                                if (notCompiledDirectories.Count > 0)
                                {
                                    studentPathsToDlls = Build(localDir);
                                    bool found = false;
                                    Regex regex = new Regex(@"Sprint\d.Task\d\b");
                                    foreach (string directory in notCompiledDirectories)
                                    {
                                        Match match = regex.Match(directory);
                                        taskData.Task = match.Value;
                                        foreach (string path in studentPathsToDlls)
                                        {
                                            if (path.Contains(directory) && taskData.Task != "")
                                            {

                                                found = true;
                                                taskData.Score = 0.4;
                                                studentInetrfaceFromDll = ExtractInterfaceFromDll(path);
                                                if (studentInetrfaceFromDll != null)
                                                {
                                                    if (LaunchFiles(studentInetrfaceFromDll))
                                                    {
                                                        taskData.Score = 0.6;
                                                    }
                                                    ;
                                                }
                                                WriteReport(studentResultFile, taskData.StudentData);
                                                break;
                                            }
                                        }
                                        if (!found)
                                        {
                                            taskData.Score = 0.2;
                                            WriteReport(studentResultFile, taskData.StudentData);
                                            found = false;
                                        }
                                    } 
                                }
                                else
                                {
                                    WriteReport(studentResultFile, taskData.StudentData);
                                }    
                            }
                            else
                            {
                                WriteReport(studentResultFile, taskData.StudentData);
                            }
                        }
                    }
                    line = sr.ReadLine();
                }
                return @$"{gitController.rootDir}\{currentDate}\educon.txt";
            }
        }

        public List <string> GetNotCompiledLibDirs(string[] directory)
        {
            List <string> notCompoiledLibDirs = new List <string>();
            foreach (string dir in directory)
            {
                if (dir.EndsWith(".Lib"))
                {
                    notCompoiledLibDirs.Add(dir);
                }
            }
            return notCompoiledLibDirs;
        }

        private static bool RunProcess(string directory)
        {
            Process process = new Process();
            process.StartInfo.FileName = "dotnet";
            process.StartInfo.Arguments = "build";
            process.StartInfo.WorkingDirectory = directory;

            process.Start();
            process.WaitForExit();

            return process.ExitCode == 0;
        }

        public List<string> Build(string directory)
        {
            List<string> dllPaths = new List<string>();
            List<string> subLibDirectories = new List<string>();
            foreach (var subDir in Directory.GetDirectories(directory))
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

                        dllPaths.Add($@"{directory}\{Path.GetFileNameWithoutExtension(subDirectory)}.Lib\bin\Debug\{Path.GetFileNameWithoutExtension(subDirectory)}.Lib.dll");
                    }
                }
            }
            return dllPaths;

        }

        public object ExtractInterfaceFromDll(string studentDllPath)
        {
            object? dllInterface = null;

            if (!studentDllPath.Contains("Task5"))
            {
                try
                {
                    dllInterface = assemblyController.CreateInstanceFromFile(studentDllPath);

                }
                catch (Exception e)
                {
                    //throw new Exception(e.Message);
                };
            }
            return dllInterface;
        }

        public bool LaunchFiles(object interfaceFromDll)
        {

            try
            {
                (var areEquals, var report) = testingController.Run(interfaceFromDll);
                if (areEquals)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }


        }

        public void WriteReport(string filePath, string data)
        {
            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                sw.WriteLine(data);
            }
        }

        private TaskData Parse(string line)
        {
            var values = line.Split(',');
            DateTime date = DateTime.ParseExact(values[4].Replace("\"", ""), "d MMMM yyyy  HH:mm", CultureInfo.InvariantCulture);
            return new TaskData()
            {
                Name = values[0],
                SurName = values[1],
                Date = date.ToString("dd.MM.yyyy HH:mm"),
                Task = values[7].Replace("\"", ""),
                Link = values[8]
            };
        }


    }

    public class TaskData
    {
        public string Name = string.Empty;
        public string SurName = string.Empty;
        public string Date = string.Empty;
        public string Task = string.Empty;
        public string Link = string.Empty;
        public double Score = 0.0;
        public string StudentData { get { return $"Группа 1,{Name} {SurName},{Task},{Date},{Score.ToString().Replace(',', '.')}"; } }

    }
}
