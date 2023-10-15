using LibGit2Sharp;
using System.Diagnostics;
using System.IO;
using tyuiu.cources.programming.interfaces;
using tyuiu.cources.programming.interfaces.Sprint1;
using Moq;


namespace tyuiu.cources.programming.tests
{


    [TestClass]
    public class TaskCheckControllerTests
    {
        TaskCheckController taskCheckController = new TaskCheckController(
                "-",
                new GitController(@"F:\ServiceWorkFolder"),
                new AssemblyController(),
                new TestingController(new TestingDataController()));

        [TestMethod]
        public void InvalidTestData()
        {
            taskCheckController.taskNumber = "10";
            string items = taskCheckController.LoadLink("");
            string[] results = File.ReadAllLines(items);

            Assert.AreEqual("НЕВАЛИДНЫЕ ДАННЫЕ", results[1]);
        }

        [TestMethod]
        public void InvalidLink()
        {
            taskCheckController.taskNumber = "11";
            string items = taskCheckController.LoadLink("https://github.com/clipboard1/Tyuiu.SimonSTests.Sprint");
            string[] results = File.ReadAllLines(items);
            int indexOfStatusColumn = Array.IndexOf(results[0].Split(","), "Статус");

            Assert.AreEqual("ССЫЛКА НЕВАЛИДНА", results[1].Split(",")[indexOfStatusColumn]);
        }

        [TestMethod]
        public void LibDontExists()
        {
            taskCheckController.taskNumber = "10";
            string items = taskCheckController.LoadLink("https://github.com/clipboard1/Tyuiu.SimonSRTests.Sprint1");
            string[] results = File.ReadAllLines(items);
            int indexOfStatusColumn = Array.IndexOf(results[0].Split(","), "Статус");

            Assert.AreEqual("НЕТ БИБЛИОТЕКИ У НУЖНОГО ТАСКА", results[1].Split(",")[indexOfStatusColumn]);
        }

        [TestMethod]
        public void LibBuildFail()
        {
            taskCheckController.taskNumber = "1";
            string items = taskCheckController.LoadLink("https://github.com/clipboard1/Tyuiu.SimonSRTests.Sprint1");
            string[] results = File.ReadAllLines(items);
            int indexOfStatusColumn = Array.IndexOf(results[0].Split(","), "Статус");

            Assert.AreEqual("БИБЛИОТЕКА НЕ СКОМПИЛИРОВАЛАСЬ", results[1].Split(",")[indexOfStatusColumn]);
        }

        [TestMethod]
        public void InvalidTaskName()
        {
            taskCheckController.taskNumber = "0";
            string items = taskCheckController.LoadLink("https://github.com/clipboard1/Tyuiu.SimonSRTests.Sprint1");
            string[] results = File.ReadAllLines(items);
            int indexOfStatusColumn = Array.IndexOf(results[0].Split(","), "Статус");

            Assert.AreEqual("НЕКОРРЕКТНОЕ НАЗВАНИЕ ТАСКА", results[1].Split(",")[indexOfStatusColumn]);
        }


        [TestMethod]
        public void InterfaceLoadFail()
        {
            taskCheckController.taskNumber = "2";
            string items = taskCheckController.LoadLink("https://github.com/clipboard1/Tyuiu.SimonSRTests.Sprint1");
            string[] results = File.ReadAllLines(items);
            int indexOfStatusColumn = Array.IndexOf(results[0].Split(","), "Статус");

            Assert.AreEqual("ОШИБКА ИНТЕРФЕЙСА", results[1].Split(",")[indexOfStatusColumn]);
        }

        [TestMethod]
        public void InvalidAnswers()
        {
            taskCheckController.taskNumber = "3";
            string items = taskCheckController.LoadLink("https://github.com/clipboard1/Tyuiu.SimonSRTests.Sprint1");
            string[] results = File.ReadAllLines(items);
            int indexOfStatusColumn = Array.IndexOf(results[0].Split(","), "Статус");

            Assert.AreEqual("НЕ СОШЛИСЬ ОТВЕТЫ", results[1].Split(",")[indexOfStatusColumn]);
        }

        [TestMethod]
        public void FullValidLaunch()
        {
            taskCheckController.taskNumber = "4";
            string items = taskCheckController.LoadLink("https://github.com/clipboard1/Tyuiu.SimonSRTests.Sprint1");
            string[] results = File.ReadAllLines(items);
            int indexOfStatusColumn = Array.IndexOf(results[0].Split(","), "Статус");

            Assert.AreEqual("ВСЕ ХОРОШО", results[1].Split(",")[indexOfStatusColumn]);
        }


    }
}


