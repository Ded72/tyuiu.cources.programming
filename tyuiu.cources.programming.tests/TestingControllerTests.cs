using Moq;
using tyuiu.cources.programming.interfaces;
using tyuiu.cources.programming.interfaces.Sprint1;
using tyuiu.cources.programming.interfaces.Sprint2;

using Tyuiu.SimonSRTests.Sprint1.Task0.V10.Lib;

namespace tyuiu.cources.programming.tests
{
    [TestClass]
    public class TestingControllerTests
    {
        
        private readonly TestingController testingController =
            new TestingController( new TestingDataController());
        private readonly AssemblyController assemblyController = new AssemblyController();
        private readonly string filename = typeof(DataService).Assembly.Location;


        [TestMethod]
        public void RunValid()
        {
            Mock<ISprint1Task3V17> m = new Mock<ISprint1Task3V17>();
            m.Setup(f => f.ZeroCheck(150.150)).Returns(true);
            var res = testingController.Run(m.Object);
            Assert.IsNotNull(res);
            Console.WriteLine(res.IsSuccess);
            foreach (var line in res.lines)
            {
                Console.WriteLine(line);
            }
            Assert.IsTrue(res.IsSuccess);
        }


        [TestMethod]
        public void RunInvalid()
        {
            Mock<ISprint1Task3V17> m = new Mock<ISprint1Task3V17>();
            m.Setup(f => f.ZeroCheck(150.150)).Returns(false); // Устанавливаем возвращаемое значение false
            var res = testingController.Run(m.Object);
            Assert.IsNotNull(res);
            Console.WriteLine(res.IsSuccess);
            foreach (var line in res.lines)
            {
                Console.WriteLine(line);
            }
            Assert.IsFalse(res.IsSuccess); // Проверяем, что ошибочный запуск возвращает false
        }
    }
}
