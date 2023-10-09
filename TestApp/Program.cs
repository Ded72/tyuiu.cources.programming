using System.Net;
using System.Text;
using tyuiu.cources.programming;


var taskCheckController = new TaskCheckController(
                "3",
                new GitController(@"F:\ServiceWorkFolder"),
                new AssemblyController(),
                new TestingController(new TestingDataController()));

var tableReportController = new TableContoller(
    "1",
    new GitController(@"F:\ServiceWorkFolder")
    );

string[] items = taskCheckController.LoadFile(@"C:\Temp\test\Sprint1.Task3.csv");

Console.WriteLine(tableReportController.WriteExcelReport(items));

//Console.WriteLine(tableReportController.MergeTables(@"C:\Temp\TableFiles"));






