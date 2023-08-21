using tyuiu.cources.programming.interfaces;
using tyuiu.SidorovAY.Sprint0.Task0.V0;

namespace tyuiu.cources.programming.tests
{
    [TestClass]
    public class AssemblyControllerTests
    {
        private readonly string filename = typeof(TestingClass).Assembly.Location;
        private readonly AssemblyController controller = new AssemblyController();

        [TestMethod]
        public void LoadFromFileValid()
        {
            var intf = controller.CreateInstanceFromFile<ISprint0Task0V0>(filename);
            var res = intf.ReverseString("12345");
            Assert.AreEqual("54321", res);
        }
        [TestMethod]
        public void CheckLoadFromStreamValid()
        {
            using (var stream = File.OpenRead(filename))
            {
                var intf = controller.CreateInstanceFromStream<ISprint0Task0V1>(stream);
                var res = intf.SubFrom100(10);
                Assert.AreEqual(90, res);
            }
        }
        [TestMethod]
        public void CheckLoadFromByteArrayValid()
        {
            var buffer = File.ReadAllBytes(filename);
            var intf = controller.CreateInstanceFromByteArray<ISprint0Task0V2>(buffer);
            var res = intf.Multiply(3, 5);
            Assert.AreEqual(15, res);
        }
        [TestMethod]
        public void LoadFromFileInvalid()
        {
            Assert.ThrowsException<FileNotFoundException>(() =>
            {
                var intf = controller.CreateInstanceFromFile<ISprint0Task0V0>(@"NotExists.dll");
            });
        }
        [TestMethod]
        public void LoadNotExistsInterfaceInvalid()
        {
            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                var intf = controller.CreateInstanceFromFile<ISprint0Task99V99>(filename);
            });
        }
    }
}