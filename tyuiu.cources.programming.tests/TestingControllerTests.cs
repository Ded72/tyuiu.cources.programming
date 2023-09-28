using Moq;
using tyuiu.cources.programming.interfaces;
using tyuiu.cources.programming.interfaces.Sprint1;
using tyuiu.cources.programming.interfaces.Sprint2;

namespace tyuiu.cources.programming.tests
{
    [TestClass]
    public class TestingControllerTests
    {
        
        private readonly TestingController testingController =
            new TestingController( new TestingDataController());
      

        [TestMethod]
        public void RunValid()
        {
            Mock<ISprint1Task3V17> m5 = new Mock<ISprint1Task3V17>();
            m5.Setup(f => f.ZeroCheck(150.150)).Returns(true);
            var res = testingController.Run(m5.Object);
            Assert.IsNotNull(res);
            Console.WriteLine(res.IsSuccess);
            foreach (var line in res.lines)
            {
                Console.WriteLine(line);
            }
            Assert.IsTrue(res.IsSuccess);

            Mock<ISprint2Task0V0> m = new Mock<ISprint2Task0V0>();
            m.Setup(f => f.GetCompareOperations(5,6)).Returns(new bool[6] { true, true, true, true, true, true });
            res = testingController.Run(m.Object);
            Assert.IsNotNull(res);
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
