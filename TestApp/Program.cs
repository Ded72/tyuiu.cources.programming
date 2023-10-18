using NLog;
using System.Net;
using System.Text;
using tyuiu.cources.programming;

string taskNubmer = "5";
string fileName = "Sprint2.Task5";

GlobalDiagnosticsContext.Set("logFileName", $"{fileName}_" + DateTime.Now.ToString("g"));


Logger Logger = LogManager.GetCurrentClassLogger();


var taskCheckController = new TaskCheckController(
                taskNubmer,
                new GitController(@"C:\ServiceWorkFolder"),
                new AssemblyController(),
                new TestingController(new TestingDataController()));

var tableReportController = new TableContoller(
    taskNubmer,
    new GitController(@"C:\ServiceWorkFolder")
    );

string csvReportPath = taskCheckController.LoadFile(@$"C:\Temp\test\{fileName}.csv");
Logger.Info(tableReportController.WriteExcelReport(csvReportPath));

//Logger.Info(tableReportController.MergeTables(@"C:\TableFiles"));






