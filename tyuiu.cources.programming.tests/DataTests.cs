using System.Reflection;
using Test;
using tyuiu.cources.programming.interfaces;

namespace tyuiu.cources.programming.tests
{
    [TestClass]
    public class DataTests
    {
        private readonly DataController dataController;

        public DataTests()
        {
            this.dataController = new DataController();
        }
        [TestMethod]
        public void GetDataValid()
        {
            var data = dataController.GetData<ISprint0Task0V0>();
            Assert.AreEqual("54321", data.result);
            Assert.AreEqual("12345", data.param![0]);
        }
    }
}
