using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using tyuiu.cources.programming.interfaces;
using OfficeOpenXml;

namespace tyuiu.cources.programming
{
    
    public class TaskCheckController
    {

        public GitController gitController { get; }

        private readonly AssemblyController assemblyController;
        private readonly TestingController testingController;
        private string taskNumber;
        public string TaskNumber
        {
            get { return taskNumber; }
            set { taskNumber = value; }
        }

        public TaskCheckController(
            string taskNumber,
            GitController gitController,
            AssemblyController assemblyController,
            TestingController testingController)
        {
            this.taskNumber = taskNumber;
            this.gitController = gitController;
            this.assemblyController = assemblyController;
            this.testingController = testingController;
        }

        

        public string LoadLink(string link)
        {
            FileController fileController = new FileController(gitController.rootDir, taskNumber);
            string resultFilePath = ProcessRepositoryFromLink(link, fileController);
            return resultFilePath;
        }
        public string LoadFile(string csvPath)
        {
            FileController fileController = new FileController(gitController.rootDir, taskNumber);
            List<string> csvFileLines = new List<string>();
            if (File.Exists(csvPath))
            {
                csvFileLines = ReadCsvFile(csvPath);
                ProcessRepositoryFromFile(csvFileLines, fileController);
            }
            else if (Directory.Exists(csvPath))
            {
                foreach (string csvFilePath in Directory.GetFiles(csvPath))
                {
                    if (Path.GetExtension(csvFilePath) == ".csv")
                    {
                        csvFileLines = ReadCsvFile(csvFilePath);
                        ProcessRepositoryFromFile(csvFileLines, fileController);
                    }
                }
            }
            else
            {
                fileController.WriteReport($"Некорректно указан путь - {csvPath}");
            }
            return fileController.StudentResultfilePath;
        }

        public List<string> ReadCsvFile(string pathCsvFile)
        {
            List<string> lines = new List<string>(File.ReadAllLines(pathCsvFile));
            List<string> correctedLines = new List<string>(DeleteDuplitaces(lines));
            return correctedLines;

        }

