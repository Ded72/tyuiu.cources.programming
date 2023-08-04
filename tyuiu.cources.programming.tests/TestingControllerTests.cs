using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tyuiu.cources.programming.interfaces;
using tyuiu.SidorovAY.Sprint0.Task0.V0;

namespace tyuiu.cources.programming.tests
{
    [TestClass]
    public class TestingControllerTests
    {
        private readonly string filename = typeof(TestingClass).Assembly.Location;
        private readonly TestingController testingController =
            new TestingController(new AssemblyController(), new TestDataController());

        [TestMethod]
        public void RunValid()
        {
            var res = testingController.Run<ISprint0Task0V0>(filename);
            Assert.IsNotNull(res);
            Assert.AreEqual(8, res.Count());
            Assert.AreEqual("VALID", res.ToArray<string>()[6]);

            res = testingController.Run<ISprint0Task0V1>(filename);
            Assert.IsNotNull(res);
            Assert.AreEqual(8, res.Count());
            Assert.AreEqual("VALID", res.ToArray<string>()[6]);

            res = testingController.Run<ISprint0Task0V2>(filename);
            Assert.IsNotNull(res);
            Assert.AreEqual(9, res.Count());
            Assert.AreEqual("VALID", res.ToArray<string>()[7]);

            res = testingController.Run<ISprint1Task0V0>(filename);
            Assert.IsNotNull(res);
            Assert.AreEqual(8, res.Count());
            Assert.AreEqual("VALID", res.ToArray<string>()[6]);
        }
        [TestMethod]
        public void RunFail()
        {
            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                var res = testingController.Run<ISprint0Task99V99>(filename);
            });
        }
    }
}
