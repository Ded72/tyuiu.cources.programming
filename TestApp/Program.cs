using System.Net;
using System.Text;
using tyuiu.cources.programming;

string taskNubmer = "3";

var taskCheckController = new TaskCheckController(
                taskNubmer,
                new GitController(@"F:\ServiceWorkFolder"),
                new AssemblyController(),
                new TestingController(new TestingDataController()));

var tableReportController = new TableContoller(
    taskNubmer,
    new GitController(@"F:\ServiceWorkFolder")
    );

string[] items = taskCheckController.LoadFile(@"C:\Temp\test\Sprint1.Task3.csv");

Console.WriteLine(tableReportController.WriteExcelReport(items));

//Console.WriteLine(tableReportController.MergeTables(@"C:\Temp\TableFiles"));






