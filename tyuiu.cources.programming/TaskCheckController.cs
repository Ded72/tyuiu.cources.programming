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
        private readonly string taskNumber;

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

        public static string currentDate = DateTime.Now.ToString().Replace(" ", "-").Replace(":", ".");
        public string[] Load(string csvPath)
        {
            string studentResultFile = @$"{gitController.rootDir}\{currentDate}\CsvReport-Task{taskNumber}.csv";
            string studentResultHrefFile = @$"{gitController.rootDir}\{currentDate}\HrefCsvReport-Task{taskNumber}.csv";
            List<string> csvFileLines = new List<string>();
            if (!Directory.Exists(@$"{gitController.rootDir}\{currentDate}"))
            {
                Directory.CreateDirectory(@$"{gitController.rootDir}\{currentDate}");
            }
            File.Copy(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WorkFiles", "Vedomost.xlsm"), @$"{gitController.rootDir}\{currentDate}\Vedomost-Task-{taskNumber}-{currentDate}.xlsm");
            if (File.Exists(csvPath))
            {
                csvFileLines = ReadCsvFile(csvPath);
                ProcessRepository(csvFileLines, studentResultFile, studentResultHrefFile);
            }
            else if (Directory.Exists(csvPath))
            {
                foreach (string csvFilePath in Directory.GetFiles(csvPath))
                {
                    if (Path.GetExtension(csvFilePath) == ".csv")
                    {
                        csvFileLines = ReadCsvFile(csvFilePath);
                        ProcessRepository(csvFileLines, studentResultFile, studentResultHrefFile);
                    }
                }
            }
            else
            {
                WriteReport(studentResultFile, $"Некорректно указан путь - {csvPath}");
            }
            return new string[] { studentResultFile, currentDate };
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

        public void ProcessRepository(List<string> csvFileLines, string studentResultFile, string studentResutlHrefFile)
        {

            using (StreamWriter sw = new StreamWriter(studentResultFile, false)) { }
            using (StreamWriter sw = new StreamWriter(studentResutlHrefFile, false)) { }
            WriteReport(studentResultFile, "Группа,ФИО,Спринт,Таск,Вариант,Дата сдачи,Дата проверки,Оценка,Статус,Баллы,Бонус,Сумма,Ссылка");
            WriteReport(studentResutlHrefFile, "Группа,ФИО,Спринт,Таск,Вариант,Дата сдачи,Дата проверки,Оценка,Статус,Баллы,Бонус,Сумма,Ссылка");
            List<string> studentPathsToDlls = new List<string>();
            object studentInetrfaceFromDll;
            TaskData taskData = new TaskData();
            foreach (string line in csvFileLines)
            {
                if (!line.Contains("Ответ"))
                {
                    taskData = Parse(line);
                    taskData.Group = GetGroup(taskData.Name, taskData.SurName);
                    taskData.Score = 0.0;
                    if (gitController.Load(taskData.Link, currentDate))
                    {
                        taskData.Score = 0.2;
                        var filename = Path.GetFileNameWithoutExtension(taskData.Link);
                        var localDir = $@"{gitController.rootDir}\{currentDate}\{filename}";
                        List<string> notCompiledDirectories = GetNotCompiledLibDirs(Directory.GetDirectories(localDir));

                        if (notCompiledDirectories.Count > 0)
                        {
                            studentPathsToDlls = Build(localDir);
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
                                                if (studentInetrfaceFromDll != null && directory.Replace(".", "").Contains          (studentInetrfaceFromDll.GetType().GetInterfaces().First().Name.Substring(1)))
                                                {
                                                    if (LaunchFiles(studentInetrfaceFromDll))
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
                                                WriteReport(studentResultFile, taskData.StudentData);
                                                WriteReport(studentResutlHrefFile, taskData.StudentHrefdata);
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        taskData.Score = 0.2;
                                        taskData.Task = Path.GetFileNameWithoutExtension(directory);
                                        taskData.TaskStatus = "НЕКОРРЕКТНОЕ НАЗВАНИЕ ТАСКА";
                                        WriteReport(studentResultFile, taskData.StudentData);
                                        WriteReport(studentResutlHrefFile, taskData.StudentHrefdata);
                                    }

                                }

                            }
                            else
                            {
                                taskData.TaskStatus = "БИБЛИОТЕКА НЕ СКОМПИЛИРОВАЛАСЬ";
                                WriteReport(studentResultFile, taskData.StudentData);
                                WriteReport(studentResutlHrefFile, taskData.StudentHrefdata);
                            }

                        }
                        else
                        {
                            taskData.TaskStatus = "НЕТ БИБЛИОТЕКИ У НУЖНОГО ТАСКА";
                            WriteReport(studentResultFile, taskData.StudentData);
                            WriteReport(studentResutlHrefFile, taskData.StudentHrefdata);
                        }
                    }
                    else
                    {
                        taskData.TaskStatus = "ССЫЛКА НЕВАЛИДНА";
                        WriteReport(studentResultFile, taskData.StudentData);
                        WriteReport(studentResutlHrefFile, taskData.StudentHrefdata);
                    }

                }
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

        public List<string> Build(string directory)
        {
            List<string> dllPaths = new List<string>();
            List<string> subLibDirectories = new List<string>();
            foreach (var subDir in Directory.GetDirectories(directory))
            {
                if (subDir.EndsWith(".Lib"))
                {

                    if (taskNumber != "-" && subDir.Contains($"Task{taskNumber}"))
                    {
                        subLibDirectories.Add(subDir);
                    }
                    else if (taskNumber == "-")
                    {
                        subLibDirectories.Add(subDir);
                    }

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
            try
            {
                dllInterface = assemblyController.CreateInstanceFromFile(studentDllPath);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                //throw new Exception(e.Message);
            };
            return dllInterface;
        }

        public bool LaunchFiles(object interfaceFromDll)
        {
            try
            {
                (var areEquals, var report) = testingController.Run(interfaceFromDll);
                foreach (string line in report)
                {
                    Console.WriteLine(line);
                }
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

                Console.WriteLine(e.Message);
                return false;
            }


        }

        public void WriteReport(string filePath, string data)
        {
            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                sw.WriteLine(data);
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

    public class TaskData
    {
        public string Group = string.Empty;
        public string Name = string.Empty;
        public string SurName = string.Empty;
        public string Date = string.Empty;
        public string Sprint = string.Empty;
        public string Task = string.Empty;
        public string Variant = string.Empty;
        public string Link = string.Empty;
        public string TaskStatus = string.Empty;
        public double Score = 0.0;
        public string StudentData { get { return $"{Group},{SurName} {Name},{Sprint},{Task},{Variant},{Date},{TaskCheckController.currentDate},{TaskStatus},{Score.ToString().Replace(',', '.')},,,{Link}"; } }
        public string StudentHrefdata { get { return @$"{Group},{SurName} {Name},{Sprint},{Task},{Variant},{Date},{TaskCheckController.currentDate},{TaskStatus},{Score.ToString().Replace(',', '.')},,,<a href=""" + Link + @""" target=""_blank"">" + Link + "</a>"; } }
    }
}

