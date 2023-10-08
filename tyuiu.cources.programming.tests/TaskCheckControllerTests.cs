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
        [TestMethod]
        public void InvalidTestData()
        {
            var taskCheckController = new TaskCheckController(
                "0",
                new GitController(@"F:\ServiceWorkFolder"),
                new AssemblyController(),
                new TestingController(new TestingDataController()));
            string[] items = taskCheckController.LoadLink("");
            string[] results = File.ReadAllLines(items[0]);

            Assert.AreEqual("НЕВАЛИДНЫЕ ДАННЫЕ", results[0]);
        }

        [TestMethod]
        public void InvalidLink()
        {
            var taskCheckController = new TaskCheckController(
               "0",
               new GitController(@"F:\ServiceWorkFolder"),
               new AssemblyController(),
               new TestingController(new TestingDataController()));
            string[] items = taskCheckController.LoadLink("https://github.com/clipboard1/Tyuiu.SimonSRTests.Sprint");

            string[] results = File.ReadAllLines(items[0]);

            Assert.AreEqual("ССЫЛКА НЕВАЛИДНА", results[1].Split(",")[1]);
        }

        [TestMethod]
        public void LibDontExists()
        {
            var taskCheckController = new TaskCheckController(
               "10",
               new GitController(@"F:\ServiceWorkFolder"),
               new AssemblyController(),
               new TestingController(new TestingDataController()));
            string[] items = taskCheckController.LoadLink("https://github.com/clipboard1/Tyuiu.SimonSRTests.Sprint1");
            string[] results = File.ReadAllLines(items[0]);

            Assert.AreEqual("НЕТ БИБЛИОТЕКИ У НУЖНОГО ТАСКА", results[1].Split(",")[1]);
        }

        [TestMethod]
        public void LibBuildFail()
        {
            var taskCheckController = new TaskCheckController(
                "1",
                new GitController(@"F:\ServiceWorkFolder"),
                new AssemblyController(),
                new TestingController(new TestingDataController()));
            string[] items = taskCheckController.LoadLink("https://github.com/clipboard1/Tyuiu.SimonSRTests.Sprint1");
            string[] results = File.ReadAllLines(items[0]);

            Assert.AreEqual("БИБЛИОТЕКА НЕ СКОМПИЛИРОВАЛАСЬ", results[1].Split(",")[1]);
        }

        [TestMethod]
        public void InvalidTaskName()
        {
            var taskCheckController = new TaskCheckController(
                "0",
                new GitController(@"F:\ServiceWorkFolder"),
                new AssemblyController(),
                new TestingController(new TestingDataController()));
            string[] items = taskCheckController.LoadLink("https://github.com/clipboard1/Tyuiu.SimonSRTests.Sprint1");
            string[] results = File.ReadAllLines(items[0]);

            Assert.AreEqual("НЕКОРРЕКТНОЕ НАЗВАНИЕ ТАСКА", results[1].Split(",")[1]);
        }


        [TestMethod]
        public void InterfaceLoadFail()
        {
            var taskCheckController = new TaskCheckController(
                "2",
                new GitController(@"F:\ServiceWorkFolder"),
                new AssemblyController(),
                new TestingController(new TestingDataController()));
            string[] items = taskCheckController.LoadLink("https://github.com/clipboard1/Tyuiu.SimonSRTests.Sprint1");
            string[] results = File.ReadAllLines(items[0]);

            Assert.AreEqual("ОШИБКА ИНТЕРФЕЙСА", results[1].Split(",")[1]);
        }

        [TestMethod]
        public void InvalidAnswers()
        {
            var taskCheckController = new TaskCheckController(
                "3",
                new GitController(@"F:\ServiceWorkFolder"),
                new AssemblyController(),
                new TestingController(new TestingDataController()));
            string[] items = taskCheckController.LoadLink("https://github.com/clipboard1/Tyuiu.SimonSRTests.Sprint1");
            string[] results = File.ReadAllLines(items[0]);

            Assert.AreEqual("НЕ СОШЛИСЬ ОТВЕТЫ", results[1].Split(",")[1]);
        }

        [TestMethod]
        public void FullValidLaunch()
        {
            var taskCheckController = new TaskCheckController(
                "4",
                new GitController(@"F:\ServiceWorkFolder"),
                new AssemblyController(),
                new TestingController(new TestingDataController()));
            string[] items = taskCheckController.LoadLink("https://github.com/clipboard1/Tyuiu.SimonSRTests.Sprint1");
            string[] results = File.ReadAllLines(items[0]);

            Assert.AreEqual("ВСЕ ХОРОШО", results[1].Split(",")[1]);
        }


    }
}


