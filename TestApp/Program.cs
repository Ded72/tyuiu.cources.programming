using System.Net;
using System.Text;
using tyuiu.cources.programming;

string taskNubmer = "2";

var taskCheckController = new TaskCheckController(
                taskNubmer,
                new GitController(@"F:\ServiceWorkFolder"),
                new AssemblyController(),
                new TestingController(new TestingDataController()));

var tableReportController = new TableContoller(
    taskNubmer,
    new GitController(@"F:\ServiceWorkFolder")
    );

string csvReportPath = taskCheckController.LoadFile(@"C:\Temp\test\Sprint1.Task2.csv");

Console.WriteLine(tableReportController.WriteExcelReport(csvReportPath));

//Console.WriteLine(tableReportController.MergeTables(@"C:\Temp\TableFiles"));






