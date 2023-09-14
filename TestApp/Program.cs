using tyuiu.cources.programming;


var csvController = new CsvController(
                new GitController(@"C:\Temp"),
                new AssemblyController(),
                new TestingController(new TestingDataController()));

string path = csvController.Load(@"C:\Temp\0_7_Vyslat_ssylku_s_GitHub-otvety.csv");
Console.WriteLine($"Путь до отчета - {path}");
