namespace tyuiu.SpirinIS.Sprint0.Task0.V2.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var cls = new Class1();
            var res = cls.Multiply(2, 2);
            Assert.AreEqual(4, res);
        }
    }
}