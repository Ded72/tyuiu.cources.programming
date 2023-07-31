using System.Reflection;
using tyuiu.cources.programming.interfaces;
using tyuiu.SidorovAY.Sprint0.Task0.V0;

namespace tyuiu.cources.programming.tests
{
    [TestClass]
    public class TestControllerTests
    {
        private readonly string filename = typeof(TestingClass).Assembly.Location;
        private readonly TestController testController = new TestController(new AssemblyController());

        [TestMethod]
        public void TestValid()
        {
            Assert.IsTrue(testController.Test<ISprint0Task0V0>(filename));
            Assert.IsTrue(testController.Test<ISprint0Task0V1>(filename));
            Assert.IsTrue(testController.Test<ISprint0Task0V2>(filename));
        }
    }
}
