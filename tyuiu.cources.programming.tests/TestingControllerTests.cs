using Moq;
using tyuiu.cources.programming.interfaces;
using tyuiu.cources.programming.interfaces.Sprint1;
using tyuiu.SidorovAY.Sprint0.Task0.V0;

namespace tyuiu.cources.programming.tests
{
    [TestClass]
    public class TestingControllerTests
    {
        private readonly string filename = typeof(TestingClass).Assembly.Location;
        private readonly TestingController testingController =
            new TestingController( new TestingDataController());
      

        [TestMethod]
        public void RunValid()
        {

            Mock<ISprint1Task0V0> m4 = new Mock<ISprint1Task0V0>();
            m4.Setup(f => f.Calculate()).Returns(2.0);
            var res = testingController.Run(m4.Object);
            Assert.IsNotNull(res);
            Assert.AreEqual(7, res.lines.Count());
            Console.WriteLine(res.lines.Count());
            Console.WriteLine(res.IsSuccess);
            foreach (var line in res.lines)
            {
                Console.WriteLine(line);
            }
            Assert.IsTrue(res.IsSuccess);

        }
        [TestMethod]
        public void RunFail()
        {
            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                //var res = testingController.Run<ISprint0Task99V99>(filename);
            });
        }
        //[TestMethod]
        //public void TestNoDataValid()
        //{
        //    var res = testingController.Run2<ISprint0Task0V0>(filename);
        //}
    }
}
