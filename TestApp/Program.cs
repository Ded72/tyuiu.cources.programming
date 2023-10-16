using System.Net;
using System.Text;
using tyuiu.cources.programming;

string taskNubmer = "1";

var taskCheckController = new TaskCheckController(
                taskNubmer,
                new GitController(@"C:\ServiceWorkFolder"),
                new AssemblyController(),
                new TestingController(new TestingDataController()));

var tableReportController = new TableContoller(
    taskNubmer,
    new GitController(@"C:\ServiceWorkFolder")
    );

string csvReportPath = taskCheckController.LoadFile(@"C:\Ссылки гит\Sprint2.Task1.csv");

Console.WriteLine(tableReportController.WriteExcelReport(csvReportPath));

//Console.WriteLine(tableReportController.MergeTables(@"C:\TableFiles"));