        public bool CheckLink(string url)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = client.GetAsync(url).GetAwaiter().GetResult();
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
                return false;
            }
        }
        public List<string> DeleteDuplitaces(List<string> lines)
        {
            List<string> correctedLines = new List<string>();
            Dictionary<string, string> dataDictionary = new Dictionary<string, string>();
            using (HttpClient client = new HttpClient())
            {
                foreach (string line in lines)
                {
                    string[] values = line.Split(',');

                    if (values.Length >= 2)
                    {
                        string key = values[0] + "," + values[1];
                        if (!dataDictionary.ContainsKey(key))
                        {
                            dataDictionary[key] = line;
                        }
                        else if (dataDictionary.ContainsKey(key) && dataDictionary[key].Split(',')[dataDictionary[key].Split(',').Length - 1] != values[values.Length - 1] && CheckLink(values[values.Length - 1]))
                        {
                            dataDictionary[key] = line;
                        }
                    }

                }
            }
            foreach (var line in dataDictionary.Values)
            {
                correctedLines.Add(line);
            }
            return correctedLines;
        }

        public string ProcessRepositoryFromLink(string repoLink, FileController fileController)
        {
            fileController.WriteMarkupForLinkChecking();
            ProcessRepository(fileController, repoLink);
            return fileController.StudentResultfilePath;
        }

        public string ProcessRepositoryFromFile(List<string> csvFileLines, FileController fileController)
        {
            fileController.WriteMarkupForFileChecking();
            foreach (string line in csvFileLines)
            {
                if (!line.Contains("Ответ"))
                {
                    ProcessRepository(fileController, line);
                }
            }
            return fileController.StudentResultfilePath;
        }
        public void ProcessRepository(FileController fileController, string item)
        {
            List<string> studentPathsToDlls = new List<string>();
            object studentInetrfaceFromDll;
            TaskData taskData = new TaskData();
            taskData.itemIsLink = (Uri.TryCreate(item, UriKind.Absolute, out Uri uriResult));
            if (item == "")
            {
                fileController.WriteReport("НЕВАЛИДНЫЕ ДАННЫЕ");
                return;
            }
            if (taskData.itemIsLink)
            {
                taskData.Link = item;
                taskData.Score = 0.0;
            }
            else
            {
                taskData = Parse(item);
                taskData.Group = GetGroup(taskData.Name, taskData.SurName);
                taskData.Score = 0.0;
            }
            taskData.CheckingDate = fileController.CurrentDate;
            taskData.Task = Path.GetFileName(fileController.StudentResultfilePath);
            if (CheckLink(taskData.Link) && gitController.Load(taskData.Link, fileController.CurrentDate))
            {
                taskData.Score = 0.2;
                var filename = Path.GetFileNameWithoutExtension(taskData.Link);
                var localDir = $@"{gitController.rootDir}\{fileController.CurrentDate}\{filename}";
                List<string> notCompiledDirectories = GetNotCompiledLibDirs(Directory.GetDirectories(localDir));
                if (notCompiledDirectories.Count > 0)
                {
                    studentPathsToDlls = Build(notCompiledDirectories);
                    if (studentPathsToDlls.Count > 0)
                    {
                        Regex rightTaskNumberRegex = new Regex(@"Sprint\d{1,2}.Task\d{1,2}.V\d{1,2}\b");
                        foreach (string directory in notCompiledDirectories)
                        {
                            Match match = rightTaskNumberRegex.Match(directory);
                            if (match.Value != "")
                            {
                                taskData.Sprint = match.Value.Split('.')[0];
                                taskData.Task = match.Value.Split('.')[1];
                                taskData.Variant = match.Value.Split('.')[2];
                                foreach (string path in studentPathsToDlls)
                                {
                                    if (path.Contains(directory))
                                    {
                                        taskData.Score = 0.4;
                                        studentInetrfaceFromDll = ExtractInterfaceFromDll(path);
                                        if (studentInetrfaceFromDll != null && directory.Replace(".", "").Contains(studentInetrfaceFromDll.GetType().GetInterfaces().First().Name.Substring(1)))
                                        {
                                            (bool successLaunch, IEnumerable<String> lines) = LaunchMethodFromDll(studentInetrfaceFromDll);
                                            printResultsOnScreen(lines);
                                            if (successLaunch)
                                            {                                        
                                                taskData.Score = 0.6;
                                                taskData.TaskStatus = "ВСЕ ХОРОШО";
                                            }
                                            else
                                            {
                                                taskData.TaskStatus = "НЕ СОШЛИСЬ ОТВЕТЫ";
                                            }
                                        }
                                        else
                                        {
                                            taskData.TaskStatus = "ОШИБКА ИНТЕРФЕЙСА";
                                        }
                                        fileController.WriteReport(taskData.GetStudentData);
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                taskData.Score = 0.2;
                                taskData.Task = Path.GetFileNameWithoutExtension(directory);
                                taskData.TaskStatus = "НЕКОРРЕКТНОЕ НАЗВАНИЕ ТАСКА";
                                fileController.WriteReport(taskData.GetStudentData);
                            }

                        }

                    }
                    else
                    {
                        taskData.TaskStatus = "БИБЛИОТЕКА НЕ СКОМПИЛИРОВАЛАСЬ";
                        fileController.WriteReport(taskData.GetStudentData);
                    }

                }
                else
                {
                    taskData.TaskStatus = "НЕТ БИБЛИОТЕКИ У НУЖНОГО ТАСКА";
                    fileController.WriteReport(taskData.GetStudentData);
                }
            }
            else
            {
                taskData.TaskStatus = "ССЫЛКА НЕВАЛИДНА";
                fileController.WriteReport(taskData.GetStudentData);
            }
        }

        public List<string> GetNotCompiledLibDirs(string[] directory)
        {
            List<string> notCompoiledLibDirs = new List<string>();
            foreach (string dir in directory)
            {
                if (dir.EndsWith(".Lib"))
                {
                    if (taskNumber != "-" && dir.Contains($"Task{taskNumber}"))
                    {
                        notCompoiledLibDirs.Add(dir);
                    }
                    else if (taskNumber == "-")
                    {
                        notCompoiledLibDirs.Add(dir);
                    }

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

        public List<string> Build(List <string> notCompiledLibDirs)
        {
            List<string> dllPaths = new List<string>();
                foreach (string notCompiledLibDir in notCompiledLibDirs)
                {
                    if (RunProcess(notCompiledLibDir))
                    {
                        
                        dllPaths.Add($@"{notCompiledLibDir}\bin\Debug\{Path.GetFileNameWithoutExtension(notCompiledLibDir)}.Lib.dll");
                    }
                }
            return dllPaths;
        }

        public object ExtractInterfaceFromDll(string studentDllPath)
        {
            object dllInterface = null;
            try
            {
                dllInterface = assemblyController.CreateInstanceFromFile(studentDllPath);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            };
            return dllInterface;
        }

        public (bool IsSuccess, IEnumerable<String> lines) LaunchMethodFromDll(object interfaceFromDll)
        {
            try
            {
                return testingController.Run(interfaceFromDll);
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return (false, lines: new List<string> { });
            }
        }

        public void printResultsOnScreen(IEnumerable<String> results)
        {
            foreach (string resultItem in results)
            {
                Console.WriteLine(resultItem);
            }
        }

        

        public string GetGroup(string name, string surName)
        {
            string group = "Группа";

            List<string> lines = new List<string>(File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WorkFiles", "GroupsList.csv")));
            foreach (string line in lines)
            {
                string[] values = line.Split(',');
                if (surName == values[1] && name == values[2])
                {
                    group = values[0];
                    break;
                }
            }
            return group;
        }
        private TaskData Parse(string line)
        {
            var values = line.Split(',');
            try
            {
                DateTime date = DateTime.ParseExact(values[4].Replace("\"", ""), "d MMMM yyyy  HH:mm", CultureInfo.InvariantCulture);
                if (values.Length < 8)
                {
                    return new TaskData()
                    {
                        SurName = values[0],
                        Name = values[1],
                        Date = date.ToString("dd.MM.yyyy HH:mm"),
                        Sprint = String.Empty,
                        Task = String.Empty,
                        Variant = String.Empty,
                        Link = values[values.Length - 1]
                    };
                }
                else
                {
                    return new TaskData()
                    {
                        SurName = values[0],
                        Name = values[1],
                        Date = date.ToString("dd.MM.yyyy HH:mm"),
                        Task = values[values.Length - 2],
                        Link = values[values.Length - 1]
                    };
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                if (values.Length < 8)
                {
                    return new TaskData()
                    {
                        SurName = values[0],
                        Name = values[1],
                        Date = String.Empty,
                        Task = String.Empty,
                        Link = values[values.Length - 1]
                    };
                }
                else
                {
                    return new TaskData()
                    {
                        SurName = values[0],
                        Name = values[1],
                        Date = String.Empty,
                        Task = values[values.Length - 2],
                        Link = values[values.Length - 1]
                    };
                }

            }
        }
    }

    public class FileController
    {
        private readonly string currentDate = DateTime.Now.ToString().Replace(" ", "-").Replace(":", ".");
        private string studentResultfilePath;
        private string rootDir;
        private string taskNumber;
        public FileController(string rootDir, string taskNumber)
        {
            this.rootDir = rootDir;
            this.taskNumber = taskNumber;
            InitializestudentResultfilePath();
        }
        public string StudentResultfilePath
        {
            get { return studentResultfilePath; }
        }
        public string CurrentDate
        {
            get { return currentDate; }
        }
        public void InitializestudentResultfilePath()
        {
            if (!Directory.Exists(@$"{rootDir}\{currentDate}"))
            {
                Directory.CreateDirectory(@$"{rootDir}\{currentDate}");
            }
            studentResultfilePath = @$"{rootDir}\{currentDate}\CsvReport-Task{taskNumber}.csv";
            using (StreamWriter sw = new StreamWriter(studentResultfilePath, false)) { };
        }
        public void WriteMarkupForFileChecking()
        {
            WriteReport("Группа,ФИО,Спринт,Таск,Вариант,Дата сдачи,Дата проверки,Статус,Баллы,Бонус,Сумма,Ссылка");
        }
        public void WriteMarkupForLinkChecking()
        {
            WriteReport("Задание,Статус,Ссылка");
        }

        public void WriteReport (string data)
        {
            using (StreamWriter sw = new StreamWriter(studentResultfilePath, true))
            {
                sw.WriteLine(data);
            }
        }
    }
    public class TaskData
    {
        public bool itemIsLink;
        public string Group = string.Empty;
        public string Name = string.Empty;
        public string SurName = string.Empty;
        public string Date = string.Empty;
        public string CheckingDate = string.Empty;
        public string TaskStatus = string.Empty;
        public string Sprint = string.Empty;
        public string Task = string.Empty;
        public string Variant = string.Empty;
        public string Link = string.Empty;
        public double Score = 0.0;
        public string GetStudentData { get { if (itemIsLink) { { return $"{Task},{TaskStatus},{Link}"; } } else { return $"{Group},{SurName} {Name},{Sprint},{Task},{Variant},{Date},{CheckingDate},{TaskStatus},{Score.ToString().Replace(',', '.')},,,{Link}"; } } }
    }
}

