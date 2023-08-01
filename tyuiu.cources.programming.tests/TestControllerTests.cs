using System.Reflection;
using tyuiu.cources.programming.interfaces;
using tyuiu.SidorovAY.Sprint0.Task0.V0;

namespace tyuiu.cources.programming.tests
{
    [TestClass]
    public class TestControllerTests
    {
        private readonly string filename = typeof(TestingClass).Assembly.Location;
        private readonly TestController testController = 
            new TestController(
                new AssemblyController(), 
                new FileOutputController(@"C:\Temp\test.txt")
            );

        [TestMethod]
        public void TestValid()
        {
            Assert.IsTrue(testController.Run<ISprint0Task0V0>(filename));
            Assert.IsTrue(testController.Run<ISprint0Task0V1>(filename));
            Assert.IsTrue(testController.Run<ISprint0Task0V2>(filename));
        }
        [TestMethod]
        public void RunEnv()
        {
            var lib = typeof(tyuiu.SpirinIS.Sprint0.Task0.V2.con.Program).Assembly.Location;
            System.Diagnostics.Process.Start(
                new System.Diagnostics.ProcessStartInfo(lib) { 
                    RedirectStandardOutput = true
                });
        }
        [TestMethod]
        public void RunMethodValid() { 
            testController.Run<ISprint0Task0V2>(filename);
        }
    }
}
