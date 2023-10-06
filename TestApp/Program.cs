using tyuiu.cources.programming;


var csvController = new CsvController(
                "1",
                new GitController(@"F:\ServiceWorkFolder"),
                new AssemblyController(),
                new TestingController(new TestingDataController()));

var tableReportController = new TableContoller(
    "1",
    new GitController(@"F:\ServiceWorkFolder")
    );

string[] items = csvController.Load(@"D:\Downloads\C# 1 Курс 2023-#1.1 Выслать ссылку с GitHub-ответы.csv");

Console.WriteLine(tableReportController.WriteExcelReport(items));


//Console.WriteLine(tableReportController.MergeTables(@"C:\Temp\TableFiles"));

//Test dev branch commit