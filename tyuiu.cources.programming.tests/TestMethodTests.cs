using System.Reflection;
using Test;
using tyuiu.cources.programming.interfaces;

namespace tyuiu.cources.programming.tests
{
    [TestClass]
    public class TestMethodTests
    {
        private readonly string filename;
        private AssemblyController controller;

        public TestMethodTests()
        {
            this.filename = typeof(TestingClass).Assembly.Location;
            this.controller = new AssemblyController();
        }
        [TestMethod]
        public void LoadFromFileValid()
        {
            var intf = controller.LoadFromFile<ISprint0Task0V0>(filename);
            var res = intf.ReverseString("12345");
            Assert.AreEqual("54321", res);
        }
        [TestMethod]
        public void CheckLoadFromStreamValid()
        {
            using (var stream = File.OpenRead(filename))
            {
                var intf = controller.LoadFromStream<ISprint0Task0V1>(stream);
                var res = intf.SubFrom100(10);
                Assert.AreEqual(90, res);
            }
        }
        [TestMethod]
        public void CheckLoadFromByteArrayValid()
        {
            var buffer = File.ReadAllBytes(filename);
            var intf = controller.LoadFromByteArray<ISprint0Task0V2>(buffer);
            var res = intf.Multiply(3, 5);
            Assert.AreEqual(15, res);
        }
        [TestMethod]
        public void LoadFromFileInvalid()
        {
            Assert.ThrowsException<FileNotFoundException>(() =>
            {
                var intf = controller.LoadFromFile<ISprint0Task0V0>(@"NotExists.dll");
            });
        }
        [TestMethod]
        public void LoadNotExistsInterfaceInvalid()
        {
            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                var intf = controller.LoadFromFile<ISprint0Task99V99>(filename);
            });
        }
    }
}