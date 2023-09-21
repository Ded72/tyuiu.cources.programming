using tyuiu.cources.programming;


var csvController = new CsvController(
                "1",
                new GitController(@"C:\Temp"),
                new AssemblyController(),
                new TestingController(new TestingDataController()));

string path = csvController.Load(@"C:\Temp\test\links.csv");
Console.WriteLine($"Путь до отчета - {path}");
