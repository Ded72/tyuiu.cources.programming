using tyuiu.cources.programming.interfaces;
using tyuiu.SidorovAY.Sprint0.Task0.V0;

namespace tyuiu.cources.programming.tests
{
    [TestClass]
    public class TestingControllerTests
    {
        private readonly string filename = typeof(TestingClass).Assembly.Location;
        private readonly TestingController testingController =
            new TestingController(new AssemblyController(), new TestingDataController());

        [TestMethod]
        public void RunValid()
        {
            var res = testingController.Run<ISprint0Task0V0>(filename);
            Assert.IsNotNull(res);
            Assert.AreEqual(8, res.lines.Count());
            Assert.IsTrue(res.IsSuccess);

            res = testingController.Run<ISprint0Task0V1>(filename);
            Assert.IsNotNull(res);
            Assert.AreEqual(8, res.lines.Count());
            Assert.IsTrue(res.IsSuccess);

            res = testingController.Run<ISprint0Task0V2>(filename);
            Assert.IsNotNull(res);
            Assert.AreEqual(9, res.lines.Count());
            Assert.IsTrue(res.IsSuccess);

            res = testingController.Run<ISprint1Task0V0>(filename);
            Assert.IsNotNull(res);
            Assert.AreEqual(8, res.lines.Count());
            Assert.IsTrue(res.IsSuccess);
        }
        [TestMethod]
        public void RunFail()
        {
            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                var res = testingController.Run<ISprint0Task99V99>(filename);
            });
        }
        [TestMethod]
        public void TestNoDataValid()
        {
            var res = testingController.Run2<ISprint0Task0V0>(filename);
        }
    }
}
