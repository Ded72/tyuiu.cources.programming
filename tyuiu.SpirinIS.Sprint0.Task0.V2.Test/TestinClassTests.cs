using tyuiu.cources.programming.interfaces;

namespace tyuiu.SpirinIS.Sprint0.Task0.V2.Test
{
    [TestClass]
    public class TestinClassTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            ISprint0Task0V2 cls = new TestingClass();
            var res = cls.Multiply(2, 2);
            Assert.AreEqual(4, res);
        }
    }
}