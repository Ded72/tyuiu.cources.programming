using tyuiu.cources.programming.interfaces;

namespace tyuiu.cources.programming.tests
{
    [TestClass]
    public class TestControllerTests
    {
        [TestMethod]
        public void CheckLoadDLLValid()
        {
            var controller = new TestController();
            var intf = controller.LoadTestingInterface<ISprint0Test0V0>(@"C:\Temp\Test.dll");
            var res = intf.ReverseString("12345");
            Assert.AreEqual("54321", res);
        }
    }
}